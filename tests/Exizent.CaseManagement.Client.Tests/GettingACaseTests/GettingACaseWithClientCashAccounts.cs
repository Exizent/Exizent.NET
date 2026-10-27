using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithClientCashAccounts : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithClientCashAccounts(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEveryClientCashAccount()
    {
        var defaultAccount = new ClientCashAccountResourceRepresentation
        {
            Id = Guid.Parse("7F05AD59-A20F-4FAD-9716-2011E72774E2"),
            Name = "Main Cash Account",
            IsDefault = true
        };
        var otherAccount = new ClientCashAccountResourceRepresentation
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
        caseDetails!.ClientCashAccounts.Should().BeEquivalentTo(
            new[] { defaultAccount, otherAccount }, options => options.WithStrictOrdering());
    }

    [Fact]
    public async Task ShouldReturnNoClientCashAccountsWhenTheResponseHasNone()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.ClientCashAccounts.Should().BeEmpty();
    }
}
