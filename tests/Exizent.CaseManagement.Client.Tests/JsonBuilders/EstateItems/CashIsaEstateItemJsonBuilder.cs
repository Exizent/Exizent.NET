using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class CashIsaEstateItemJsonBuilder : EstateItemJsonBuilder<CashIsaResourceRepresentation>
{
    public CashIsaEstateItemJsonBuilder(CashIsaResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        CashIsaResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.CashIsa));
        jsonObject.Add("institution", resourceRepresentation.Institution);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("sortCode", resourceRepresentation.SortCode);
        jsonObject.Add("isNationalSavingsAndInvestments", resourceRepresentation.IsNationalSavingsAndInvestments);
        jsonObject.Add("nationalSavingsAndInvestmentsProduct", resourceRepresentation.NationalSavingsAndInvestmentsProduct?.ToString());
        jsonObject.Add("estimatedBalance", resourceRepresentation.EstimatedBalance);
        jsonObject.Add("confirmedBalance", resourceRepresentation.ConfirmedBalance);
        jsonObject.Add("interestUpToDateOfDeath", resourceRepresentation.InterestUpToDateOfDeath);
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("realisation", resourceRepresentation.Realisation is null ? null : EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));

        return jsonObject;
    }
}