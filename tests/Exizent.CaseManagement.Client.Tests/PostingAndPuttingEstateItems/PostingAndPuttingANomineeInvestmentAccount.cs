using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

public sealed class PostingAndPuttingANomineeInvestmentAccount : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public PostingAndPuttingANomineeInvestmentAccount(Harness harness) => _harness = harness;

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public async Task ShouldPostIsListedOnUkExchangesOnEachInvestmentCategory(bool? isListedOnUkExchanges)
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId,
            Populate(new PostNomineeInvestmentAccountResourceRepresentation(), isListedOnUkExchanges));

        AssertIsListedOnUkExchangesSent(isListedOnUkExchanges);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public async Task ShouldPutIsListedOnUkExchangesOnEachInvestmentCategory(bool? isListedOnUkExchanges)
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId,
            Populate(new PutNomineeInvestmentAccountResourceRepresentation(), isListedOnUkExchanges));

        AssertIsListedOnUkExchangesSent(isListedOnUkExchanges);
    }

    private static T Populate<T>(T account, bool? isListedOnUkExchanges)
        where T : NomineeInvestmentAccountResourceRepresentationBase
    {
        account.Location = Location.Elsewhere;
        account.Address = new AddressResourceRepresentation();
        account.Realisation = new EstateItemRealisationResourceRepresentation();
        account.InvestmentCategories = new[]
        {
            new InvestmentCategoryResourceRepresentation
            {
                Id = Guid.NewGuid(),
                Category = InvestmentType.GeneralInvestmentAccount,
                IsListedOnRecognisedStockExchange = true,
                IsListedOnUkExchanges = isListedOnUkExchanges
            }
        };

        return account;
    }

    private void AssertIsListedOnUkExchangesSent(bool? expected)
    {
        var category = JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!["investmentCategories"]![0]!
            .AsObject();

        category.ContainsKey("isListedOnUkExchanges").Should().BeTrue();
        ((bool?)category["isListedOnUkExchanges"]).Should().Be(expected);
    }
}
