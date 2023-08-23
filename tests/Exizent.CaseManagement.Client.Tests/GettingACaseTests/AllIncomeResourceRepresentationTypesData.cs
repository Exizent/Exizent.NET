using System.Reflection;
using Exizent.CaseManagement.Client.Models.Incomes;
using Xunit.Sdk;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class AllIncomeResourceRepresentationTypesData : DataAttribute
{
    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        yield return new object[] { typeof(GetIncomeResourceRepresentation) };
        yield return new object[] { typeof(SumsReceivedGetIncomeResourceRepresentation) };
        yield return new object[] { typeof(ReceiptGetIncomeResourceRepresentation) };
    }
}