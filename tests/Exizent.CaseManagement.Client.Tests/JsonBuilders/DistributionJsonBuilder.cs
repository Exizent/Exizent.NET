using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Distributions;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public class DistributionJsonBuilder
{
    public static JsonObject Build(DistributionResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject
        {
            { "id", resourceRepresentation.Id },
            {
                "caseItemIds", new JsonArray(
                    resourceRepresentation.CaseItemIds?.Select(x => JsonValue.Create(x)).ToArray<JsonNode?>() ??
                    Array.Empty<JsonNode>())
            },
            { "isDeleted", resourceRepresentation.IsDeleted },
            { "type", resourceRepresentation.Type.ToString() },
            { "interimPaymentValue", resourceRepresentation.InterimPaymentValue },
            {
                "distributionBeneficiaries", resourceRepresentation.DistributionBeneficiaries is null? null: DistributionBeneficiaryJsonBuilder.Build(resourceRepresentation.DistributionBeneficiaries)
            },
        };


        return jsonObject;
    }
    
}