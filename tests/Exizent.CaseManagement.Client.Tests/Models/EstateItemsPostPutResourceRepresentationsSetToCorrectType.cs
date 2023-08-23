using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.Models;

public class EstateItemsPostPutResourceRepresentationsSetToCorrectType
{
    [Fact]
    public void TestBankAccount()
    {
        EstateItemResourceRepresentationBase estateItem = new PostBankAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.BankAccount);
        estateItem = new PutBankAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.BankAccount);
    }
    
    [Fact]
    public void TestBuilding()
    {
        EstateItemResourceRepresentationBase estateItem = new PostBuildingResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Building);
        estateItem = new PutBuildingResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Building);
    }

        
    [Fact]
    public void TestBusinessInterest()
    {
        EstateItemResourceRepresentationBase estateItem = new PostBusinessInterestResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.BusinessInterest);
        estateItem = new PutBusinessInterestResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.BusinessInterest);
    }
        
    [Fact]
    public void TestCashIsa()
    {
        EstateItemResourceRepresentationBase estateItem = new PostCashIsaResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.CashIsa);
        estateItem = new PutCashIsaResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.CashIsa);
    }

    [Fact]
    public void TestCashSavingsAccount()
    {
        EstateItemResourceRepresentationBase estateItem = new PostCashSavingsAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.CashSavingsAccount);
        estateItem = new PutCashSavingsAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.CashSavingsAccount);
    }

    [Fact]
    public void TestCreditCardOrPersonalLoan()
    {
        EstateItemResourceRepresentationBase estateItem = new PostCreditCardOrPersonalLoanResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.CreditCardOrPersonalLoan);
        estateItem = new PutCreditCardOrPersonalLoanResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.CreditCardOrPersonalLoan);
    }

    [Fact]
    public void TestCryptocurrency()
    {
        EstateItemResourceRepresentationBase estateItem = new PostCryptocurrencyResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Cryptocurrency);
        estateItem = new PutCryptocurrencyResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Cryptocurrency);
    }

    [Fact]
    public void TestEndowmentPolicy()
    {
        EstateItemResourceRepresentationBase estateItem = new PostEndowmentPolicyResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.EndowmentPolicy);
        estateItem = new PutEndowmentPolicyResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.EndowmentPolicy);
    }

    [Fact]
    public void TestFinancialGift()
    {
        EstateItemResourceRepresentationBase estateItem = new PostFinancialGiftResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.FinancialGift);
        estateItem = new PutFinancialGiftResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.FinancialGift);
    }

    [Fact]
    public void TestIncomeBond()
    {
        EstateItemResourceRepresentationBase estateItem = new PostIncomeBondResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.IncomeBond);
        estateItem = new PutIncomeBondResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.IncomeBond);
    }

    [Fact]
    public void TestInvestmentBond()
    {
        EstateItemResourceRepresentationBase estateItem = new PostInvestmentBondResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.InvestmentBond);
        estateItem = new PutInvestmentBondResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.InvestmentBond);
    }

    [Fact]
    public void TestLand()
    {
        EstateItemResourceRepresentationBase estateItem = new PostLandResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Land);
        estateItem = new PutLandResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Land);
    }

    [Fact]
    public void TestLifePolicy()
    {
        EstateItemResourceRepresentationBase estateItem = new PostLifePolicyResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.LifePolicy);
        estateItem = new PutLifePolicyResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.LifePolicy);
    }

    [Fact]
    public void TestMiscellaneousAsset()
    {
        EstateItemResourceRepresentationBase estateItem = new PostMiscellaneousAssetResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.MiscellaneousAsset);
        estateItem = new PutMiscellaneousAssetResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.MiscellaneousAsset);
    }

    [Fact]
    public void TestMiscellaneousDebt()
    {
        EstateItemResourceRepresentationBase estateItem = new PostMiscellaneousDebtResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.MiscellaneousDebt);
        estateItem = new PutMiscellaneousDebtResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.MiscellaneousDebt);
    }

    [Fact]
    public void TestMoneyOwed()
    {
        EstateItemResourceRepresentationBase estateItem = new PostMoneyOwedResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.MoneyOwed);
        estateItem = new PutMoneyOwedResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.MoneyOwed);
    }
    
    [Fact]
    public void TestMortgage()
    {
        EstateItemResourceRepresentationBase estateItem = new PostMortgageResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Mortgage);
        estateItem = new PutMortgageResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Mortgage);
    }
    
    [Fact]
    public void TestNomineeInvestmentAccount()
    {
        EstateItemResourceRepresentationBase estateItem = new PostNomineeInvestmentAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.NomineeInvestmentAccount);
        estateItem = new PutNomineeInvestmentAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.NomineeInvestmentAccount);
    }
    
    [Fact]
    public void TestPension()
    {
        EstateItemResourceRepresentationBase estateItem = new PostPensionResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Pension);
        estateItem = new PutPensionResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Pension);
    }
    
    [Fact]
    public void TestPhysicalShareholding()
    {
        EstateItemResourceRepresentationBase estateItem = new PostPhysicalShareholdingResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.PhysicalShareholding);
        estateItem = new PutPhysicalShareholdingResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.PhysicalShareholding);
    }
    
    [Fact]
    public void TestPremiumBond()
    {
        EstateItemResourceRepresentationBase estateItem = new PostPremiumBondResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.PremiumBond);
        estateItem = new PutPremiumBondResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.PremiumBond);
    }
    
    [Fact]
    public void TestSecuredLoan()
    {
        EstateItemResourceRepresentationBase estateItem = new PostSecuredLoanResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.SecuredLoan);
        estateItem = new PutSecuredLoanResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.SecuredLoan);
    }
    
    [Fact]
    public void TestStateBenefitOverpayment()
    {
        EstateItemResourceRepresentationBase estateItem = new PostStateBenefitOverpaymentResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.StateBenefitOverpayment);
        estateItem = new PutStateBenefitOverpaymentResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.StateBenefitOverpayment);
    }
    
    [Fact]
    public void TestStoreCardOrCatalogueAccount()
    {
        EstateItemResourceRepresentationBase estateItem = new PostStoreCardOrCatalogueAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.StoreCardOrCatalogueAccount);
        estateItem = new PutStoreCardOrCatalogueAccountResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.StoreCardOrCatalogueAccount);
    }
    
    [Fact]
    public void TestUkGovAndMunicipalSecurities()
    {
        EstateItemResourceRepresentationBase estateItem = new PostUKGovAndMunicipalSecuritiesResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.UkGovAndMunicipalSecurities);
        estateItem = new PutUKGovAndMunicipalSecuritiesResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.UkGovAndMunicipalSecurities);
    }
    
    [Fact]
    public void TestUnitTrust()
    {
        EstateItemResourceRepresentationBase estateItem = new PostUnitTrustResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.UnitTrust);
        estateItem = new PutUnitTrustResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.UnitTrust);
    }
    
    [Fact]
    public void TestVehicle()
    {
        EstateItemResourceRepresentationBase estateItem = new PostVehicleResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Vehicle);
        estateItem = new PutVehicleResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.Vehicle);
    }
    
    [Fact]
    public void TestVehicleFinance()
    {
        EstateItemResourceRepresentationBase estateItem = new PostVehicleFinanceResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.VehicleFinance);
        estateItem = new PutVehicleFinanceResourceRepresentation();
        estateItem.Type.Should().Be(EstateItemType.VehicleFinance);
    }
}