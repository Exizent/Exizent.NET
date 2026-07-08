using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class LifePolicyEstateItemJsonBuilder : EstateItemJsonBuilder<LifePolicyResourceRepresentation>
{
    public LifePolicyEstateItemJsonBuilder(LifePolicyResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
    
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        LifePolicyResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.LifePolicy));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("policyNumber", resourceRepresentation.PolicyNumber);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("sumAssured", resourceRepresentation.SumAssured);
        jsonObject.Add("paysOnDeathOfDeceased", resourceRepresentation.PaysOnDeathOfDeceased);
        jsonObject.Add("beneficiaryDetails", resourceRepresentation.BeneficiaryDetails);
        jsonObject.Add("comments", resourceRepresentation.Comments);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("policyType", (int)resourceRepresentation.PolicyType);
        jsonObject.Add("isJointlyOwned", resourceRepresentation.IsJointlyOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("nameOfLifeAssured", resourceRepresentation.NameOfLifeAssured);
        jsonObject.Add("didPaymentsContinueAfterDeath", resourceRepresentation.DidPaymentsContinueAfterDeath);
        jsonObject.Add("lumpSumDisposalDetails", resourceRepresentation.LumpSumDisposalDetails);
        jsonObject.Add("repaymentFrequency", resourceRepresentation.RepaymentFrequency);
        jsonObject.Add("guaranteedPaymentIncreaseDetails", resourceRepresentation.GuaranteedPaymentIncreaseDetails);
        jsonObject.Add("finalGuaranteedPaymentDate", resourceRepresentation.FinalGuaranteedPaymentDate?.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));

        return jsonObject;
    }
}