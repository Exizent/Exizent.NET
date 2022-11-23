﻿using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class MoneyOwedEstateItemJsonBuilder : EstateItemJsonBuilder<MoneyOwedResourceRepresentation>
{
    public MoneyOwedEstateItemJsonBuilder(MoneyOwedResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        MoneyOwedResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.MoneyOwed));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("itemClassification", resourceRepresentation.ItemClassification.ToString("G"));
        jsonObject.Add("institution", resourceRepresentation.Institution);
        jsonObject.Add("itemDescription", resourceRepresentation.ItemDescription);
        jsonObject.Add("expectedValue", resourceRepresentation.ExpectedValue);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}