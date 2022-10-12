using AutoFixture;
using Exizent.CaseManagement.Client.Models.Organisations;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithOrganisations : IClassFixture<Harness>
{
    private readonly Harness _harness;
    
    public GettingACaseWithOrganisations(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyOrganisationCollection()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Organisations.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnOrganisation()
    {
        var expectedOrganisation = _harness.Fixture.Create<OrganisationResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedOrganisation)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Organisations.Single().Should()
            .BeEquivalentTo(expectedOrganisation);
    }
}