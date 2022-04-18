using System.Reflection;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Xunit.Sdk;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class AllEstateItemResourceRepresentationTypesData : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { typeof(BankAccountResourceRepresentation) };
        yield return new object[] { typeof(BuildingResourceRepresentation) };
        yield return new object[] { typeof(BusinessInterestResourceRepresentation) };
        yield return new object[] { typeof(CashIsaResourceRepresentation) };
        yield return new object[] { typeof(CashSavingsAccountResourceRepresentation) };
        yield return new object[] { typeof(CreditCardOrPersonalLoanResourceRepresentation) };
        yield return new object[] { typeof(CryptocurrencyResourceRepresentation) };
        yield return new object[] { typeof(EndowmentPolicyResourceRepresentation) };
        yield return new object[] { typeof(IncomeBondResourceRepresentation) };
        yield return new object[] { typeof(InvestmentBondResourceRepresentation) };
        yield return new object[] { typeof(LandResourceRepresentation) };
        yield return new object[] { typeof(LifePolicyResourceRepresentation) };
        yield return new object[] { typeof(MiscellaneousAssetResourceRepresentation) };
        yield return new object[] { typeof(MiscellaneousDebtResourceRepresentation) };
    }
}