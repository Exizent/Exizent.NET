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
            {"destination", resourceRepresentation.Destination.ToString()},
            {"otherDestination", resourceRepresentation.OtherDestination},
            {"estateItemId", resourceRepresentation.EstateItemId}
        };
}