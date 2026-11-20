using AutoFixture;
using Exizent.CaseManagement.Client.Models.People;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithPeople : IClassFixture<Harness>
{
    private readonly Harness _harness;
    
    public GettingACaseWithPeople(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyPersonCollection()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.People.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnPerson()
    {
        var expectedPerson = _harness.Fixture.Create<PersonResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedPerson)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.People.Single().Should()
            .BeEquivalentTo(expectedPerson);
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
        var person = _harness.Fixture.Create<PersonResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(person)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        body["people"]![0]!["executorStatus"] = executorStatus;

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.People.Single().Id.Should().Be(person.Id);
        caseDetails.People.Single().ExecutorStatus.Should().Be(expected);
    }
}