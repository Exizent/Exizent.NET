﻿namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class StoreCardOrCatalogueAccountResourceRepresentationBase : EstateItemResourceRepresentationBase, IStandardJointOwnership, ISettleable
{
    
    protected StoreCardOrCatalogueAccountResourceRepresentationBase(): base(EstateItemType.StoreCardOrCatalogueAccount){}

    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public string? Provider { get; set; }
    public bool HasProviderBeenAdvised { get; set; }
    public decimal? DebtValue { get; set; }
    public EstateItemSettlementResourceRepresentation Settlement { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}
