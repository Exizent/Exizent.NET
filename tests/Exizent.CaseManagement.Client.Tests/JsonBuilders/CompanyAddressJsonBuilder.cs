﻿using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Company;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class CompanyAddressJsonBuilder
{
    public static JsonObject Build(AddressResourceRepresentation address)
    {
        var jsonObject = new JsonObject
        {
            { "buildingNumber", address.BuildingNumber },
            { "buildingName", address.BuildingName },
            { "street", address.Street },
            { "city", address.City },
            { "postcode", address.Postcode }
        };

        return jsonObject;
    }
}