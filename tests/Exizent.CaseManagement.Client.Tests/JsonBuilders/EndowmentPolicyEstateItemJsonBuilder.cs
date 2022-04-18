using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public class EndowmentPolicyEstateItemJsonBuilder : EstateItemJsonBuilder<EndowmentPolicyResourceRepresentation>
{
    public EndowmentPolicyEstateItemJsonBuilder(EndowmentPolicyResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
 
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        EndowmentPolicyResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.EndowmentPolicy));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("provider", resourceRepresentation.Provider);
        jsonObject.Add("policyNumber", resourceRepresentation.PolicyNumber);
        jsonObject.Add("policyType", resourceRepresentation.PolicyType.ToString("G"));
        jsonObject.Add("sumAssured", resourceRepresentation.SumAssured);
        jsonObject.Add("bonusDue", resourceRepresentation.BonusDue);
        jsonObject.Add("paysOnDeathOfDeceased", resourceRepresentation.PaysOnDeathOfDeceased);
        jsonObject.Add("comments", resourceRepresentation.Comments);

        return jsonObject;
    }
}