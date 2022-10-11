using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class AddressJsonBuilder
{
    public static JsonObject Build(AddressResourceRepresentation address)
    {
        var jsonObject = new JsonObject
        {
            { "buildingNumber", address.BuildingNumber },
            { "buildingNameOrFlatNumber", address.BuildingNameOrFlatNumber },
            { "streetName", address.StreetName },
            { "city", address.City },
            { "postcode", address.Postcode }
        };

        return jsonObject;
    }
}