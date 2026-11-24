using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// A dividend or interest on a nominee investment account received as shares. The JSON here is shaped as the Cases API
/// sends it, including the fields the client does not model (<c>destination</c>, <c>linkedTo</c>). Before the client
/// knew this income type, a case holding one could not be read at all.
/// </summary>
public sealed class GettingACaseWithNomineeInvestmentAccountShareIncomes : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithNomineeInvestmentAccountShareIncomes(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReadAShareIncomeAsTheApiSendsIt()
    {
        var id = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        var listedInvestmentId = Guid.NewGuid();
        var destinationCaseItemId = Guid.NewGuid();
        var caseJson = CaseJson(new JsonArray(
            ShareIncomeJson(id, estateItemId, listedInvestmentId, destinationCaseItemId)));

        var caseDetails = await GetCase(caseJson);

        var income = caseDetails.Incomes.Should().ContainSingle()
            .Which.Should().BeOfType<NomineeInvestmentAccountShareIncomeResourceRepresentation>().Subject;
        using var _ = new AssertionScope();
        income.Id.Should().Be(id);
        income.Value.Should().Be(340.00m);
        income.Notes.Should().Be("Scrip dividend");
        income.CreatedAt.Should().Be(new DateTime(2026, 4, 6, 9, 15, 0, DateTimeKind.Utc));
        income.UpdatedAt.Should().Be(new DateTime(2026, 4, 7, 10, 30, 0, DateTimeKind.Utc));
        income.IsDeleted.Should().BeFalse();
        income.EstateItemId.Should().Be(estateItemId);
        income.Source.Should().Be(IncomeSource.Dividend);
        income.OtherSource.Should().BeNull();
        income.From.Should().Be(new DateTime(2025, 10, 1, 0, 0, 0, DateTimeKind.Utc));
        income.To.Should().Be(new DateTime(2026, 3, 31, 0, 0, 0, DateTimeKind.Utc));
        income.IsTaxable.Should().BeTrue();
        income.AmountType.Should().Be(AmountType.Net);
        income.DestinationCaseItemId.Should().Be(destinationCaseItemId);
        income.OtherDestination.Should().BeNull();
        income.IncludesValueUpToDateOfDeath.Should().BeTrue();
        income.ValueIncludingUpToDateOfDeath.Should().Be(340.00m);
        income.ListedInvestmentId.Should().Be(listedInvestmentId);
        income.QuantityOfShares.Should().Be(1000m);
        income.SharePrice.Should().Be(34m);
    }

    [Fact]
    public async Task ShouldReadAShareIncomeWithNoListedInvestment()
    {
        var caseJson = CaseJson(new JsonArray(
            ShareIncomeJson(Guid.NewGuid(), Guid.NewGuid(), listedInvestmentId: null, Guid.NewGuid())));

        var caseDetails = await GetCase(caseJson);

        caseDetails.Incomes.Should().ContainSingle()
            .Which.Should().BeOfType<NomineeInvestmentAccountShareIncomeResourceRepresentation>()
            .Which.ListedInvestmentId.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReadAShareIncomeAlongsideTheOtherIncomeTypes()
    {
        var estateItemId = Guid.NewGuid();
        var caseJson = CaseJson(new JsonArray(
            NomineeInvestmentAccountIncomeJson(estateItemId),
            ShareIncomeJson(Guid.NewGuid(), estateItemId, Guid.NewGuid(), Guid.NewGuid()),
            IncomeJson()));

        var caseDetails = await GetCase(caseJson);

        caseDetails.Incomes.Select(x => x.GetType()).Should().Equal(
            typeof(NomineeInvestmentAccountIncomeResourceRepresentation),
            typeof(NomineeInvestmentAccountShareIncomeResourceRepresentation),
            typeof(IncomeResourceRepresentation));
    }

    private async Task<CaseResourceRepresentation> GetCase(JsonObject caseJson)
    {
        var caseId = caseJson["id"]!.GetValue<Guid>();
        _harness.ClientHandler.AddGetCaseResponse(caseId, caseJson.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseId);

        return caseDetails!;
    }

    private static JsonObject CaseJson(JsonArray incomes)
    {
        var caseJson = CaseJsonBuilder.Build(new CaseResourceRepresentationBuilder().Build());
        caseJson["incomes"] = incomes;
        return caseJson;
    }

    private static JsonObject ShareIncomeJson(Guid id, Guid estateItemId, Guid? listedInvestmentId,
        Guid destinationCaseItemId) =>
        new()
        {
            { "type", "NomineeInvestmentAccountShareIncome" },
            { "id", id },
            { "value", 340.00m },
            { "notes", "Scrip dividend" },
            { "createdAt", "2026-04-06T09:15:00Z" },
            { "updatedAt", "2026-04-07T10:30:00Z" },
            { "isDeleted", false },
            { "linkedTo", new JsonArray() },
            { "estateItemId", estateItemId },
            { "source", "Dividend" },
            { "otherSource", null },
            { "from", "2025-10-01T00:00:00Z" },
            { "to", "2026-03-31T00:00:00Z" },
            { "isTaxable", true },
            { "amountType", "Net" },
            { "destination", "AssetValue" },
            { "otherDestination", null },
            { "destinationCaseItemId", destinationCaseItemId },
            { "includesValueUpToDateOfDeath", true },
            { "valueIncludingUpToDateOfDeath", 340.00m },
            { "listedInvestmentId", listedInvestmentId },
            { "quantityOfShares", 1000m },
            { "sharePrice", 34m },
        };

    private static JsonObject NomineeInvestmentAccountIncomeJson(Guid estateItemId) =>
        new()
        {
            { "type", "NomineeInvestmentAccountIncome" },
            { "id", Guid.NewGuid() },
            { "value", 82.10m },
            { "notes", null },
            { "createdAt", "2026-04-06T09:15:00Z" },
            { "updatedAt", "2026-04-06T09:15:00Z" },
            { "isDeleted", false },
            { "linkedTo", new JsonArray() },
            { "estateItemId", estateItemId },
            { "source", "Interest" },
            { "otherSource", null },
            { "from", "2025-04-06T00:00:00Z" },
            { "to", "2026-04-05T00:00:00Z" },
            { "isTaxable", true },
            { "amountType", "Gross" },
            { "destination", "ClientCashAccount" },
            { "otherDestination", null },
            { "destinationCaseItemId", Guid.NewGuid() },
            { "includesValueUpToDateOfDeath", false },
            { "valueIncludingUpToDateOfDeath", null },
            { "origin", "CashUninvested" },
            { "listedInvestmentId", null },
            { "niaDestination", "CashUninvested" },
        };

    private static JsonObject IncomeJson() =>
        new()
        {
            { "type", "Income" },
            { "id", Guid.NewGuid() },
            { "value", 12.50m },
            { "notes", null },
            { "createdAt", "2026-04-06T09:15:00Z" },
            { "updatedAt", "2026-04-06T09:15:00Z" },
            { "isDeleted", false },
            { "linkedTo", new JsonArray() },
            { "estateItemId", null },
            { "source", "Rent" },
            { "otherSource", null },
            { "from", "2025-04-06T00:00:00Z" },
            { "to", "2026-04-05T00:00:00Z" },
            { "isTaxable", true },
            { "amountType", "Gross" },
            { "destination", "Other" },
            { "otherDestination", "Paid to the tenant's agent" },
            { "destinationCaseItemId", null },
            { "includesValueUpToDateOfDeath", false },
            { "valueIncludingUpToDateOfDeath", null },
        };
}
