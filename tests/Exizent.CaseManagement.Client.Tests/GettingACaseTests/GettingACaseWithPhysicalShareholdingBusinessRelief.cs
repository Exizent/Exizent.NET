using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithPhysicalShareholdingBusinessRelief : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithPhysicalShareholdingBusinessRelief(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldDeserialiseBusinessReliefSplitAcross50And100Percent()
    {
        var shareholding = new PhysicalShareholdingResourceRepresentation
        {
            Id = Guid.NewGuid(),
            OwnedForTwoYears = true,
            BusinessReliefAt50Percent = 250.50m,
            BusinessReliefAt100Percent = 749.50m
        };

        var caseDetails = await GetCaseWith(shareholding);

        using var _ = new AssertionScope();
        var actual = caseDetails.EstateItems.Single().Should()
            .BeOfType<PhysicalShareholdingResourceRepresentation>().Subject;
        actual.OwnedForTwoYears.Should().BeTrue();
        actual.BusinessReliefAt50Percent.Should().Be(250.50m);
        actual.BusinessReliefAt100Percent.Should().Be(749.50m);
    }

    [Fact]
    public async Task ShouldDeserialiseNullBusinessReliefWhenNotClaimed()
    {
        var shareholding = new PhysicalShareholdingResourceRepresentation
        {
            Id = Guid.NewGuid(),
            OwnedForTwoYears = false,
            BusinessReliefAt50Percent = null,
            BusinessReliefAt100Percent = null
        };

        var caseDetails = await GetCaseWith(shareholding);

        using var _ = new AssertionScope();
        var actual = caseDetails.EstateItems.Single().Should()
            .BeOfType<PhysicalShareholdingResourceRepresentation>().Subject;
        actual.OwnedForTwoYears.Should().BeFalse();
        actual.BusinessReliefAt50Percent.Should().BeNull();
        actual.BusinessReliefAt100Percent.Should().BeNull();
    }

    private async Task<CaseResourceRepresentation> GetCaseWith(
        PhysicalShareholdingResourceRepresentation shareholding)
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(shareholding)
            .Build();

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id,
            CaseJsonBuilder.Build(caseResourceRepresentation).ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        caseDetails.Should().NotBeNull();
        return caseDetails!;
    }
}
