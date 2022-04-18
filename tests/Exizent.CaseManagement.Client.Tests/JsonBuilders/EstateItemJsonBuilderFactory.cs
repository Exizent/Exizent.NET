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
            _ => throw new NotImplementedException()
        };
}