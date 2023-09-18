using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class GettingEstateItems: IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingEstateItems(Harness harness) => _harness = harness;

    [Theory]
    [AllEstateItemResourceRepresentationTypesData]
    public async Task ShouldReturnEstateItem(Type estateItemResourceRepresentationType)
    {
        var caseId = Guid.NewGuid();
        var estateItem = _harness.CreateEstateItem(estateItemResourceRepresentationType);

        var body = EstateItemJsonBuilderFactory.Create(estateItem).Build();

        _harness.ClientHandler.AddGetEstateItemResponse(caseId, estateItem.Id, body.ToJsonString());

        var estateitemResponse = await _harness.Client.GetEstateItem(caseId, estateItem.Id);

        using var _ = new AssertionScope();
        estateitemResponse.Should().NotBeNull();
        estateitemResponse!.Id.Should().Be(estateItem.Id);
        estateitemResponse.Should()
            .BeEquivalentTo(estateItem, o => o.RespectingRuntimeTypes());
    }
}