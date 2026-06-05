using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithIncomes : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithIncomes(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyIncomes()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Incomes.Should().BeEmpty();
    }

    [Theory]
    [AllIncomeResourceRepresentationTypesData]
    public async Task ShouldReturnIncome(Type incomeResourceRepresentationType)
    {
        var income = _harness.CreateIncome(incomeResourceRepresentationType);

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(income)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Incomes.Single().Should()
            .BeEquivalentTo(income, o => o.RespectingRuntimeTypes());
    }

    [Fact]
    public async Task ShouldComputeValueForShareholdingIncome()
    {
        var income = new ShareholdingIncomeResourceRepresentation
        {
            EstateItemId = Guid.NewGuid(),
            QuantityOfShares = 100m,
            SharePrice = 250m,   // pence — so £2.50 per share
            Cash = 10m,
            Source = IncomeSource.Dividend,
            Destination = IncomeDestination.AssetValue,
            From = new DateTime(2023, 1, 1),
            To = new DateTime(2023, 12, 31)
        };

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(income)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        var shareholdingIncome = caseDetails!.Incomes.Single()
            .Should().BeOfType<ShareholdingIncomeResourceRepresentation>().Subject;

        // Value = QuantityOfShares * (SharePrice / 100) + Cash = 100 * 2.50 + 10 = 260
        shareholdingIncome.Value.Should().Be(260m);
    }
}