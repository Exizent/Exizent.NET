using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// Income destinations and share sale surplus types are still returned alongside the case item ids that identify
/// them, and still deserialise into the obsolete enum properties.
/// </summary>
#pragma warning disable CS0618
public sealed class GettingACaseWithMoneyDestinations : IClassFixture<Harness>
{
    private const string OtherDestination = "Paid directly to the beneficiary";

    private readonly Harness _harness;

    public GettingACaseWithMoneyDestinations(Harness harness) => _harness = harness;

    [Theory]
    [InlineData(IncomeDestination.ClientCashAccount, true)]
    [InlineData(IncomeDestination.AssetValue, true)]
    [InlineData(IncomeDestination.BankAccount, true)]
    [InlineData(IncomeDestination.Other, false)]
    public async Task ShouldReadTheIncomeDestination(IncomeDestination destination, bool paidToACaseItem)
    {
        var destinationCaseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var otherDestination = paidToACaseItem ? null : OtherDestination;
        var caseJson = CaseJson(incomes: new JsonArray(
            IncomeJson(Guid.NewGuid(), destination, destinationCaseItemId, otherDestination)));

        var caseDetails = await GetCase(caseJson);

        var income = caseDetails.Incomes.Should().ContainSingle()
            .Which.Should().BeOfType<IncomeResourceRepresentation>().Subject;
        using var _ = new AssertionScope();
        income.Destination.Should().Be(destination);
        income.DestinationCaseItemId.Should().Be(destinationCaseItemId);
        income.OtherDestination.Should().Be(otherDestination);
    }

    [Theory]
    [InlineData(IncomeDestination.ClientCashAccount, true)]
    [InlineData(IncomeDestination.AssetValue, true)]
    [InlineData(IncomeDestination.BankAccount, true)]
    [InlineData(IncomeDestination.Other, false)]
    public async Task ShouldReadTheShareholdingIncomeDestination(IncomeDestination destination, bool paidToACaseItem)
    {
        var destinationCaseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var otherDestination = paidToACaseItem ? null : OtherDestination;
        var caseJson = CaseJson(incomes: new JsonArray(
            ShareholdingIncomeJson(Guid.NewGuid(), destination, destinationCaseItemId, otherDestination)));

        var caseDetails = await GetCase(caseJson);

        var income = caseDetails.Incomes.Should().ContainSingle()
            .Which.Should().BeOfType<ShareholdingIncomeResourceRepresentation>().Subject;
        using var _ = new AssertionScope();
        income.Destination.Should().Be(destination);
        income.DestinationCaseItemId.Should().Be(destinationCaseItemId);
        income.OtherDestination.Should().Be(otherDestination);
    }

    [Theory]
    [InlineData(PaySurplusToType.ClientCashAccount, true)]
    [InlineData(PaySurplusToType.BankAccount, true)]
    [InlineData(PaySurplusToType.Other, false)]
    public async Task ShouldReadThePaySurplusType(PaySurplusToType type, bool paidToACaseItem)
    {
        var caseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var other = paidToACaseItem ? null : OtherDestination;
        var caseJson = CaseJson(expenses: new JsonArray(
            ShareSaleExpenseJson(Guid.NewGuid(), Guid.NewGuid(), type, caseItemId, other)));

        var caseDetails = await GetCase(caseJson);

        var paySurplus = caseDetails.Expenses.Should().ContainSingle()
            .Which.Settlements.Should().ContainSingle()
            .Which.ShareSettlement!.PaySurplus!;
        using var _ = new AssertionScope();
        paySurplus.Type.Should().Be(type);
        paySurplus.CaseItemId.Should().Be(caseItemId);
        paySurplus.Other.Should().Be(other);
    }

    [Fact]
    public async Task ShouldReadACaseWithEveryKindOfMoneyMovement()
    {
        var bankAccount = (BankAccountResourceRepresentation)_harness.CreateEstateItem(
            typeof(BankAccountResourceRepresentation));
        var shareholding = (PhysicalShareholdingResourceRepresentation)_harness.CreateEstateItem(
            typeof(PhysicalShareholdingResourceRepresentation));
        var clientCashAccountId = Guid.NewGuid();
        var caseJson = CaseJson(
            estateItems: new EstateItemResourceRepresentation[] { bankAccount, shareholding },
            incomes: new JsonArray(
                IncomeJson(bankAccount.Id, IncomeDestination.AssetValue, bankAccount.Id, null),
                IncomeJson(bankAccount.Id, IncomeDestination.ClientCashAccount, clientCashAccountId, null),
                ShareholdingIncomeJson(shareholding.Id, IncomeDestination.BankAccount, bankAccount.Id, null),
                ShareholdingIncomeJson(shareholding.Id, IncomeDestination.Other, null, OtherDestination)),
            expenses: new JsonArray(
                ShareSaleExpenseJson(shareholding.Id, shareholding.Id, PaySurplusToType.BankAccount,
                    bankAccount.Id, null)));

        var caseDetails = await GetCase(caseJson);

        using var _ = new AssertionScope();
        caseDetails.EstateItems.Should().HaveCount(2);
        caseDetails.Incomes.OfType<IncomeResourceRepresentation>()
            .Select(x => (x.Destination, x.DestinationCaseItemId))
            .Should().Equal(
                (IncomeDestination.AssetValue, bankAccount.Id),
                (IncomeDestination.ClientCashAccount, clientCashAccountId));
        caseDetails.Incomes.OfType<ShareholdingIncomeResourceRepresentation>()
            .Select(x => (x.Destination, x.DestinationCaseItemId, x.OtherDestination))
            .Should().Equal(
                (IncomeDestination.BankAccount, bankAccount.Id, null),
                (IncomeDestination.Other, null, OtherDestination));
        var settlement = caseDetails.Expenses.Single().Settlements.Single();
        settlement.CaseItemId.Should().Be(shareholding.Id);
        settlement.ShareSettlement!.PaySurplus!.Type.Should().Be(PaySurplusToType.BankAccount);
        settlement.ShareSettlement.PaySurplus.CaseItemId.Should().Be(bankAccount.Id);
    }

    private async Task<CaseResourceRepresentation> GetCase(JsonObject caseJson)
    {
        var caseId = caseJson["id"]!.GetValue<Guid>();
        _harness.ClientHandler.AddGetCaseResponse(caseId, caseJson.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseId);

        return caseDetails!;
    }

    private static JsonObject CaseJson(
        IEnumerable<EstateItemResourceRepresentation>? estateItems = null,
        JsonArray? incomes = null,
        JsonArray? expenses = null)
    {
        var builder = new CaseResourceRepresentationBuilder();
        foreach (var estateItem in estateItems ?? Enumerable.Empty<EstateItemResourceRepresentation>())
        {
            builder.With(estateItem);
        }

        var caseJson = CaseJsonBuilder.Build(builder.Build());
        caseJson["incomes"] = incomes ?? new JsonArray();
        caseJson["expenses"] = expenses ?? new JsonArray();
        return caseJson;
    }

    private static JsonObject IncomeJson(Guid estateItemId, IncomeDestination destination,
        Guid? destinationCaseItemId, string? otherDestination) =>
        new()
        {
            { "id", Guid.NewGuid() },
            { "createdAt", "2026-04-06T09:15:00Z" },
            { "updatedAt", "2026-04-06T09:15:00Z" },
            { "notes", null },
            { "value", 125.40m },
            { "isDeleted", false },
            { "type", "Income" },
            { "estateItemId", estateItemId },
            { "source", "Interest" },
            { "otherSource", null },
            { "from", "2025-04-06T00:00:00Z" },
            { "to", "2026-04-05T00:00:00Z" },
            { "amountType", "Gross" },
            { "destination", destination.ToString() },
            { "destinationCaseItemId", destinationCaseItemId },
            { "otherDestination", otherDestination },
            { "includesValueUpToDateOfDeath", false },
            { "valueIncludingUpToDateOfDeath", null },
        };

    private static JsonObject ShareholdingIncomeJson(Guid estateItemId, IncomeDestination destination,
        Guid? destinationCaseItemId, string? otherDestination) =>
        new()
        {
            { "id", Guid.NewGuid() },
            { "createdAt", "2026-04-06T09:15:00Z" },
            { "updatedAt", "2026-04-06T09:15:00Z" },
            { "notes", null },
            { "value", 340.00m },
            { "isDeleted", false },
            { "type", "ShareholdingIncome" },
            { "estateItemId", estateItemId },
            { "source", "Dividend" },
            { "otherSource", null },
            { "from", "2025-10-01T00:00:00Z" },
            { "to", "2026-03-31T00:00:00Z" },
            { "isTaxable", true },
            { "amountType", "Net" },
            { "destination", destination.ToString() },
            { "destinationCaseItemId", destinationCaseItemId },
            { "otherDestination", otherDestination },
            { "includesValueUpToDateOfDeath", false },
            { "valueIncludingUpToDateOfDeath", null },
            { "quantityOfShares", 1000m },
            { "sharePrice", 34m },
            { "cash", null },
            { "shareholderReferenceNumber", "C0012345678" },
        };

    private static JsonObject ShareSaleExpenseJson(Guid expenseCaseItemId, Guid settlementCaseItemId,
        PaySurplusToType surplusType, Guid? surplusCaseItemId, string? surplusOther) =>
        new()
        {
            { "id", Guid.NewGuid() },
            { "description", "Broker's commission" },
            { "caseItemId", expenseCaseItemId },
            { "value", 60m },
            { "from", "2026-05-01T00:00:00Z" },
            { "to", "2026-05-01T00:00:00Z" },
            { "supplier", "Broker" },
            { "paidByThirdParty", false },
            {
                "settlements", new JsonArray(new JsonObject
                {
                    { "id", Guid.NewGuid() },
                    { "value", 60m },
                    { "sourceOfFunds", "ProceedsOfSale" },
                    { "at", "2026-05-01T00:00:00Z" },
                    { "caseItemId", settlementCaseItemId },
                    { "personId", null },
                    { "generateIht423", false },
                    {
                        "shareSettlement", new JsonObject
                        {
                            { "quantity", 10 },
                            { "price", 1000m },
                            {
                                "paySurplus", new JsonObject
                                {
                                    { "type", surplusType.ToString() },
                                    { "caseItemId", surplusCaseItemId },
                                    { "other", surplusOther },
                                }
                            },
                        }
                    },
                })
            },
            { "notes", null },
            { "purpose", "AssetSaleOrLiabilityRepayment" },
            { "generateIht423", false },
            { "isComplete", true },
            { "createdAt", "2026-05-01T10:00:00Z" },
            { "updatedAt", "2026-05-01T10:00:00Z" },
        };
}
#pragma warning restore CS0618
