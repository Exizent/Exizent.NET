using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Distributions;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class DistributionBeneficiaryJsonBuilder
{
    public static JsonArray Build(IReadOnlyList<DistributionBeneficiaryResourceRepresentation> beneficiaries)
    {
        var jsonArray = new JsonArray();
        foreach (var b in beneficiaries)
        {
            jsonArray.Add(new JsonObject
            {
                { "beneficiaryId", b.BeneficiaryId },
                { "descriptionOfTrustOrContingency", b.DescriptionOfTrustOrContingency },
                { "leadTrusteePersonId", b.LeadTrusteePersonId },
            });
        }

        return jsonArray;
    }
}