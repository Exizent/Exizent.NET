using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.StateBenefitOverpayment))]
public class StateBenefitOverpaymentResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal? Amount { get; init; }
    public bool HasBeenRepaid { get; init; }
    public EstateItemSettlementResourceRepresentation? Settlement { get; init; }
}