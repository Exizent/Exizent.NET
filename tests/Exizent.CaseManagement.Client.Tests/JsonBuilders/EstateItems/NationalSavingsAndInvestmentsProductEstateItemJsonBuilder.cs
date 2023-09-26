using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class NationalSavingsAndInvestmentsProductEstateItemJsonBuilder : EstateItemJsonBuilder<NationalSavingsAndInvestmentsProductResourceRepresentation>
{
    public NationalSavingsAndInvestmentsProductEstateItemJsonBuilder(NationalSavingsAndInvestmentsProductResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
  
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        NationalSavingsAndInvestmentsProductResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.NationalSavingsAndInvestmentsProduct));
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("productType", (int)resourceRepresentation.ProductType);
        jsonObject.Add("confirmedBalance", resourceRepresentation.ConfirmedBalance);
        jsonObject.Add("estimatedBalance", resourceRepresentation.EstimatedBalance);
        jsonObject.Add("interestUpToDateOfDeath", resourceRepresentation.InterestUpToDateOfDeath);
        jsonObject.Add("indexLinkedIncrease", resourceRepresentation.IndexLinkedIncrease);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));
        
        if (resourceRepresentation.Address is not null) jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));

        return jsonObject;
    }
}