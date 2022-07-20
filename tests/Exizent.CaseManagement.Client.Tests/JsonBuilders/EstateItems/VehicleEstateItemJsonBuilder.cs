using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class VehicleEstateItemJsonBuilder : EstateItemJsonBuilder<VehicleResourceRepresentation>
{
    public VehicleEstateItemJsonBuilder(VehicleResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
              
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        VehicleResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.Vehicle));
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("manufacturer", resourceRepresentation.Manufacturer);
        jsonObject.Add("model", resourceRepresentation.Model);
        jsonObject.Add("registrationPlate", resourceRepresentation.RegistrationPlate);
        jsonObject.Add("executorEstimatedValue", resourceRepresentation.ExecutorEstimatedValue);
        jsonObject.Add("ukVehicleDataEstimatedValue", resourceRepresentation.UkVehicleDataEstimatedValue);
        jsonObject.Add("formalValuation", resourceRepresentation.FormalValuation);
        jsonObject.Add("formalValuationBy", resourceRepresentation.FormalValuationBy);
        jsonObject.Add("hasPrivatePlate", resourceRepresentation.HasPrivatePlate);
        jsonObject.Add("hasAdvisedInsurance", resourceRepresentation.HasAdvisedInsurance);
        jsonObject.Add("hasFinancing", resourceRepresentation.HasFinancing);
        jsonObject.Add("hasFinanceProviderBeenAdvised", resourceRepresentation.HasFinanceProviderBeenAdvised);
        jsonObject.Add("isCharityDonation", resourceRepresentation.IsCharityDonation);
        jsonObject.Add("conditionAtDateOfDeath", resourceRepresentation.ConditionAtDateOfDeath?.ToString());
        jsonObject.Add("mileage", resourceRepresentation.Mileage);
        jsonObject.Add("yearOfManufacture", resourceRepresentation.YearOfManufacture);
        jsonObject.Add("grossSaleProceeds", resourceRepresentation.GrossSaleProceeds);
        jsonObject.Add("dateOfSale", resourceRepresentation.DateOfSale);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}