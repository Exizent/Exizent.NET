﻿namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class TrustLiabilityResourceRepresentation
{
    public string? NameOfCreditor { get; set; }
    public string Description { get; init; } = null!;
    public decimal Value { get; init; }
}