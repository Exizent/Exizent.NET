using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class EstateItemJsonBuilderFactory
{
    public static IEstateItemJsonBuilder Create(EstateItemResourceRepresentation resourceRepresentation) =>
        resourceRepresentation switch
        {
            BankAccountResourceRepresentation bankAccount => new BankAccountEstateItemJsonBuilder(bankAccount),
            BuildingResourceRepresentation building => new BuildingEstateItemJsonBuilder(building),
            BusinessInterestResourceRepresentation businessInterest => new BusinessInterestEstateItemJsonBuilder(businessInterest),
            CashIsaResourceRepresentation cashIsa => new CashIsaEstateItemJsonBuilder(cashIsa),
            CashSavingsAccountResourceRepresentation cashSavingsAccount => new CashSavingsAccountEstateItemJsonBuilder(cashSavingsAccount),
            CreditCardOrPersonalLoanResourceRepresentation creditCardOrPersonalLoan => new CreditCardOrPersonalLoanEstateItemJsonBuilder(creditCardOrPersonalLoan),
            CryptocurrencyResourceRepresentation cryptocurrency => new CryptocurrencyEstateItemJsonBuilder(cryptocurrency),
            EndowmentPolicyResourceRepresentation endowmentPolicy => new EndowmentPolicyEstateItemJsonBuilder(endowmentPolicy),
            IncomeBondResourceRepresentation incomeBond => new IncomeBondEstateItemJsonBuilder(incomeBond),
            InvestmentBondResourceRepresentation investmentBond => new InvestmentBondEstateItemJsonBuilder(investmentBond),
            LandResourceRepresentation land => new LandEstateItemJsonBuilder(land),
            LifePolicyResourceRepresentation lifePolicy => new LifePolicyEstateItemJsonBuilder(lifePolicy),
            MiscellaneousAssetResourceRepresentation miscellaneousAsset => new MiscellaneousAssetEstateItemJsonBuilder(miscellaneousAsset),
            MiscellaneousDebtResourceRepresentation miscellaneousDebt => new MiscellaneousDebtEstateItemJsonBuilder(miscellaneousDebt),
            MortgageResourceRepresentation mortgage => new MortgageEstateItemJsonBuilder(mortgage),
            NomineeInvestmentAccountResourceRepresentation nomineeInvestmentAccount => new NomineeInvestmentAccountEstateItemJsonBuilder(nomineeInvestmentAccount),
            PensionResourceRepresentation pension => new PensionEstateItemJsonBuilder(pension),
            PhysicalShareholdingResourceRepresentation physicalShareholding => new PhysicalShareholdingEstateItemJsonBuilder(physicalShareholding),
            PremiumBondResourceRepresentation premiumBond => new PremiumBondEstateItemJsonBuilder(premiumBond),
            SecuredLoanResourceRepresentation securedLoan => new SecuredLoanEstateItemJsonBuilder(securedLoan),
            _ => throw new NotImplementedException()
        };
}