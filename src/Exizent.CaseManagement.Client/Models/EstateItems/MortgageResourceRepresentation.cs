using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.Mortgage))]
public class MortgageResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public string? Provider { get; init; }
    public string? MortgageType { get; init; }
    public Guid? LinkedEstateItemId { get; init; }
    public decimal? DebtValue { get; init; }
    public EstateItemSettlementResourceRepresentation? Settlement { get; init; }
}