﻿namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class MortgageResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public string? Provider { get; init; }
    public string? MortgageType { get; init; }
    public Guid? LinkedEstateItemId { get; init; }
    public decimal? DebtValue { get; init; }
}