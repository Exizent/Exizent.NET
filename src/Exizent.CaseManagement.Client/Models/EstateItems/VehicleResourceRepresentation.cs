using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.Vehicle))]
public class VehicleResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public string? Manufacturer { get; init; }
    public string? Model { get; init; }
    public string? EngineSize { get; init; }
    public string? RegistrationPlate { get; init; }
    public decimal? ExecutorEstimatedValue { get; init; }
    public decimal? UkVehicleDataEstimatedValue { get; init; }
    public decimal? FormalValuation { get; init; }
    public string? FormalValuationBy { get; init; }
    public bool HasPrivatePlate { get; init; }
    public bool HasAdvisedInsurance { get; init; }
    public bool HasFinancing { get; init; }
    public bool HasFinanceProviderBeenAdvised { get; init; }
    public bool IsCharityDonation { get; init; }
    public VehicleCondition? ConditionAtDateOfDeath { get; init; }
    public int? Mileage { get; init; }
    public int? YearOfManufacture { get; init; }
    public decimal? GrossSaleProceeds { get; init; }
    public DateTime? DateOfSale { get; init; }
    public EstateItemRealisationResourceRepresentation Realisation { get; init; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; init; } = Array.Empty<Guid>();
}
