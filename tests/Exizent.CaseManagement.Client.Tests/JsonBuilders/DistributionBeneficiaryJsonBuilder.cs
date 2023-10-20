using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Distributions;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class DistributionBeneficiaryJsonBuilder
{
    public static JsonObject Build(DistributionBeneficiaryResourceRepresentation beneficiary)
    {
        var jsonObject = new JsonObject
        {
            { "beneficiaryId", beneficiary.BeneficiaryId },
            { "descriptionOfTrustOrContingency", beneficiary.DescriptionOfTrustOrContingency },
            { "leadTrusteePersonId", beneficiary.LeadTrusteePersonId },
        };

        return jsonObject;
    }
}