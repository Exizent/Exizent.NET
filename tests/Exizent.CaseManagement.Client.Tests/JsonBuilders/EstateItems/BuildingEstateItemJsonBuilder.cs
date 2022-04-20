using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class BuildingEstateItemJsonBuilder : EstateItemJsonBuilder<BuildingResourceRepresentation>
{
    public BuildingEstateItemJsonBuilder(BuildingResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        BuildingResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.Building));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("residenceType", resourceRepresentation.ResidenceType);
        jsonObject.Add("conveyancingDescription", resourceRepresentation.ConveyancingDescription);
        jsonObject.Add("isMainResidence", resourceRepresentation.IsMainResidence);
        jsonObject.Add("isVacant", resourceRepresentation.IsVacant);
        jsonObject.Add("isRented", resourceRepresentation.IsRented);
        jsonObject.Add("proprietorship", resourceRepresentation.Proprietorship.ToString("G"));
        jsonObject.Add("purpose", resourceRepresentation.Purpose.ToString("G"));
        jsonObject.Add("executorEstimatedValue", resourceRepresentation.ExecutorEstimatedValue);
        jsonObject.Add("zooplaEstimatedValue", resourceRepresentation.ZooplaEstimatedValue);
        jsonObject.Add("surveyorFormalValue", resourceRepresentation.SurveyorFormalValue);
        jsonObject.Add("formalValuationBy", resourceRepresentation.FormalValuationBy);
        jsonObject.Add("hasAdvisedInsurance", resourceRepresentation.HasAdvisedInsurance);
        jsonObject.Add("hasAdvisedUtilities", resourceRepresentation.HasAdvisedUtilities);
        jsonObject.Add("hasAdvisedCouncil", resourceRepresentation.HasAdvisedCouncil);
        jsonObject.Add("hasAdvisedCommsSuppliers", resourceRepresentation.HasAdvisedCommsSuppliers);
        jsonObject.Add("containsMoveableItems", resourceRepresentation.ContainsMoveableItems);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("grossSaleProceeds", resourceRepresentation.GrossSaleProceeds);
        jsonObject.Add("isFarmOrFarmhouse", resourceRepresentation.IsFarmOrFarmhouse);
        jsonObject.Add("isFreehold", resourceRepresentation.IsFreehold);
        jsonObject.Add("lengthOfLease", resourceRepresentation.LengthOfLease);
        jsonObject.Add("annualRent", resourceRepresentation.AnnualRent);
        jsonObject.Add("dateTenancyBegan", resourceRepresentation.DateTenancyBegan);
        jsonObject.Add("dateTenancyEnds", resourceRepresentation.DateTenancyEnds);
        jsonObject.Add("monthlyRent", resourceRepresentation.MonthlyRent);
        jsonObject.Add("agriculturalBusinessOrHeritageReliefExemption",
            resourceRepresentation.AgriculturalBusinessOrHeritageReliefExemption);
        jsonObject.Add("businessReliefValue", resourceRepresentation.BusinessReliefValue);
        jsonObject.Add("heritageExemptionValue", resourceRepresentation.HeritageExemptionValue);
        jsonObject.Add("agriculturalReliefValue", resourceRepresentation.AgriculturalReliefValue);
        jsonObject.Add("woodlandsReliefValue", resourceRepresentation.WoodlandsReliefValue);
        jsonObject.Add("isSubjectToSpecialFactors", resourceRepresentation.IsSubjectToSpecialFactors);
        jsonObject.Add("specialFactorsDescription", resourceRepresentation.SpecialFactorsDescription);
        jsonObject.Add("isCharityDonation", resourceRepresentation.IsCharityDonation);

        return jsonObject;
    }
}