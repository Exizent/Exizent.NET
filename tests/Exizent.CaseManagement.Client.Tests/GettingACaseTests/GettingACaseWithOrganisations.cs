using AutoFixture;
using Exizent.CaseManagement.Client.Models.Organisations;
using Exizent.CaseManagement.Client.Models.People;
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

    [Theory]
    [InlineData("Sole", ExecutorStatus.Sole)]
    [InlineData("JointLead", ExecutorStatus.JointLead)]
    [InlineData("JointSecondary", ExecutorStatus.JointSecondary)]
    [InlineData("Declined", ExecutorStatus.Declined)]
    [InlineData("PowerOfAttorney", ExecutorStatus.PowerOfAttorney)]
    [InlineData("PowerReserved", ExecutorStatus.PowerReserved)]
    [InlineData("Renounced", ExecutorStatus.Renounced)]
    [InlineData("Deceased", ExecutorStatus.Deceased)]
    [InlineData("LacksCapacity", ExecutorStatus.LacksCapacity)]
    public async Task ShouldReadTheExecutorStatus(string executorStatus, ExecutorStatus expected)
    {
        var organisation = _harness.Fixture.Create<OrganisationResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(organisation)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        body["organisations"]![0]!["executorStatus"] = executorStatus;

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Organisations.Single().Id.Should().Be(organisation.Id);
        caseDetails.Organisations.Single().ExecutorStatus.Should().Be(expected);
    }
}