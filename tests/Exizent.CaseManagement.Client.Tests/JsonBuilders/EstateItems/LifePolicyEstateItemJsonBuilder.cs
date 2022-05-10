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
        jsonObject.Add("realisation", resourceRepresentation.Realisation is null ? null : EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));

        return jsonObject;
    }
}