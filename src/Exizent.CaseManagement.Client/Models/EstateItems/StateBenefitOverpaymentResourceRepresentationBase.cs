namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class StateBenefitOverpaymentResourceRepresentationBase : EstateItemResourceRepresentationBase, ISettleable
{
    public decimal? Amount { get; set; }
    public bool HasBeenRepaid { get; set; }
    public EstateItemSettlementResourceRepresentation Settlement { get; set; } = null!;
}