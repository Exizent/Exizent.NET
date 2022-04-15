namespace Exizent.CaseManagement.Client.Models;

public class InvestmentBondResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? Provider { get; init; }
    public string? AccountNumber { get; init; }
    public bool HasChargeableEventOccurred { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal? DividendDue { get; init; }
    public decimal? InvestmentValue { get; init; }
    public bool IsValidForInheritanceTax { get; init; }
}