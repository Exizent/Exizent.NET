using System.Text.Json.Nodes;
using AutoFixture;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// A settlement's case item is read from caseItemId only; the retired estateItemId name is ignored.
/// </summary>
public sealed class GettingACaseWithExpenseSettlementCaseItemIds : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithExpenseSettlementCaseItemIds(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReadTheCaseItemIdWhenCaseItemIdIsSent()
    {
        var caseItemId = Guid.NewGuid();

        var settlement = await GetSettlementWith(("caseItemId", caseItemId));

        settlement.CaseItemId.Should().Be(caseItemId);
    }

    [Fact]
    public async Task ShouldHaveNoCaseItemIdWhenCaseItemIdIsNotSent()
    {
        var settlement = await GetSettlementWith();

        settlement.CaseItemId.Should().BeNull();
    }

    [Fact]
    public async Task ShouldHaveNoCaseItemIdWhenCaseItemIdIsNull()
    {
        var settlement = await GetSettlementWith(("caseItemId", null));

        settlement.CaseItemId.Should().BeNull();
    }

    [Fact]
    public async Task ShouldIgnoreEstateItemIdWhenOnlyEstateItemIdIsSent()
    {
        var settlement = await GetSettlementWith(("estateItemId", Guid.NewGuid()));

        settlement.CaseItemId.Should().BeNull();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadTheCaseItemIdAndIgnoreEstateItemIdWhenBothAreSent(bool caseItemIdFirst)
    {
        var caseItemId = Guid.NewGuid();
        var caseItemIdProperty = ("caseItemId", (Guid?)caseItemId);
        var estateItemIdProperty = ("estateItemId", (Guid?)Guid.NewGuid());

        var settlement = await GetSettlementWith(caseItemIdFirst
            ? new[] { caseItemIdProperty, estateItemIdProperty }
            : new[] { estateItemIdProperty, caseItemIdProperty });

        settlement.CaseItemId.Should().Be(caseItemId);
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
        foreach (var (name, value) in idProperties)
        {
            settlementJson.Add(name, value);
        }

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        return caseDetails!.Expenses.Single().Settlements.Single();
    }
}
