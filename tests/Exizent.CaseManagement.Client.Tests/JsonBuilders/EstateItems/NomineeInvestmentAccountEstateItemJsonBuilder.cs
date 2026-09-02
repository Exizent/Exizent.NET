using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class NomineeInvestmentAccountEstateItemJsonBuilder : EstateItemJsonBuilder<NomineeInvestmentAccountResourceRepresentation>
{
    public NomineeInvestmentAccountEstateItemJsonBuilder(NomineeInvestmentAccountResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    private static JsonObject BuildInvestmentCategoryResourceRepresentation(
        InvestmentCategoryResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject
        {
            { "id", resourceRepresentation.Id },
            { "category", resourceRepresentation.Category.ToString() },
            { "investments",  new JsonArray(resourceRepresentation.Investments.Select(BuildListedInvestmentResourceRepresentation).ToArray<JsonNode>()) }
        };

        return jsonObject;
    }
         
    private static JsonObject BuildListedInvestmentResourceRepresentation(
        ListedInvestmentResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject
        {
            { "identifier", resourceRepresentation.Identifier },
            { "quantity", resourceRepresentation.Quantity },
            { "shareDescription", resourceRepresentation.ShareDescription },
            { "sharePrice", resourceRepresentation.SharePrice },
            { "dividendDue", resourceRepresentation.DividendDue }
        };

        return jsonObject;
    }
         
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        NomineeInvestmentAccountResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.NomineeInvestmentAccount));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("nomineeManager", resourceRepresentation.NomineeManager);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("accountType", resourceRepresentation.AccountType);
        jsonObject.Add("listedInvestments",  new JsonArray(resourceRepresentation.ListedInvestments.Select(BuildListedInvestmentResourceRepresentation).ToArray<JsonNode>()));
        jsonObject.Add("investmentCategories",  new JsonArray(resourceRepresentation.InvestmentCategories.Select(BuildInvestmentCategoryResourceRepresentation).ToArray<JsonNode>()));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("dividendDue", resourceRepresentation.DividendDue);
        jsonObject.Add("investmentValue", resourceRepresentation.InvestmentValue);
        jsonObject.Add("cash", resourceRepresentation.Cash);
        jsonObject.Add("valuationBy", resourceRepresentation.ValuationBy);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));
        jsonObject.Add("hadControlOfTheCompany", resourceRepresentation.HadControlOfTheCompany);

        return jsonObject;
    }
}