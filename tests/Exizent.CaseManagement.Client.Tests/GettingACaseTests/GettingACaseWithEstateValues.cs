using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithEstateValues : IClassFixture<Harness>
{
    private readonly Harness _harness;
    public GettingACaseWithEstateValues(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnACaseWithEstateValues()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.GrossEstateValue.Should().Be(caseResourceRepresentation.GrossEstateValue);
        caseDetails.NetEstateValue.Should().Be(caseResourceRepresentation.NetEstateValue);
    }
}
