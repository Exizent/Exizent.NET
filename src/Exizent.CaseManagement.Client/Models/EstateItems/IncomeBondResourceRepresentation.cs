using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.IncomeBond))]
public class IncomeBondResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? AccountNumber { get; init; }
    public decimal? ConfirmedBalance { get; init; }
    public decimal? EstimatedBalance { get; init; }
    public decimal? InterestUpToDateOfDeath { get; init; }
    public bool IsNationalSavingsAndInvestments { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
}