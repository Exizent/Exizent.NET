using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public static class EstateItemRealisationJsonBuilder
{
    public static JsonObject Build(EstateItemRealisationResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("receivedAt", resourceRepresentation.ReceivedAt);
        jsonObject.Add("value", resourceRepresentation.Value);
        jsonObject.Add("destination", resourceRepresentation.Destination.ToString());
        jsonObject.Add("otherDestination", resourceRepresentation.OtherDestination);

        return jsonObject;
    }
}