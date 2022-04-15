using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class CaseJsonBuilder
{
    public static JsonObject Build(CaseResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();
        
        jsonObject.Add("id", resourceRepresentation.Id);
        jsonObject.Add("deceased", DeceasedJsonBuilder.Build(resourceRepresentation.Deceased));
        jsonObject.Add("people",   new JsonArray(resourceRepresentation.People.Select(PersonJsonBuilder.Build).ToArray<JsonNode?>()));
        jsonObject.Add("estateItems",   new JsonArray(resourceRepresentation.EstateItems.Select(x => EstateItemJsonBuilderFactory.Create(x).Build()).ToArray<JsonNode?>()));

        return jsonObject;
    }
}