namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class PensionResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? Provider { get; init; }
    public string? PlanReference { get; init; }
    public bool? HasValidNominationForm { get; init; }
    public decimal? DeathBenefitValuePayable { get; init; }
    public string? BeneficiaryDetails { get; init; }
    public bool? IsValidForInheritanceTax { get; init; }
}