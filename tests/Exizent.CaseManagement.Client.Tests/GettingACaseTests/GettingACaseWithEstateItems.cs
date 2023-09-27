using AutoFixture;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Company;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithEstateItems : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithEstateItems(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyEstateItems()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.EstateItems.Should().BeEmpty();
    }

    [Theory]
    [AllEstateItemResourceRepresentationTypesData]
    public async Task ShouldReturnEstateItem(Type estateItemResourceRepresentationType)
    {
        var estateItem = _harness.CreateEstateItem(estateItemResourceRepresentationType);

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(estateItem)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.EstateItems.Single().Should()
            .BeEquivalentTo(estateItem, o => o.RespectingRuntimeTypes());
    }

    [Theory]
    [InlineData(EstateItemsFilter.Archived)]
    [InlineData(EstateItemsFilter.Open)]
    [InlineData(EstateItemsFilter.Complete)]
    [InlineData(EstateItemsFilter.AllAssets)]
    public async Task ShouldReturnEstateItemWithFilters(EstateItemsFilter estateItemsFilter)
    {
        var estateItem = _harness.CreateEstateItem(typeof(NationalSavingsAndInvestmentsProductResourceRepresentation));

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(estateItem)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseWithEstateItemsFilterResponse(caseResourceRepresentation.Id, estateItemsFilter,
            body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id,
            new GetCaseOptions { EstateItemsFilter = estateItemsFilter });

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.EstateItems.Single().Should()
            .BeEquivalentTo(estateItem, o => o.RespectingRuntimeTypes());
    }

    [Theory]
    [InlineData(EstateItemsFilter.Archived)]
    [InlineData(EstateItemsFilter.Open)]
    [InlineData(EstateItemsFilter.Complete)]
    [InlineData(EstateItemsFilter.AllAssets)]
    public async Task ShouldReturnEstateItemWithCompanyExpandAndFilters(EstateItemsFilter estateItemsFilter)
    {
        var estateItem = _harness.CreateEstateItem(typeof(NationalSavingsAndInvestmentsProductResourceRepresentation));
        var expectedCompany = _harness.Fixture.Create<CompanyResourceRepresentation>();

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(estateItem)
            .With(expectedCompany)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseWithCompanyAndEstateItemsFilterResponse(caseResourceRepresentation.Id,
            estateItemsFilter,
            body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id,
            new GetCaseOptions { ExpandCompany = true, EstateItemsFilter = estateItemsFilter });

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails?.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails?.Company.Should().BeEquivalentTo(caseResourceRepresentation.Company);
        caseDetails?.EstateItems.Single().Should()
            .BeEquivalentTo(estateItem, o => o.RespectingRuntimeTypes());
    }
}