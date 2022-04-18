using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

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

        return jsonObject;
    }
}

public class CashSavingsAccountEstateItemJsonBuilder : EstateItemJsonBuilder<CashSavingsAccountResourceRepresentation>
 {
     public CashSavingsAccountEstateItemJsonBuilder(CashSavingsAccountResourceRepresentation resourceRepresentation)
         : base(resourceRepresentation)
     {
     }
 
     protected override JsonObject InnerBuild(JsonObject jsonObject,
         CashSavingsAccountResourceRepresentation resourceRepresentation)
     {
         jsonObject.Add("type", nameof(EstateItemType.CashSavingsAccount));
         jsonObject.Add("institution", resourceRepresentation.Institution);
         jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
         jsonObject.Add("sortCode", resourceRepresentation.SortCode);
         jsonObject.Add("isFixedTerm", resourceRepresentation.IsFixedTerm);
         jsonObject.Add("isNationalSavingsAndInvestments", resourceRepresentation.IsNationalSavingsAndInvestments);
         jsonObject.Add("nationalSavingsAndInvestmentsProduct", resourceRepresentation.NationalSavingsAndInvestmentsProduct?.ToString());
         jsonObject.Add("estimatedBalance", resourceRepresentation.EstimatedBalance);
         jsonObject.Add("confirmedBalance", resourceRepresentation.ConfirmedBalance);
         jsonObject.Add("interestUpToDateOfDeath", resourceRepresentation.InterestUpToDateOfDeath);
         jsonObject.Add("institutionAddress", AddressJsonBuilder.Build(resourceRepresentation.InstitutionAddress));
         jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
         jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
         jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
 
         return jsonObject;
     }
 }