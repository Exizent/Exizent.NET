namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

// Member names are the API's. An unknown name fails the whole read, so a value must be added here before the API
// starts returning it.

/// <summary>The service that produced, or was asked for, an estate item valuation.</summary>
public enum EstateItemValuationProvider
{
    Hometrack
}

/// <summary>Where an estate item valuation has got to, and whether it returned a value.</summary>
public enum EstateItemValuationStatus
{
    Completed,
    Failed,

    /// <summary>Requested and not yet finished.</summary>
    Pending
}

/// <summary>How sure the provider is of a value, on one scale whatever the provider.</summary>
public enum EstateItemValuationConfidence
{
    Low,
    Medium,
    High
}

/// <summary>Why an estate item valuation failed, on one scale whatever the provider.</summary>
public enum EstateItemValuationFailure
{
    /// <summary>The provider answered, but with no value for the item.</summary>
    NotValued,

    /// <summary>The provider could not be reached, refused the call, or answered with something unreadable.</summary>
    ProviderUnavailable,

    /// <summary>No answer arrived before the valuation's timeout.</summary>
    TimedOut,

    /// <summary>The valuation was stored but never queued to be asked for.</summary>
    CouldNotStart,

    /// <summary>Something unexpected went wrong while asking the provider.</summary>
    CouldNotComplete
}
