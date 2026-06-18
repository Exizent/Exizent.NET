using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public static class ShareRealisationJsonBuilder
{
    public static JsonObject Build(GetShareRealisationResourceRepresentation r) =>
        new()
        {
            { "id", r.Id },
            { "receivedAt", r.ReceivedAt?.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture) },
            { "destination", r.Destination?.ToString() },
            { "otherDestination", r.OtherDestination },
            { "quantity", r.Quantity },
            { "price", r.Price },
            { "value", r.Value },
            { "includeDateOfDeathShares", r.IncludeDateOfDeathShares },
            { "incomeIds", new JsonArray(r.IncomeIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()) },
            { "caseItemId", r.CaseItemId },
            { "createdAt", r.CreatedAt },
            { "updatedAt", r.UpdatedAt }
        };
}
