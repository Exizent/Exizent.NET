using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Deceased;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class WillDetailsJsonBuilder
{
    public static JsonObject Build(WillDetailsResourceRepresentation willDetails)
    {
        var jsonObject = new JsonObject();
        
        jsonObject.Add("hasWill", willDetails.HasWill);
        jsonObject.Add("willDate", willDetails.WillDate);
        jsonObject.Add("originalWillLocation", willDetails.OriginalWillLocation);
        
        return jsonObject;
    }
}