using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// A case's cash accounts are sent as cashAccounts, and may also be sent under their older name, clientCashAccounts.
/// </summary>
public sealed class GettingACaseWithCashAccounts : IClassFixture<Harness>
{
    private static readonly CashAccountResourceRepresentation DefaultAccount = new()
    {
        Id = Guid.Parse("7F05AD59-A20F-4FAD-9716-2011E72774E2"),
        Name = "Client Account",
        IsDefault = true
    };

    private static readonly CashAccountResourceRepresentation OtherAccount = new()
    {
        Id = Guid.NewGuid(),
        Name = "Second account",
        IsDefault = false
    };

    private readonly Harness _harness;

    public GettingACaseWithCashAccounts(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEveryCashAccountWhenOnlyCashAccountsIsSent()
    {
        var caseDetails = await GetCaseWith(("cashAccounts", new[] { DefaultAccount, OtherAccount }));

        AssertCashAccounts(caseDetails, DefaultAccount, OtherAccount);
    }

    [Fact]
    public async Task ShouldReturnEveryCashAccountWhenOnlyClientCashAccountsIsSent()
    {
        var caseDetails = await GetCaseWith(("clientCashAccounts", new[] { DefaultAccount, OtherAccount }));

        AssertCashAccounts(caseDetails, DefaultAccount, OtherAccount);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReturnEveryCashAccountWhenBothAreSent(bool cashAccountsFirst)
    {
        var accounts = new[] { DefaultAccount, OtherAccount };

        var caseDetails = await GetCaseWith(InOrder(cashAccountsFirst, accounts, accounts));

        AssertCashAccounts(caseDetails, DefaultAccount, OtherAccount);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadEachPropertyFromItsOwnNameWhenBothAreSentWithDifferentAccounts(bool cashAccountsFirst)
    {
        var caseDetails = await GetCaseWith(InOrder(cashAccountsFirst, new[] { DefaultAccount, OtherAccount },
            new[] { DefaultAccount }));

        using var _ = new AssertionScope();
        caseDetails.CashAccounts.Should().BeEquivalentTo(
            new[] { DefaultAccount, OtherAccount }, options => options.WithStrictOrdering());
#pragma warning disable CS0618
        caseDetails.ClientCashAccounts.Should().BeEquivalentTo(new[] { DefaultAccount });
#pragma warning restore CS0618
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ShouldReadClientCashAccountsWhenCashAccountsIsNull(bool cashAccountsFirst)
    {
        var caseDetails = await GetCaseWith(InOrder(cashAccountsFirst, null, new[] { DefaultAccount, OtherAccount }));

        AssertCashAccounts(caseDetails, DefaultAccount, OtherAccount);
    }

    [Fact]
    public async Task ShouldReturnNoCashAccountsWhenNeitherIsSent()
    {
        var caseDetails = await GetCaseWith();

        AssertCashAccounts(caseDetails);
    }

    [Fact]
    public async Task ShouldReturnNoCashAccountsWhenBothAreNull()
    {
        var caseDetails = await GetCaseWith(("cashAccounts", null), ("clientCashAccounts", null));

        AssertCashAccounts(caseDetails);
    }

    private static (string Name, CashAccountResourceRepresentation[]? Accounts)[] InOrder(bool cashAccountsFirst,
        CashAccountResourceRepresentation[]? cashAccounts, CashAccountResourceRepresentation[]? clientCashAccounts)
    {
        var cashAccountsProperty = ("cashAccounts", cashAccounts);
        var clientCashAccountsProperty = ("clientCashAccounts", clientCashAccounts);

        return cashAccountsFirst
            ? new[] { cashAccountsProperty, clientCashAccountsProperty }
            : new[] { clientCashAccountsProperty, cashAccountsProperty };
    }

    private async Task<CaseResourceRepresentation> GetCaseWith(
        params (string Name, CashAccountResourceRepresentation[]? Accounts)[] accountProperties)
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        foreach (var (name, accounts) in accountProperties)
        {
            body.Add(name, accounts is null
                ? null
                : new JsonArray(accounts.Select(CashAccountJsonBuilder.Build).ToArray<JsonNode?>()));
        }

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        caseDetails.Should().NotBeNull();
        return caseDetails!;
    }

    private static void AssertCashAccounts(CaseResourceRepresentation caseDetails,
        params CashAccountResourceRepresentation[] expected)
    {
        using var _ = new AssertionScope();
        caseDetails.CashAccounts.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
#pragma warning disable CS0618
        caseDetails.ClientCashAccounts.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
#pragma warning restore CS0618
    }
}
