namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class CashIsaResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IRealisable
{
    public string? Institution { get; set; }
    public string? AccountNumber { get; set; }
    public string? SortCode { get; set; }
    public bool IsNationalSavingsAndInvestments { get; set; }
    public NationalSavingsAndInvestmentsIsaProduct? NationalSavingsAndInvestmentsProduct { get; set; }
    public decimal? EstimatedBalance { get; set; }
    public decimal? ConfirmedBalance { get; set; }
    public decimal? InterestUpToDateOfDeath { get; set; }
    public AddressResourceRepresentation Address { get; set; } = null!;
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}