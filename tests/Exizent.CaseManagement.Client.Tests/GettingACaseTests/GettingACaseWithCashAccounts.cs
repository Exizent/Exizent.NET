using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithCashAccounts : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithCashAccounts(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEveryCashAccount()
    {
        var defaultAccount = new CashAccountResourceRepresentation
        {
            Id = Guid.Parse("7F05AD59-A20F-4FAD-9716-2011E72774E2"),
            Name = "Client Account",
            IsDefault = true
        };
        var otherAccount = new CashAccountResourceRepresentation
        {
            Id = Guid.NewGuid(),
            Name = "Second account",
            IsDefault = false
        };
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(defaultAccount)
            .With(otherAccount)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.CashAccounts.Should().BeEquivalentTo(
            new[] { defaultAccount, otherAccount }, options => options.WithStrictOrdering());
    }

    [Fact]
    public async Task ShouldReturnNoCashAccountsWhenTheResponseHasNone()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.CashAccounts.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnNoCashAccountsWhenCashAccountsIsNull()
    {
        var caseDetails = await GetCaseWith("cashAccounts", null);

        caseDetails.CashAccounts.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldNotReadClientCashAccounts()
    {
        var account = new CashAccountResourceRepresentation { Id = Guid.NewGuid(), Name = "Client Account" };

        var caseDetails = await GetCaseWith("clientCashAccounts", new JsonArray(CashAccountJsonBuilder.Build(account)));

        caseDetails.CashAccounts.Should().BeEmpty();
    }

    private async Task<CaseResourceRepresentation> GetCaseWith(string propertyName, JsonNode? value)
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        body.Add(propertyName, value);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        caseDetails.Should().NotBeNull();
        return caseDetails!;
    }
}
