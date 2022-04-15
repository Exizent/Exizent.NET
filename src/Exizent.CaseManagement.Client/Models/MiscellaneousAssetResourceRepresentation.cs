namespace Exizent.CaseManagement.Client.Models;

public class MiscellaneousAssetResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public MiscellaneousAssetClassification ItemClassification { get; init; }
    public string ItemDescription { get; init; } = null!;
    public decimal? ExecutorEstimatedValue { get; init; }
    public decimal? FormalValuation { get; init; }
    public string? FormalValuationBy { get; init; }
    public bool IsHeritable { get; init; }
    public bool IsValidForInheritanceTax { get; init; }
    public bool? IsCharityDonation { get; init; }
    public decimal? GrossSaleProceeds { get; init; }
}