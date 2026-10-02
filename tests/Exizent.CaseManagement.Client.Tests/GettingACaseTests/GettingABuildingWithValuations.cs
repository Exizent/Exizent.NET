using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.EstateItemValuations;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingABuildingWithValuations : IClassFixture<Harness>
{
    private const string CompletedHometrack = @"{
        ""id"": ""6b0f5d0e-58a4-4a8e-9d3c-2f1e0c1a7b01"",
        ""provider"": ""Hometrack"",
        ""status"": ""Completed"",
        ""valuedAt"": ""2026-09-30T10:15:00Z"",
        ""value"": 350000.00,
        ""confidence"": ""High"",
        ""failureReason"": null
    }";

    private const string FailedZoopla = @"{
        ""id"": ""6b0f5d0e-58a4-4a8e-9d3c-2f1e0c1a7b02"",
        ""provider"": ""Zoopla"",
        ""status"": ""Failed"",
        ""valuedAt"": ""2024-03-01T09:00:00Z"",
        ""value"": null,
        ""confidence"": null,
        ""failureReason"": ""Property not found""
    }";

    private readonly Harness _harness;

    public GettingABuildingWithValuations(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReadEveryValuationInTheOrderTheApiSendsThem()
    {
        var (caseId, _, body) = BuildingCase();
        BuildingJson(body)["valuations"] = JsonNode.Parse($"[{CompletedHometrack}, {FailedZoopla}]");
        _harness.ClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var valuations = (await GetBuilding(caseId)).Valuations;

        using var _ = new AssertionScope();
        valuations.Should().HaveCount(2);
        valuations[0].Should().BeEquivalentTo(new EstateItemValuationSummaryResourceRepresentation
        {
            Id = Guid.Parse("6b0f5d0e-58a4-4a8e-9d3c-2f1e0c1a7b01"),
            Provider = EstateItemValuationProvider.Hometrack,
            Status = EstateItemValuationStatus.Completed,
            ValuedAt = new DateTime(2026, 9, 30, 10, 15, 0, DateTimeKind.Utc),
            Value = 350000m,
            Confidence = EstateItemValuationConfidence.High
        });
        valuations[1].Should().BeEquivalentTo(new EstateItemValuationSummaryResourceRepresentation
        {
            Id = Guid.Parse("6b0f5d0e-58a4-4a8e-9d3c-2f1e0c1a7b02"),
            Provider = EstateItemValuationProvider.Zoopla,
            Status = EstateItemValuationStatus.Failed,
            ValuedAt = new DateTime(2024, 3, 1, 9, 0, 0, DateTimeKind.Utc),
            FailureReason = "Property not found"
        });
    }

    [Theory]
    [InlineData("Hometrack", EstateItemValuationProvider.Hometrack, "Completed", EstateItemValuationStatus.Completed)]
    [InlineData("Hometrack", EstateItemValuationProvider.Hometrack, "Failed", EstateItemValuationStatus.Failed)]
    [InlineData("Zoopla", EstateItemValuationProvider.Zoopla, "Completed", EstateItemValuationStatus.Completed)]
    [InlineData("Zoopla", EstateItemValuationProvider.Zoopla, "Failed", EstateItemValuationStatus.Failed)]
    public async Task ShouldReadEveryProviderAndStatusTheApiReturns(string providerName,
        EstateItemValuationProvider provider, string statusName, EstateItemValuationStatus status)
    {
        var (caseId, _, body) = BuildingCase();
        var valuation = JsonNode.Parse(CompletedHometrack)!;
        valuation["provider"] = providerName;
        valuation["status"] = statusName;
        BuildingJson(body)["valuations"] = new JsonArray(valuation);
        _harness.ClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var read = (await GetBuilding(caseId)).Valuations.Single();

        using var _ = new AssertionScope();
        read.Provider.Should().Be(provider);
        read.Status.Should().Be(status);
    }

    [Theory]
    [InlineData("Low", EstateItemValuationConfidence.Low)]
    [InlineData("Medium", EstateItemValuationConfidence.Medium)]
    [InlineData("High", EstateItemValuationConfidence.High)]
    [InlineData(null, null)]
    public async Task ShouldReadEveryConfidenceTheApiReturns(string? confidenceName,
        EstateItemValuationConfidence? confidence)
    {
        var (caseId, _, body) = BuildingCase();
        var valuation = JsonNode.Parse(CompletedHometrack)!;
        valuation["confidence"] = confidenceName;
        BuildingJson(body)["valuations"] = new JsonArray(valuation);
        _harness.ClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        (await GetBuilding(caseId)).Valuations.Single().Confidence.Should().Be(confidence);
    }

    [Fact]
    public async Task ShouldReadNoValuationsWhenTheApiPredatesThem()
    {
        var (caseId, building, body) = BuildingCase();
        BuildingJson(body).Remove("valuations");
        _harness.ClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var read = await GetBuilding(caseId);

        using var _ = new AssertionScope();
        read.Valuations.Should().BeEmpty();
        read.Should().BeEquivalentTo(building, o => o.Excluding(b => b.Valuations));
    }

    [Fact]
    public async Task ShouldReadNoValuationsWhenTheApiSendsNull()
    {
        var (caseId, _, body) = BuildingCase();
        BuildingJson(body)["valuations"] = null;
        _harness.ClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var read = await GetBuilding(caseId);

        read.Valuations.Should().NotBeNull().And.BeEmpty();
    }

    [Fact]
    public async Task ShouldIgnoreFieldsItDoesNotKnow()
    {
        var (caseId, _, body) = BuildingCase();
        var valuation = JsonNode.Parse(CompletedHometrack)!;
        valuation["rentalValue"] = 1200;
        valuation["valueRange"] = new JsonObject { { "low", 300000 }, { "high", 400000 } };
        BuildingJson(body)["valuations"] = new JsonArray(valuation);
        BuildingJson(body)["somethingNew"] = "unknown to this client";
        _harness.ClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var read = (await GetBuilding(caseId)).Valuations.Single();

        using var _ = new AssertionScope();
        read.Provider.Should().Be(EstateItemValuationProvider.Hometrack);
        read.Value.Should().Be(350000m);
    }

    [Fact]
    public async Task ShouldReadValuationsOnTheSingleEstateItem()
    {
        var (caseId, building, body) = BuildingCase();
        var estateItem = BuildingJson(body);
        estateItem["valuations"] = JsonNode.Parse($"[{CompletedHometrack}, {FailedZoopla}]");
        _harness.ClientHandler.AddGetEstateItemResponse(caseId, building.Id, estateItem.ToJsonString());

        var read = await _harness.Client.GetEstateItem(caseId, building.Id);

        read.Should().BeOfType<BuildingResourceRepresentation>()
            .Which.Valuations.Select(v => (v.Provider, v.Status)).Should().Equal(
                (EstateItemValuationProvider.Hometrack, EstateItemValuationStatus.Completed),
                (EstateItemValuationProvider.Zoopla, EstateItemValuationStatus.Failed));
    }

    private (Guid CaseId, BuildingResourceRepresentation Building, JsonObject Body) BuildingCase()
    {
        var building = (BuildingResourceRepresentation)_harness.CreateEstateItem(
            typeof(BuildingResourceRepresentation));
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder().With(building).Build();

        return (caseResourceRepresentation.Id, building, CaseJsonBuilder.Build(caseResourceRepresentation));
    }

    private static JsonObject BuildingJson(JsonObject caseBody) => caseBody["estateItems"]![0]!.AsObject();

    private async Task<BuildingResourceRepresentation> GetBuilding(Guid caseId)
    {
        var caseDetails = await _harness.Client.GetCase(caseId);

        return caseDetails!.EstateItems.OfType<BuildingResourceRepresentation>().Single();
    }
}
