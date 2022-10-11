using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Deceased;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class WillDetailsJsonBuilder
{
    public static JsonObject Build(WillDetailsResourceRepresentation willDetails)
    {
        var jsonObject = new JsonObject
        {
            { "hasWill", willDetails.HasWill },
            { "willDate", willDetails.WillDate },
            { "originalWillLocation", willDetails.OriginalWillLocation }
        };

        return jsonObject;
    }
}