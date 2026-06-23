using AutoFixture;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithExpenses : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithExpenses(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyExpenseCollection()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Expenses.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnExpense()
    {
        var expectedExpense = _harness.Fixture.Create<ExpenseResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedExpense)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Expenses.Single().Should()
            .BeEquivalentTo(expectedExpense);
    }

    [Fact]
    public async Task ShouldDeserialiseShareSettlementWithPaySurplusOnExpenseSettlement()
    {
        var bankAccountId = Guid.NewGuid();
        var settlement = new ExpenseSettlementResourceRepresentation
        {
            Id = Guid.NewGuid(),
            Value = 100m,
            SourceOfFunds = SourceOfFunds.ProceedsOfSale,
            At = DateTime.UtcNow,
            ShareSettlement = new ShareSettlementDetailsResourceRepresentation
            {
                Quantity = 10,
                Price = 1000m,
                PaySurplus = new PaySurplusDestinationResourceRepresentation
                {
                    Type = PaySurplusToType.BankAccount,
                    CaseItemId = bankAccountId
                }
            }
        };
        var expense = new ExpenseResourceRepresentation
        {
            Id = Guid.NewGuid(),
            Description = "Share Sale",
            CaseItemId = Guid.NewGuid(),
            Value = 60m,
            From = DateTime.UtcNow,
            To = DateTime.UtcNow,
            Supplier = "Broker",
            Purpose = ExpensePurposeType.AssetSaleOrLiabilityRepayment,
            Settlements = new List<ExpenseSettlementResourceRepresentation> { settlement }
        };
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expense)
            .Build();

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id,
            CaseJsonBuilder.Build(caseResourceRepresentation).ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        var returnedSettlement = caseDetails!.Expenses.Single().Settlements.Single();
        returnedSettlement.ShareSettlement.Should().NotBeNull();
        returnedSettlement.ShareSettlement!.Quantity.Should().Be(10);
        returnedSettlement.ShareSettlement.Price.Should().Be(1000m);
        returnedSettlement.ShareSettlement.Value.Should().Be(100m);  // 10 * 1000 / 100
        returnedSettlement.ShareSettlement.PaySurplus!.Type.Should().Be(PaySurplusToType.BankAccount);
        returnedSettlement.ShareSettlement.PaySurplus.CaseItemId.Should().Be(bankAccountId);
    }

}