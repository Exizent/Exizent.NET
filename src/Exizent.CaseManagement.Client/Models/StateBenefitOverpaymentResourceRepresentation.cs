namespace Exizent.CaseManagement.Client.Models;

public class StateBenefitOverpaymentResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal? Amount { get; init; }
    public bool HasBeenRepaid { get; init; }
}