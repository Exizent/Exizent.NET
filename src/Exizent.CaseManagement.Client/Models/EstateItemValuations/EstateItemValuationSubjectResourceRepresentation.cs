namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

public enum EstateItemValuationSubjectType
{
    Property
}

/// <summary>What was sent for valuation. Its shape depends on how the thing being valued is identified.</summary>
public abstract class EstateItemValuationSubjectResourceRepresentation { }

/// <summary>A property, identified by its postal address.</summary>
public class PropertyEstateItemValuationSubjectResourceRepresentation : EstateItemValuationSubjectResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
}

/// <summary>What the provider said about the subject beyond its value. Its shape matches the subject's.</summary>
public abstract class EstateItemValuationDetailsResourceRepresentation { }

public class PropertyEstateItemValuationDetailsResourceRepresentation : EstateItemValuationDetailsResourceRepresentation
{
    /// <summary>The address the provider says it valued.</summary>
    public string? MatchedAddress { get; init; }
}
