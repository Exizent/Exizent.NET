using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.StoreCardOrCatalogueAccount))]
public class StoreCardOrCatalogueAccountResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public string? Provider { get; init; }
    public bool HasProviderBeenAdvised { get; init; }
    public decimal? DebtValue { get; init; }
}
