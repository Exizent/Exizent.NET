using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class BusinessInterestEstateItemJsonBuilder : EstateItemJsonBuilder<BusinessInterestResourceRepresentation>
{
    public BusinessInterestEstateItemJsonBuilder(BusinessInterestResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        BusinessInterestResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.BusinessInterest));
        jsonObject.Add("businessName", resourceRepresentation.BusinessName);
        jsonObject.Add("executorEstimatedValue", resourceRepresentation.ExecutorEstimatedValue);
        jsonObject.Add("surveyorFormalValue", resourceRepresentation.SurveyorFormalValue);
        jsonObject.Add("formalValuationBy", resourceRepresentation.FormalValuationBy);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("isJointlyOwned", resourceRepresentation.IsJointlyOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("isHeritable", resourceRepresentation.IsHeritable);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("grossSaleProceeds", resourceRepresentation.GrossSaleProceeds);
        jsonObject.Add("realisation", resourceRepresentation.Realisation is null ? null : EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));

        return jsonObject;
    }
}