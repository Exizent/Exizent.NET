using Exizent.CaseManagement.Client.Models.Incomes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public static class IncomeJsonBuilderFactory
{
    public static IIncomeBaseJsonBuilder Create(GetIncomeBaseResourceRepresentation resourceRepresentation) =>
        resourceRepresentation switch
        {
            GetIncomeResourceRepresentation income => new IncomeJsonBuilder(income),
            SumsReceivedGetIncomeResourceRepresentation sumsReceived => new SumsReceivedIncomeJsonBuilder(sumsReceived),
            ReceiptGetIncomeResourceRepresentation receipt => new ReceiptIncomeJsonBuilder(receipt),
            _ => throw new ArgumentException("This income resource representation is not supported", resourceRepresentation.GetType().Name)
        };
}