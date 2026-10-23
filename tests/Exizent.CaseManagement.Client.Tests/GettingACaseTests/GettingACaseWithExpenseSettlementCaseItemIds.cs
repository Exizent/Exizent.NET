using System.Text.Json.Nodes;
using AutoFixture;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// A settlement's case item is sent as caseItemId, and may also be sent under its older name, estateItemId.
/// </summary>
public sealed class GettingACaseWithExpenseSettlementCaseItemIds : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithExpenseSettlementCaseItemIds(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReadTheCaseItemIdWhenOnlyCaseItemIdIsSent()
    {
        var caseItemId = Guid.NewGuid();

        var settlement = await GetSettlementWith(("caseItemId", caseItemId));

        AssertCaseItemId(settlement, caseItemId);
    }

    [Fact]
    public async Task ShouldReadTheCaseItemIdFromEstateItemIdWhenOnlyEstateItemIdIsSent()
    {
        var estateItemId = Guid.NewGuid();

        var settlement = await GetSettlementWith(("estateItemId", estateItemId));

        AssertCaseItemId(settlement, estateItemId);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadTheCaseItemIdWhenBothAreSentWithTheSameValue(bool caseItemIdFirst)
    {
        var caseItemId = Guid.NewGuid();

        var settlement = await GetSettlementWith(InOrder(caseItemIdFirst, caseItemId, caseItemId));

        AssertCaseItemId(settlement, caseItemId);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldPreferCaseItemIdWhenBothAreSentWithDifferentValues(bool caseItemIdFirst)
    {
        var caseItemId = Guid.NewGuid();

        var settlement = await GetSettlementWith(InOrder(caseItemIdFirst, caseItemId, Guid.NewGuid()));

        AssertCaseItemId(settlement, caseItemId);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadTheCaseItemIdFromEstateItemIdWhenCaseItemIdIsNull(bool caseItemIdFirst)
    {
        var estateItemId = Guid.NewGuid();

        var settlement = await GetSettlementWith(InOrder(caseItemIdFirst, null, estateItemId));

        AssertCaseItemId(settlement, estateItemId);
    }

    [Fact]
    public async Task ShouldHaveNoCaseItemIdWhenNeitherIsSent()
    {
        var settlement = await GetSettlementWith();

        AssertCaseItemId(settlement, null);
    }

    [Fact]
    public async Task ShouldHaveNoCaseItemIdWhenBothAreNull()
    {
        var settlement = await GetSettlementWith(("caseItemId", null), ("estateItemId", null));

        AssertCaseItemId(settlement, null);
    }

    private static (string Name, Guid? Value)[] InOrder(bool caseItemIdFirst, Guid? caseItemId, Guid? estateItemId)
    {
        var caseItemIdProperty = ("caseItemId", caseItemId);
        var estateItemIdProperty = ("estateItemId", estateItemId);

        return caseItemIdFirst
            ? new[] { caseItemIdProperty, estateItemIdProperty }
            : new[] { estateItemIdProperty, caseItemIdProperty };
    }

    private async Task<ExpenseSettlementResourceRepresentation> GetSettlementWith(
        params (string Name, Guid? Value)[] idProperties)
    {
        var expense = _harness.Fixture.Build<ExpenseResourceRepresentation>()
            .With(x => x.Settlements,
                _harness.Fixture.CreateMany<ExpenseSettlementResourceRepresentation>(1).ToList())
            .Create();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expense)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        var settlementJson = body["expenses"]![0]!["settlements"]![0]!.AsObject();
        settlementJson.Remove("caseItemId");
        settlementJson.Remove("estateItemId");
        foreach (var (name, value) in idProperties)
        {
            settlementJson.Add(name, value);
        }

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        return caseDetails!.Expenses.Single().Settlements.Single();
    }

    private static void AssertCaseItemId(ExpenseSettlementResourceRepresentation settlement, Guid? expected)
    {
        using var _ = new AssertionScope();
        settlement.CaseItemId.Should().Be(expected);
#pragma warning disable CS0618
        settlement.EstateItemId.Should().Be(expected);
#pragma warning restore CS0618
    }
}
