namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class CashSavingsAccountResourceRepresentation : EstateItemResourceRepresentation
{
    public string? Institution { get; init; }
    public string? AccountNumber { get; init; }
    public string? SortCode { get; init; }
    public bool IsFixedTerm { get; init; }
    public bool IsNationalSavingsAndInvestments { get; init; }
    public NationalSavingsAndInvestmentsSavingsProduct? NationalSavingsAndInvestmentsProduct { get; init; }
    public decimal? EstimatedBalance { get; init; }
    public decimal? ConfirmedBalance { get; init; }
    public decimal? InterestUpToDateOfDeath { get; init; }
    public AddressResourceRepresentation InstitutionAddress { get; init; } = null!;
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal ProportionOwned { get; init; }
}