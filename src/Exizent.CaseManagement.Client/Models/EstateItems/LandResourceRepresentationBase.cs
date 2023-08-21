namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class LandResourceRepresentationBase : EstateItemResourceRepresentationBase, ICanBeValidForIht, IHeritable, IRealisable, IHasJointOwners
{
    public string LandType { get; set; } = null!;
    public string LandRegistryNumber { get; set; } = null!;
    public string ConveyancingDescription { get; set; } = null!;
    public bool IsMainResidence { get; set; }
    public bool IsVacant { get; set; }
    public bool IsRented { get; set; }
    public PropertyPurpose Purpose { get; set; }
    public PropertyProprietorship Proprietorship { get; set; }
    public decimal? ExecutorEstimatedValue { get; set; }
    public decimal? SurveyorFormalValue { get; set; }
    public string? FormalValuationBy { get; set; }
    public bool HasAdvisedInsurance { get; set; }
    public bool HasAdvisedCouncil { get; set; }
    public decimal ProportionOwned { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public decimal? GrossSaleProceeds { get; set; }
    public bool IsFarmOrFarmhouse { get; set; }
    public bool IsFreehold { get; set; }
    public int? LengthOfLease { get; set; }
    public decimal? AnnualRent { get; set; }
    public DateTime? DateTenancyBegan { get; set; }
    public DateTime? DateTenancyEnds { get; set; }
    public decimal? MonthlyRent { get; set; }
    public bool AgriculturalBusinessOrHeritageReliefExemption { get; set; }
    public decimal? BusinessReliefValue { get; set; }
    public decimal? HeritageExemptionValue { get; set; }
    public decimal? AgriculturalReliefValue { get; set; }
    public decimal? WoodlandsReliefValue { get; set; }
    public bool IsSubjectToSpecialFactors { get; set; }
    public string? SpecialFactorsDescription { get; set; }
    public bool IsCharityDonation { get; set; }
    public bool IsClaimingResidenceNilRateBand { get; set; }
    public bool IsHeritable { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}
