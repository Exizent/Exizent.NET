using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.EstateItemValuations;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public static class EstateItemValuationSummaryJsonBuilder
{
    public static JsonObject Build(EstateItemValuationSummaryResourceRepresentation resourceRepresentation) =>
        new()
        {
            { "id", resourceRepresentation.Id },
            { "provider", resourceRepresentation.Provider.ToString("G") },
            { "status", resourceRepresentation.Status.ToString("G") },
            { "valuedAt", resourceRepresentation.ValuedAt },
            { "value", resourceRepresentation.Value },
            { "confidence", resourceRepresentation.Confidence?.ToString("G") },
            { "failureReason", resourceRepresentation.FailureReason }
        };
}
