﻿namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class MoneyOwedResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IStandardJointOwnership, IRealisable, ICanBeValidForIht
{
    
    protected MoneyOwedResourceRepresentationBase(): base(EstateItemType.MoneyOwed){}

    public AddressResourceRepresentation Address { get; set; } = null!;
    public decimal ProportionOwned { get; set; }
    public bool? IsPassedToSurvivingJointOwner { get; set; }
    public string? NotPassedDetails { get; set; }
    public MoneyOwedClassification Classification { get; set; }
    public string Institution { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal? ExpectedValue { get; set; }
    public bool IsValidForInheritanceTax { get; set; }
    public bool GenerateIht409 { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
    public IReadOnlyList<Guid> JointOwnerIds { get; set; } = Array.Empty<Guid>();
}