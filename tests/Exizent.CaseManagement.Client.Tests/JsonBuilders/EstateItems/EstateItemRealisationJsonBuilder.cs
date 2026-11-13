using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public static class EstateItemRealisationJsonBuilder
{
    public static JsonObject Build(EstateItemRealisationResourceRepresentation resourceRepresentation) =>
        new()
        {
            {"receivedAt", resourceRepresentation.ReceivedAt},
            {"value", resourceRepresentation.Value},
            {"otherDestination", resourceRepresentation.OtherDestination},
            {"caseItemId", resourceRepresentation.CaseItemId}
        };
}