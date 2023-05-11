using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class PensionEstateItemJsonBuilder : EstateItemJsonBuilder<PensionResourceRepresentation>
{
    public PensionEstateItemJsonBuilder(PensionResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
     
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        PensionResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.Pension));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("planReference", resourceRepresentation.PlanReference);
        jsonObject.Add("pensionType", resourceRepresentation.PensionType);
        jsonObject.Add("hasValidNominationForm", resourceRepresentation.HasValidNominationForm);
        jsonObject.Add("deathBenefitValuePayable", resourceRepresentation.DeathBenefitValuePayable);
        jsonObject.Add("beneficiaryDetails", resourceRepresentation.BeneficiaryDetails);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
         
        return jsonObject;
    }
}