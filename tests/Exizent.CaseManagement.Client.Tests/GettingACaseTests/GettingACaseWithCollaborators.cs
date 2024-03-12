using AutoFixture;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Collaborators;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class GettingACaseWithCollaborators : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithCollaborators(Harness harness) => _harness = harness;
    
    [Fact]
    public async Task ShouldReturnACaseWithCollaborators()
    {
        var expectedCollaborators = _harness.Fixture.CreateMany<CollaboratorResourceRepresentation>().ToList();

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .WithCollaborators(expectedCollaborators)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Collaborators.Should().BeEquivalentTo(caseResourceRepresentation.Collaborators);
    }
    
    [Fact]
    public async Task ShouldReturnACaseWithOwner()
    {
        var expectedOwner = _harness.Fixture.Create<CollaboratorResourceRepresentation>();

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .WithOwner(expectedOwner)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Owner.Should().BeEquivalentTo(caseResourceRepresentation.Owner);
    }
}