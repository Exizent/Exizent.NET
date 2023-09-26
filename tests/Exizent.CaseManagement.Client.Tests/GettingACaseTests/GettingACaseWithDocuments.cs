using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using AutoFixture;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithDocuments : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithDocuments(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyDocuments()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Documents.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnDocument()
    {
        var document = _harness.Fixture.Create<CaseDocumentResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(document)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Documents.Single().Should()
            .BeEquivalentTo(document, o => o.RespectingRuntimeTypes());
    }
}