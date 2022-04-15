using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests;

public static class CaseJsonBuilder
{
    public static JsonObject Build(CaseResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();
        
        jsonObject.Add("id", resourceRepresentation.Id);
        jsonObject.Add("deceased", DeceasedJsonBuilder.Build(resourceRepresentation.Deceased));

        return jsonObject;
    }
}