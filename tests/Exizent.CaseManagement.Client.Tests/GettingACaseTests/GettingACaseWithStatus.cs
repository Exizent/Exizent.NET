using System.Text.Json;
using JsonNode = System.Text.Json.Nodes.JsonNode;
using AutoFixture;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// The API can return a status this version of the client does not know. It must read as null rather than stop
/// the rest of the case from being read.
/// </summary>
public sealed class GettingACaseWithStatus : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithStatus(Harness harness) => _harness = harness;

    [Theory]
    [InlineData("Open", CaseStatus.Open)]
    [InlineData("GatheringInfo", CaseStatus.GatheringInfo)]
    [InlineData("WithHmrc", CaseStatus.WithHmrc)]
    [InlineData("WithCourt", CaseStatus.WithCourt)]
    [InlineData("GrantIssued", CaseStatus.GrantIssued)]
    [InlineData("CollectingFunds", CaseStatus.CollectingFunds)]
    [InlineData("AccountsAndDistribution", CaseStatus.AccountsAndDistribution)]
    [InlineData("Closed", CaseStatus.Closed)]
    [InlineData("Archived", CaseStatus.Archived)]
    public async Task ShouldReadTheStatus(string status, CaseStatus expected)
    {
        var caseDetails = await GetCaseWithStatus(status);

        caseDetails.Status.Should().Be(expected);
    }

    [Fact]
    public async Task ShouldReadAnUnknownStatusAsNullAndStillReadTheRestOfTheCase()
    {
        var document = _harness.Fixture.Create<CaseDocumentResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .WithCompanyCaseId("CASE-123")
            .With(document)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        body["status"] = "SomeFutureStatus";
        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Status.Should().BeNull();
        caseDetails.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.CompanyCaseId.Should().Be("CASE-123");
        caseDetails.Documents.Single().Should().BeEquivalentTo(document, o => o.RespectingRuntimeTypes());
    }

    [Theory]
    [InlineData("4")]
    [InlineData("true")]
    [InlineData("{ \"name\": \"WithHmrc\" }")]
    [InlineData("[ \"WithHmrc\" ]")]
    public async Task ShouldReadAStatusThatIsNotANameAsNull(string status)
    {
        var caseDetails = await GetCaseWithStatus(JsonNode.Parse(status));

        using var _ = new AssertionScope();
        caseDetails.Status.Should().BeNull();
        caseDetails.CompanyCaseId.Should().Be("CASE-123");
    }

    [Fact]
    public async Task ShouldReadANullStatusAsNull()
    {
        var caseDetails = await GetCaseWithStatus(null);

        caseDetails.Status.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReadAMissingStatusAsNull()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder().Build();
        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        body.ContainsKey("status").Should().BeFalse();
        caseDetails.Should().NotBeNull();
        caseDetails!.Status.Should().BeNull();
    }

    [Fact]
    public void ShouldWriteTheStatusAsItsName()
    {
        var json = JsonSerializer.Serialize(new CaseResourceRepresentation { Status = CaseStatus.WithHmrc },
            new JsonSerializerOptions(JsonSerializerDefaults.Web));

        JsonNode.Parse(json)!["status"]!.GetValue<string>().Should().Be("WithHmrc");
    }

    private async Task<CaseResourceRepresentation> GetCaseWithStatus(JsonNode? status)
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .WithCompanyCaseId("CASE-123")
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        body["status"] = status;
        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);
        caseDetails.Should().NotBeNull();
        return caseDetails!;
    }
}
