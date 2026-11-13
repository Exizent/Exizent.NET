using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// Where money is paid is identified by a case item id, or by free text when it is not paid to a case item. The API
/// also returns a <c>destination</c> (or, on a share sale surplus, a <c>type</c>) name alongside them. The client does
/// not model those names, so any value - including ones added to the API after this client was released - is ignored
/// rather than failing the whole case.
/// </summary>
public sealed class GettingACaseWithMoneyDestinations : IClassFixture<Harness>
{
    private const string OtherDestination = "Paid directly to the beneficiary";

    private readonly Harness _harness;

    public GettingACaseWithMoneyDestinations(Harness harness) => _harness = harness;

    [Theory]
    [InlineData("ClientCashAccount", true)]
    [InlineData("AssetValue", true)]
    [InlineData("BankAccount", true)]
    [InlineData("Contact", true)]
    [InlineData("SomethingNew", true)]
    [InlineData("Other", false)]
    public async Task ShouldReadWhereAnIncomeWasPaid(string destination, bool paidToACaseItem)
    {
        var destinationCaseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var otherDestination = paidToACaseItem ? null : OtherDestination;
        var caseJson = CaseJson(incomes: new JsonArray(
            IncomeJson(Guid.NewGuid(), destination, destinationCaseItemId, otherDestination)));

        var caseDetails = await GetCase(caseJson);

        var income = caseDetails.Incomes.Should().ContainSingle()
            .Which.Should().BeOfType<IncomeResourceRepresentation>().Subject;
        using var _ = new AssertionScope();
        income.DestinationCaseItemId.Should().Be(destinationCaseItemId);
        income.OtherDestination.Should().Be(otherDestination);
    }

    [Theory]
    [InlineData("ClientCashAccount", true)]
    [InlineData("AssetValue", true)]
    [InlineData("BankAccount", true)]
    [InlineData("Contact", true)]
    [InlineData("SomethingNew", true)]
    [InlineData("Other", false)]
    public async Task ShouldReadWhereAShareholdingIncomeWasPaid(string destination, bool paidToACaseItem)
    {
        var destinationCaseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var otherDestination = paidToACaseItem ? null : OtherDestination;
        var caseJson = CaseJson(incomes: new JsonArray(
            ShareholdingIncomeJson(Guid.NewGuid(), destination, destinationCaseItemId, otherDestination)));

        var caseDetails = await GetCase(caseJson);

        var income = caseDetails.Incomes.Should().ContainSingle()
            .Which.Should().BeOfType<ShareholdingIncomeResourceRepresentation>().Subject;
        using var _ = new AssertionScope();
        income.DestinationCaseItemId.Should().Be(destinationCaseItemId);
        income.OtherDestination.Should().Be(otherDestination);
    }

    [Theory]
    [InlineData("ClientCashAccount", true)]
    [InlineData("AssetValue", true)]
    [InlineData("BankAccount", true)]
    [InlineData("Contact", true)]
    [InlineData("SomethingNew", true)]
    [InlineData("Other", false)]
    public async Task ShouldReadWhereANomineeInvestmentAccountIncomeWasPaid(string destination, bool paidToACaseItem)
    {
        var destinationCaseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var otherDestination = paidToACaseItem ? null : OtherDestination;
        var caseJson = CaseJson(incomes: new JsonArray(
            NomineeInvestmentAccountIncomeJson(Guid.NewGuid(), destination, destinationCaseItemId, otherDestination)));

        var caseDetails = await GetCase(caseJson);

        var income = caseDetails.Incomes.Should().ContainSingle()
            .Which.Should().BeOfType<NomineeInvestmentAccountIncomeResourceRepresentation>().Subject;
        using var _ = new AssertionScope();
        income.DestinationCaseItemId.Should().Be(destinationCaseItemId);
        income.OtherDestination.Should().Be(otherDestination);
        income.NiaDestination.Should().Be(NiaDestination.CashUninvested);
    }

    [Theory]
    [InlineData("Asset", true)]
    [InlineData("ClientCashAccount", true)]
    [InlineData("BankAccount", true)]
    [InlineData("Contact", true)]
    [InlineData("SomethingNew", true)]
    [InlineData("Other", false)]
    public async Task ShouldReadWhereAShareSaleSurplusWasPaid(string type, bool paidToACaseItem)
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
        paySurplus.CaseItemId.Should().Be(caseItemId);
        paySurplus.Other.Should().Be(other);
    }

    [Theory]
    [InlineData("ClientCashAccount", true)]
    [InlineData("BankAccount", true)]
    [InlineData("Contact", true)]
    [InlineData("SomethingNew", true)]
    [InlineData("Other", false)]
    public async Task ShouldReadWhereAnEstateItemRealisationWasPaid(string destination, bool paidToACaseItem)
    {
        var caseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var otherDestination = paidToACaseItem ? null : OtherDestination;
        var bankAccount = _harness.CreateEstateItem(typeof(BankAccountResourceRepresentation));
        var caseJson = CaseJson(estateItems: new[] { bankAccount });
        caseJson["estateItems"]![0]!["realisation"] = new JsonObject
        {
            { "receivedAt", "2026-05-01T00:00:00" },
            { "value", 1500.75m },
            { "destination", destination },
            { "otherDestination", otherDestination },
            { "caseItemId", caseItemId },
        };

        var caseDetails = await GetCase(caseJson);

        var realisation = caseDetails.EstateItems.Should().ContainSingle()
            .Which.Should().BeOfType<BankAccountResourceRepresentation>().Subject.Realisation;
        using var _ = new AssertionScope();
        realisation.Value.Should().Be(1500.75m);
        realisation.CaseItemId.Should().Be(caseItemId);
        realisation.OtherDestination.Should().Be(otherDestination);
    }

    [Theory]
    [InlineData("ClientCashAccount", true)]
    [InlineData("BankAccount", true)]
    [InlineData("Contact", true)]
    [InlineData("SomethingNew", true)]
    [InlineData("Other", false)]
    public async Task ShouldReadWhereAShareRealisationWasPaid(string destination, bool paidToACaseItem)
    {
        var caseItemId = paidToACaseItem ? Guid.NewGuid() : (Guid?)null;
        var otherDestination = paidToACaseItem ? null : OtherDestination;
        var realisationId = Guid.NewGuid();
        var shareholding = _harness.CreateEstateItem(typeof(PhysicalShareholdingResourceRepresentation));
        var caseJson = CaseJson(estateItems: new[] { shareholding });
        caseJson["estateItems"]![0]!["realisations"] = new JsonArray(new JsonObject
        {
            { "id", realisationId },
            { "receivedAt", "2026-05-01" },
            { "destination", destination },
            { "otherDestination", otherDestination },
            { "quantity", 100m },
            { "price", 250m },
            { "value", 250m },
            { "includeDateOfDeathShares", true },
            { "incomeIds", new JsonArray() },
            { "caseItemId", caseItemId },
            { "createdAt", "2026-05-01T10:00:00Z" },
            { "updatedAt", "2026-05-01T10:00:00Z" },
        });

        var caseDetails = await GetCase(caseJson);

        var realisation = caseDetails.EstateItems.Should().ContainSingle()
            .Which.Should().BeOfType<PhysicalShareholdingResourceRepresentation>().Subject
            .Realisations.Should().ContainSingle().Subject;
        using var _ = new AssertionScope();
        realisation.Id.Should().Be(realisationId);
        realisation.CaseItemId.Should().Be(caseItemId);
        realisation.OtherDestination.Should().Be(otherDestination);
    }

    [Fact]
    public async Task ShouldReadACaseWithEveryKindOfMoneyMovement()
    {
        var bankAccount = (BankAccountResourceRepresentation)_harness.CreateEstateItem(
            typeof(BankAccountResourceRepresentation));
        var shareholding = (PhysicalShareholdingResourceRepresentation)_harness.CreateEstateItem(
            typeof(PhysicalShareholdingResourceRepresentation));
        var clientCashAccountId = Guid.NewGuid();
        var contactId = Guid.NewGuid();
        var caseJson = CaseJson(
            estateItems: new EstateItemResourceRepresentation[] { bankAccount, shareholding },
            incomes: new JsonArray(
                IncomeJson(bankAccount.Id, "AssetValue", bankAccount.Id, null),
                IncomeJson(bankAccount.Id, "ClientCashAccount", clientCashAccountId, null),
                ShareholdingIncomeJson(shareholding.Id, "Contact", contactId, null),
                ShareholdingIncomeJson(shareholding.Id, "Other", null, OtherDestination)),
            expenses: new JsonArray(
                ShareSaleExpenseJson(shareholding.Id, shareholding.Id, "Contact", contactId, null)));

        var caseDetails = await GetCase(caseJson);

        using var _ = new AssertionScope();
        caseDetails.EstateItems.Should().HaveCount(2);
        caseDetails.Incomes.OfType<IncomeResourceRepresentation>()
            .Select(x => x.DestinationCaseItemId)
            .Should().Equal(bankAccount.Id, clientCashAccountId);
        caseDetails.Incomes.OfType<ShareholdingIncomeResourceRepresentation>()
            .Select(x => (x.DestinationCaseItemId, x.OtherDestination))
            .Should().Equal(
                (contactId, null),
                (null, OtherDestination));
        var settlement = caseDetails.Expenses.Single().Settlements.Single();
        settlement.CaseItemId.Should().Be(shareholding.Id);
        settlement.ShareSettlement!.PaySurplus!.CaseItemId.Should().Be(contactId);
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

    private static JsonObject IncomeJson(Guid estateItemId, string destination,
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
            { "destination", destination },
            { "destinationCaseItemId", destinationCaseItemId },
            { "otherDestination", otherDestination },
            { "includesValueUpToDateOfDeath", false },
            { "valueIncludingUpToDateOfDeath", null },
        };

    private static JsonObject ShareholdingIncomeJson(Guid estateItemId, string destination,
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
            { "destination", destination },
            { "destinationCaseItemId", destinationCaseItemId },
            { "otherDestination", otherDestination },
            { "includesValueUpToDateOfDeath", false },
            { "valueIncludingUpToDateOfDeath", null },
            { "quantityOfShares", 1000m },
            { "sharePrice", 34m },
            { "cash", null },
            { "shareholderReferenceNumber", "C0012345678" },
        };

    private static JsonObject NomineeInvestmentAccountIncomeJson(Guid estateItemId, string destination,
        Guid? destinationCaseItemId, string? otherDestination) =>
        new()
        {
            { "id", Guid.NewGuid() },
            { "createdAt", "2026-04-06T09:15:00Z" },
            { "updatedAt", "2026-04-06T09:15:00Z" },
            { "notes", null },
            { "value", 82.10m },
            { "isDeleted", false },
            { "type", "NomineeInvestmentAccountIncome" },
            { "estateItemId", estateItemId },
            { "source", "Interest" },
            { "otherSource", null },
            { "from", "2025-04-06T00:00:00Z" },
            { "to", "2026-04-05T00:00:00Z" },
            { "isTaxable", true },
            { "amountType", "Gross" },
            { "destination", destination },
            { "destinationCaseItemId", destinationCaseItemId },
            { "otherDestination", otherDestination },
            { "includesValueUpToDateOfDeath", false },
            { "valueIncludingUpToDateOfDeath", null },
            { "origin", "CashUninvested" },
            { "listedInvestmentId", null },
            { "niaDestination", "CashUninvested" },
        };

    private static JsonObject ShareSaleExpenseJson(Guid expenseCaseItemId, Guid settlementCaseItemId,
        string surplusType, Guid? surplusCaseItemId, string? surplusOther) =>
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
                                    { "type", surplusType },
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
