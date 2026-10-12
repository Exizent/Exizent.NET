using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithInheritanceTaxFlags : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithInheritanceTaxFlags(Harness harness) => _harness = harness;

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadWhetherAPensionIsValidForInheritanceTax(bool value)
    {
        var pension = await GetCaseEstateItemWith<PensionResourceRepresentation>("isValidForInheritanceTax", value);

        pension.IsValidForInheritanceTax.Should().Be(value);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadWhetherAPensionHasAValidNominationForm(bool value)
    {
        var pension = await GetCaseEstateItemWith<PensionResourceRepresentation>("hasValidNominationForm", value);

        pension.HasValidNominationForm.Should().Be(value);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadWhetherLandIsValidForInheritanceTax(bool value)
    {
        var land = await GetCaseEstateItemWith<LandResourceRepresentation>("isValidForInheritanceTax", value);

        land.IsValidForInheritanceTax.Should().Be(value);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadWhetherABuildingIsValidForInheritanceTax(bool value)
    {
        var building = await GetCaseEstateItemWith<BuildingResourceRepresentation>("isValidForInheritanceTax", value);

        building.IsValidForInheritanceTax.Should().Be(value);
    }

    private async Task<T> GetCaseEstateItemWith<T>(string propertyName, bool value)
        where T : EstateItemResourceRepresentation
    {
        var estateItem = _harness.CreateEstateItem(typeof(T));
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(estateItem)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        body["estateItems"]![0]![propertyName] = JsonValue.Create(value);
        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        return caseDetails!.EstateItems.Single().Should().BeOfType<T>().Subject;
    }
}
