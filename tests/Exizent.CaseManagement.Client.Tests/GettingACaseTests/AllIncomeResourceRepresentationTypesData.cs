using System.Reflection;
using Exizent.CaseManagement.Client.Models.Incomes;
using Xunit.Sdk;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class AllIncomeResourceRepresentationTypesData : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { typeof(IncomeResourceRepresentation) };
        yield return new object[] { typeof(SumsReceivedIncomeResourceRepresentation) };
        yield return new object[] { typeof(ReceiptIncomeResourceRepresentation) };
    }
}