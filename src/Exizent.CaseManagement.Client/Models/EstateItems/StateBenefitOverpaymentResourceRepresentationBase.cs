namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class StateBenefitOverpaymentResourceRepresentationBase : EstateItemResourceRepresentationBase, ISettleable
{
    protected StateBenefitOverpaymentResourceRepresentationBase(): base(EstateItemType.StateBenefitOverpayment){}

    public string? Institution { get; set; }
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
    public bool HasBeenRepaid { get; set; }
    public EstateItemSettlementResourceRepresentation Settlement { get; set; } = null!;
}