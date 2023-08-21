namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class MiscellaneousAssetResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IStandardJointOwnership, IRealisable, IHeritable, ICanBeValidForIht
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public MiscellaneousAssetClassification ItemClassification { get; set; }
    public string ItemDescription { get; set; } = null!;
    public decimal? ExecutorEstimatedValue { get; set; }
    public decimal? FormalValuation { get; set; }
    public string? FormalValuationBy { get; set; }
    public bool IsHeritable { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public bool? IsCharityDonation { get; set; }
    public decimal? GrossSaleProceeds { get; set; }
    public DateTime? DateOfSale { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}