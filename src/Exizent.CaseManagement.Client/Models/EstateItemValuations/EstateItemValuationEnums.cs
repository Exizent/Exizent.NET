namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

// Member names are the API's. An unknown name fails the whole read, so a value must be added here before the API
// starts returning it.

/// <summary>The service that produced, or was asked for, an estate item valuation.</summary>
public enum EstateItemValuationProvider
{
    Hometrack,
    Zoopla
}

/// <summary>Where an estate item valuation has got to, and whether it returned a value.</summary>
public enum EstateItemValuationStatus
{
    Completed,
    Failed,

    /// <summary>Requested and not yet finished. Never on an estate item's own summaries.</summary>
    Pending
}

/// <summary>How sure the provider is of a value, on one scale whatever the provider.</summary>
public enum EstateItemValuationConfidence
{
    Low,
    Medium,
    High
}
