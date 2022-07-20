using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class SecuredLoanEstateItemJsonBuilder : EstateItemJsonBuilder<SecuredLoanResourceRepresentation>
{
    public SecuredLoanEstateItemJsonBuilder(SecuredLoanResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
        
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        SecuredLoanResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.SecuredLoan));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("hasProviderBeenAdvised", resourceRepresentation.HasProviderBeenAdvised);
        jsonObject.Add("linkedEstateItemId", resourceRepresentation.LinkedEstateItemId);
        jsonObject.Add("debtValue", resourceRepresentation.DebtValue);
        jsonObject.Add("settlement", EstateItemSettlementJsonBuilder.Build(resourceRepresentation.Settlement));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}