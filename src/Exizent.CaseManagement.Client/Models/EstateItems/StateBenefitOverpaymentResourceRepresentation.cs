namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class StateBenefitOverpaymentResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal? Amount { get; init; }
    public bool HasBeenRepaid { get; init; }
}