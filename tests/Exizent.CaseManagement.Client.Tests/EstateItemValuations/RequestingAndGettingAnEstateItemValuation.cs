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

    private static string Valuation(Guid id, string status = "Pending", string finishedAt = "null",
        string value = "null", string confidence = "null", string failure = "null", string provider = "Hometrack") =>
        @"{""id"":""" + id + @""",""provider"":""" + provider + @""",""status"":""" + status +
        @""",""requestedAt"":""2026-10-02T09:30:15Z"",""finishedAt"":" + finishedAt + @",""value"":" + value +
        @",""confidence"":" + confidence + @",""failure"":" + failure + "}";

    private string Valuation(string status = "Pending", string finishedAt = "null", string value = "null",
        string confidence = "null", string failure = "null") =>
        Valuation(_valuationId, status, finishedAt, value, confidence, failure);

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

    [Theory]
    [InlineData(EstateItemValuationProvider.Hometrack, "Hometrack")]
    [InlineData(EstateItemValuationProvider.UkVehicleData, "UkVehicleData")]
    public async Task ShouldPostEveryProviderByTheApisName(EstateItemValuationProvider provider, string name)
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.Accepted,
            Valuation(_valuationId, provider: name));

        var response = await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId, provider);

        using var _ = new AssertionScope();
        _harness.ClientHandler.LastRequestBody.Should().Be(@"{""provider"":""" + name + @"""}");
        response.Valuation!.Provider.Should().Be(provider);
    }

    [Theory]
    [InlineData("Hometrack", EstateItemValuationProvider.Hometrack)]
    [InlineData("UkVehicleData", EstateItemValuationProvider.UkVehicleData)]
    public async Task ShouldReadEveryProviderTheApiReturns(string name, EstateItemValuationProvider provider)
    {
        _harness.ClientHandler.AddResponse("GET", Collection, HttpStatusCode.OK,
            $"[{Valuation(_valuationId, "Completed", @"""2026-10-02T09:30:20Z""", "7250.00", provider: name)}]");

        var valuations = (await _harness.Client.ListEstateItemValuations(_caseId, _estateItemId))!;

        using var _ = new AssertionScope();
        valuations.Should().ContainSingle().Which.Provider.Should().Be(provider);
        valuations[0].Confidence.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReadTheAcceptedValuation()
    {
        _harness.ClientHandler.AddResponse("POST", Collection, HttpStatusCode.Accepted, Valuation());

        var response = await _harness.Client.RequestEstateItemValuation(_caseId, _estateItemId,
            EstateItemValuationProvider.Hometrack);

        using var _ = new AssertionScope();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
        response.IsAccepted.Should().BeTrue();
        response.Valuation!.Id.Should().Be(_valuationId);
        response.Valuation.IsPending.Should().BeTrue();
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
            EstateItemValuationProvider.Hometrack);

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

    [Theory]
    [InlineData("Pending", EstateItemValuationStatus.Pending, true)]
    [InlineData("Completed", EstateItemValuationStatus.Completed, false)]
    [InlineData("Failed", EstateItemValuationStatus.Failed, false)]
    public async Task ShouldReadTheStatusWhenPolled(string statusName, EstateItemValuationStatus status,
        bool isPending)
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.OK, Valuation(statusName));

        var valuation = await _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId);

        using var _ = new AssertionScope();
        _harness.ClientHandler.Requests[^1].Should().Be(("GET", Item));
        valuation!.Id.Should().Be(_valuationId);
        valuation.Status.Should().Be(status);
        valuation.IsPending.Should().Be(isPending);
    }

    [Fact]
    public async Task ShouldReadACompletedValuation()
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.OK,
            Valuation("Completed", @"""2026-10-02T09:30:20Z""", "412500.00", @"""High"""));

        var valuation = await _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId);

        valuation.Should().BeEquivalentTo(new EstateItemValuationResourceRepresentation
        {
            Id = _valuationId,
            Provider = EstateItemValuationProvider.Hometrack,
            Status = EstateItemValuationStatus.Completed,
            RequestedAt = new DateTime(2026, 10, 2, 9, 30, 15, DateTimeKind.Utc),
            FinishedAt = new DateTime(2026, 10, 2, 9, 30, 20, DateTimeKind.Utc),
            Value = 412500m,
            Confidence = EstateItemValuationConfidence.High
        });
    }

    [Theory]
    [InlineData("NotValued", EstateItemValuationFailure.NotValued)]
    [InlineData("ProviderUnavailable", EstateItemValuationFailure.ProviderUnavailable)]
    [InlineData("TimedOut", EstateItemValuationFailure.TimedOut)]
    [InlineData("CouldNotStart", EstateItemValuationFailure.CouldNotStart)]
    [InlineData("CouldNotComplete", EstateItemValuationFailure.CouldNotComplete)]
    public async Task ShouldReadEveryFailureTheApiReturns(string failureName, EstateItemValuationFailure failure)
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.OK,
            Valuation("Failed", @"""2026-10-02T09:30:20Z""", failure: $@"""{failureName}"""));

        var valuation = await _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId);

        using var _ = new AssertionScope();
        valuation!.Status.Should().Be(EstateItemValuationStatus.Failed);
        valuation.Failure.Should().Be(failure);
        valuation.Value.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReadNothingForAMissingValuation()
    {
        _harness.ClientHandler.AddResponse("GET", Item, HttpStatusCode.NotFound);

        (await _harness.Client.GetEstateItemValuation(_caseId, _estateItemId, _valuationId)).Should().BeNull();
    }

    [Fact]
    public async Task ShouldListTheValuationsInTheOrderTheApiSendsThem()
    {
        var newer = Guid.NewGuid();
        var older = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("GET", Collection, HttpStatusCode.OK,
            $"[{Valuation(newer)},{Valuation(older, "Failed", @"""2026-10-01T09:30:20Z""", failure: @"""NotValued""")}]");

        var valuations = (await _harness.Client.ListEstateItemValuations(_caseId, _estateItemId))!;

        using var _ = new AssertionScope();
        _harness.ClientHandler.Requests[^1].Should().Be(("GET", Collection));
        valuations.Select(v => v.Id).Should().Equal(newer, older);
        valuations[0].IsPending.Should().BeTrue();
        valuations[1].Failure.Should().Be(EstateItemValuationFailure.NotValued);
    }

    [Fact]
    public async Task ShouldListNoneForAnEstateItemWithoutValuations()
    {
        _harness.ClientHandler.AddResponse("GET", Collection, HttpStatusCode.OK, "[]");

        (await _harness.Client.ListEstateItemValuations(_caseId, _estateItemId)).Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task ShouldListNothingForAnEstateItemThatCannotBeValued()
    {
        _harness.ClientHandler.AddResponse("GET", Collection, HttpStatusCode.NotFound);

        (await _harness.Client.ListEstateItemValuations(_caseId, _estateItemId)).Should().BeNull();
    }

    [Fact]
    public async Task ShouldThrowWhenListingFailsOtherwise()
    {
        _harness.ClientHandler.AddResponse("GET", Collection, HttpStatusCode.InternalServerError);

        var act = () => _harness.Client.ListEstateItemValuations(_caseId, _estateItemId);

        await act.Should().ThrowAsync<HttpRequestException>();
    }
}
