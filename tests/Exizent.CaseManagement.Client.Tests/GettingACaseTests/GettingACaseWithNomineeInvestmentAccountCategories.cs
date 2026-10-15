using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithNomineeInvestmentAccountCategories : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithNomineeInvestmentAccountCategories(Harness harness) => _harness = harness;

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    [InlineData(null)]
    public async Task ShouldDeserialiseIsListedOnUkExchanges(bool? isListedOnUkExchanges)
    {
        var account = new NomineeInvestmentAccountResourceRepresentation
        {
            Id = Guid.NewGuid(),
            Address = new AddressResourceRepresentation(),
            Realisation = new EstateItemRealisationResourceRepresentation
            {
                Destination = RealisationDestination.BankAccount
            },
            InvestmentCategories = new[]
            {
                new InvestmentCategoryResourceRepresentation
                {
                    Id = Guid.NewGuid(),
                    Category = InvestmentType.GeneralInvestmentAccount,
                    IsListedOnRecognisedStockExchange = true,
                    IsListedOnUkExchanges = isListedOnUkExchanges
                }
            }
        };

        var caseDetails = await GetCaseWith(account);

        var actual = caseDetails.EstateItems.Single().Should()
            .BeOfType<NomineeInvestmentAccountResourceRepresentation>().Subject;
        actual.InvestmentCategories.Single().IsListedOnUkExchanges.Should().Be(isListedOnUkExchanges);
    }

    private async Task<CaseResourceRepresentation> GetCaseWith(
        NomineeInvestmentAccountResourceRepresentation account)
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(account)
            .Build();

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id,
            CaseJsonBuilder.Build(caseResourceRepresentation).ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        caseDetails.Should().NotBeNull();
        return caseDetails!;
    }
}
