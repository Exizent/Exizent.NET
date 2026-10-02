using System.Net;
using Exizent.CaseManagement.Client.Models.EstateItemValuations;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.EstateItemValuations;

public sealed class RequestingAndGettingAnEstateItemValuation : IClassFixture<Harness>
{
    private readonly Harness _harness;
    private readonly Guid _caseId = Guid.NewGuid();
    private readonly Guid _estateItemId = Guid.NewGuid();
    private readonly Guid _valuationId = Guid.NewGuid();

    public RequestingAndGettingAnEstateItemValuation(Harness harness) => _harness = harness;

    private string Collection => $"/cases/{_caseId}/estateitems/{_estateItemId}/valuations";
    private string Item => $"{Collection}/{_valuationId}";

    private string Valuation(string status = "Pending", string completedAt = "null", string value = "null",
        string confidence = "null", string failureReason = "null", string details = "null") =>
        @"{""id"":""" + _valuationId + @""",""estateItemId"":""" + _estateItemId + @""",""provider"":""Hometrack""," +
        @"""status"":""" + status + @""",""requestedAt"":""2026-10-02T09:30:15Z"",""completedAt"":" + completedAt +
        @",""subject"":{""type"":""Property"",""address"":{""buildingNumber"":""12""," +
        @"""buildingNameOrFlatNumber"":""Flat 3"",""streetName"":""High Street"",""city"":""Leeds""," +
        @"""postcode"":""LS1 4AB""}},""value"":" + value + @",""confidence"":" + confidence +
        @",""failureReason"":" + failureReason + @",""details"":" + details + "}";

    [Fact]
    public async Task ShouldPostTheProviderToTheEstateItemsValuations()
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.Accepted, Valuation());

        await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Hometrack);

        using var _ = new AssertionScope();
        _harness.ClientHandler.Requests[^1].Should().Be(("POST", Collection));
        _harness.ClientHandler.LastRequestBody.Should().Be(@"{""provider"":""Hometrack""}");
    }

    [Fact]
    public async Task ShouldReadTheAcceptedValuationWithItsSubject()
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.Accepted, Valuation());

        var response = await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Hometrack);

        using var _ = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        response.IsAccepted.Should().BeTrue();
        response.Valuation!.Id.Should().Be(_valuationId);
        response.Valuation.IsPending.Should().BeTrue();
        response.Valuation.Details.Should().BeNull();
        response.Valuation.Subject.Should().BeOfType<PropertyEstateItemValuationSubjectResourceRepresentation>()
            .Which.Address.Postcode.Should().Be("LS1 4AB");
    }

    [Fact]
    public async Task ShouldReadAConflictsFieldErrors()
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.Conflict,
            @"{""title"":""One or more validation errors occurred."",""status"":409," +
            @"""detail"":""The estate item cannot be valued by this provider as it stands.""," +
            @"""errors"":{""address.postcode"":[""The building needs a postcode to be valued.""]," +
            @"""address.city"":[""The building needs a city to be valued.""]}}");

        var response = await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Hometrack);

        using var _ = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        response.IsAccepted.Should().BeFalse();
        response.Detail.Should().Be("The estate item cannot be valued by this provider as it stands.");
        response.Errors.Should().BeEquivalentTo(new Dictionary<string, string[]>
        {
            ["address.postcode"] = new[] { "The building needs a postcode to be valued." },
            ["address.city"] = new[] { "The building needs a city to be valued." }
        });
    }

    [Fact]
    public async Task ShouldReadABadRequestsErrors()
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.BadRequest,
            @"{""status"":400,""errors"":{""provider"":[""Valuations from this provider are not available.""]}}");

        var response = await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Zoopla);

        using var _ = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        response.Errors["provider"].Should().Equal("Valuations from this provider are not available.");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("not json")]
    public async Task ShouldReportAConflictWithoutAReadableBody(string? body)
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.Conflict, body);

        var response = await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Hometrack);

        using var _ = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
        response.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReportAMissingCaseOrEstateItem()
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.NotFound);

        var response = await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Hometrack);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ShouldThrowOnAnythingElse()
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.InternalServerError);

        var act = () => _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Hometrack);

        await act.Should().ThrowAsync<HttpRequestException>();
    }

    [Fact]
    public async Task ShouldReadACompletedValuationWithItsDetails()
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.OK,
            Valuation("Completed", @"""2026-10-02T09:30:20Z""", "412500.00", @"""High""",
                details: @"{""type"":""Property"",""matchedAddress"":""FLAT 3, 12 HIGH STREET, LEEDS""}"));

        var valuation = await _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId);

        using var _ = new AssertionScope();
        _harness.ClientHandler.Requests[^1].Should().Be(("GET", Item));
        valuation!.Status.Should().Be(EstateItemValuationStatus.Completed);
        valuation.IsPending.Should().BeFalse();
        valuation.CompletedAt.Should().Be(new DateTime(2026, 10, 2, 9, 30, 20, DateTimeKind.Utc));
        valuation.Value.Should().Be(412500m);
        valuation.Confidence.Should().Be(EstateItemValuationConfidence.High);
        valuation.Details.Should().BeOfType<PropertyEstateItemValuationDetailsResourceRepresentation>()
            .Which.MatchedAddress.Should().Be("FLAT 3, 12 HIGH STREET, LEEDS");
    }

    [Fact]
    public async Task ShouldReadAFailedValuationWithItsReason()
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.OK,
            Valuation("Failed", @"""2026-10-02T09:30:20Z""", failureReason: @"""Property not found"""));

        var valuation = await _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId);

        using var _ = new AssertionScope();
        valuation!.Status.Should().Be(EstateItemValuationStatus.Failed);
        valuation.FailureReason.Should().Be("Property not found");
        valuation.Value.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReadNothingForAMissingValuation()
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.NotFound);

        (await _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId)).Should().BeNull();
    }

    [Fact]
    public async Task ShouldRejectASubjectOfAnUnknownType()
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.OK,
            Valuation().Replace(@"""type"":""Property""", @"""type"":""Vehicle"""));

        var act = () => _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId);

        await act.Should().ThrowAsync<System.Text.Json.JsonException>();
    }
}
