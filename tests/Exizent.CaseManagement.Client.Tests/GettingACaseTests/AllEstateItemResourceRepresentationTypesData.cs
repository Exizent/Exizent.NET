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
    }
}