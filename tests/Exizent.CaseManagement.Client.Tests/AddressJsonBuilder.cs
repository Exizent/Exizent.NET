using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests;

public static class AddressJsonBuilder
{
    public static JsonObject Build(AddressResourceRepresentation address)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("buildingNumber", address.BuildingNumber);
        jsonObject.Add("buildingNameOrFlatNumber", address.BuildingNameOrFlatNumber);
        jsonObject.Add("streetName", address.StreetName);
        jsonObject.Add("city", address.City);
        jsonObject.Add("postcode", address.Postcode);

        return jsonObject;
    }
}