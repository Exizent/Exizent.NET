using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class IncomeBondEstateItemJsonBuilder : EstateItemJsonBuilder<IncomeBondResourceRepresentation>
{
    public IncomeBondEstateItemJsonBuilder(IncomeBondResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
  
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        IncomeBondResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.IncomeBond));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("confirmedBalance", resourceRepresentation.ConfirmedBalance);
        jsonObject.Add("estimatedBalance", resourceRepresentation.EstimatedBalance);
        jsonObject.Add("interestUpToDateOfDeath", resourceRepresentation.InterestUpToDateOfDeath);
        jsonObject.Add("isNationalSavingsAndInvestments", resourceRepresentation.IsNationalSavingsAndInvestments);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}