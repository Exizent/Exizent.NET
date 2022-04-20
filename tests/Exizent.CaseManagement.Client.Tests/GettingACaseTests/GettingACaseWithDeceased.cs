using AutoFixture;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithDeceased : IClassFixture<Harness>
{
    private readonly Harness _harness;
    public GettingACaseWithDeceased(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnNullWhen404NotFound()
    {
        var caseId = Guid.NewGuid();

        var caseDetails = await _harness.Client.GetCase(caseId);
        caseDetails.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnACaseWithDeceasedDetails()
    {
        
        var expectedDeceased = _harness.Fixture.Create<DeceasedResourceRepresentation>();

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedDeceased)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Deceased.Should().BeEquivalentTo(caseResourceRepresentation.Deceased);
    }
}