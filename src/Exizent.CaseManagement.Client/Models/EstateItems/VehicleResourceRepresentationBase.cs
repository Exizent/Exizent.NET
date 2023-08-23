namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class VehicleResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, IRealisable
{
    protected VehicleResourceRepresentationBase(): base(EstateItemType.Vehicle){}

    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public string? Manufacturer { get; set; }
    public string? Model { get; set; }
    public string? RegistrationPlate { get; set; }
    public decimal? ExecutorEstimatedValue { get; set; }
    public decimal? UkVehicleDataEstimatedValue { get; set; }
    public decimal? FormalValuation { get; set; }
    public string? FormalValuationBy { get; set; }
    public bool HasPrivatePlate { get; set; }
    public bool HasAdvisedInsurance { get; set; }
    public bool HasFinancing { get; set; }
    public bool HasFinanceProviderBeenAdvised { get; set; }
    public bool IsCharityDonation { get; set; }
    public VehicleCondition? ConditionAtDateOfDeath { get; set; }
    public int? Mileage { get; set; }
    public int? YearOfManufacture { get; set; }
    public decimal? GrossSaleProceeds { get; set; }
    public DateTime? DateOfSale { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}
