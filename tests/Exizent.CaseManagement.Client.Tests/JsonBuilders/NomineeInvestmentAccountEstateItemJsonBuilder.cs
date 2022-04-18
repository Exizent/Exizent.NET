using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public class NomineeInvestmentAccountEstateItemJsonBuilder : EstateItemJsonBuilder<NomineeInvestmentAccountResourceRepresentation>
{
    public NomineeInvestmentAccountEstateItemJsonBuilder(NomineeInvestmentAccountResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    private static JsonObject BuildListedInvestmentResourceRepresentation(
        ListedInvestmentResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("identifier", resourceRepresentation.Identifier);
        jsonObject.Add("quantity", resourceRepresentation.Quantity);
        jsonObject.Add("shareDescription", resourceRepresentation.ShareDescription);
        jsonObject.Add("sharePrice", resourceRepresentation.SharePrice);
        jsonObject.Add("dividendDue", resourceRepresentation.DividendDue);
        return jsonObject;
    }
         
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        NomineeInvestmentAccountResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.NomineeInvestmentAccount));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("nomineeManager", resourceRepresentation.NomineeManager);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("listedInvestments",  new JsonArray(resourceRepresentation.ListedInvestments.Select(BuildListedInvestmentResourceRepresentation).ToArray<JsonNode>()));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("dividendDue", resourceRepresentation.DividendDue);
        jsonObject.Add("investmentValue", resourceRepresentation.InvestmentValue);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);

        return jsonObject;
    }
}