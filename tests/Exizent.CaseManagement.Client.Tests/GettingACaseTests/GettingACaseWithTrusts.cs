using AutoFixture;
using Exizent.CaseManagement.Client.Models.People;
using Exizent.CaseManagement.Client.Models.Trusts;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithTrusts : IClassFixture<Harness>
{
    private readonly Harness _harness;
    
    public GettingACaseWithTrusts(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyTrustCollection()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Trusts.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnTrust()
    {
        var expectedTrust = _harness.Fixture.Create<TrustResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedTrust)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Trusts.Single().Should()
            .BeEquivalentTo(expectedTrust);
    }
}