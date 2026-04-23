using Exizent.CaseManagement.Client.Models.Incomes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public static class IncomeJsonBuilderFactory
{
    public static IIncomeBaseJsonBuilder Create(IncomeBaseResourceRepresentation resourceRepresentation) =>
        resourceRepresentation switch
        {
            IncomeResourceRepresentation income => new IncomeJsonBuilder(income),
            SumsReceivedIncomeResourceRepresentation sumsReceived => new SumsReceivedIncomeJsonBuilder(sumsReceived),
            ReceiptIncomeResourceRepresentation receipt => new ReceiptIncomeJsonBuilder(receipt),
            ShareholdingIncomeResourceRepresentation shareholding => new ShareholdingIncomeJsonBuilder(shareholding),
            _ => throw new ArgumentException("This income resource representation is not supported", resourceRepresentation.GetType().Name)
        };
}