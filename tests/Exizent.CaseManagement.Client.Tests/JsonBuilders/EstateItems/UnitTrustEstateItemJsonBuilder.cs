using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class UnitTrustEstateItemJsonBuilder : EstateItemJsonBuilder<UnitTrustResourceRepresentation>
{
    public UnitTrustEstateItemJsonBuilder(UnitTrustResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
            
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        UnitTrustResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.UnitTrust));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("fundManager", resourceRepresentation.FundManager);
        jsonObject.Add("unitsHeld", resourceRepresentation.UnitsHeld);
        jsonObject.Add("unitPrice", resourceRepresentation.UnitPrice);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("dividendDue", resourceRepresentation.DividendDue);
        jsonObject.Add("investmentValue", resourceRepresentation.InvestmentValue);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}