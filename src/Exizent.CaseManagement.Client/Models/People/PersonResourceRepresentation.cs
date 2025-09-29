namespace Exizent.CaseManagement.Client.Models.People;

public class PersonResourceRepresentation
{
    public Guid Id { get; init; }
    public IReadOnlyList<PersonRole> Roles { get; init; } = null!;
    public ExecutorStatus? ExecutorStatus { get; init; }
    public string? Title { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public IReadOnlyList<string>? OtherNames { get; init; }
    public RelationshipType? RelationshipToDeceased { get; init; }
    public string? OtherRelationshipToDeceased { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public DateTime? DateOfDeath { get; init; }
    public string? ContactNumber { get; init; }
    public string? EmailAddress { get; init; }
    public string? Occupation { get; init; }
    public string? NiNumber { get; init; }
    public AddressResourceRepresentation? Address { get; init; }
    public string? Notes { get; init; }
    public BankAccountDetailsResourceRepresentation? BankDetails { get; init; }
    public bool? IsSignatory { get; init; }
    public string? PlaceOfMarriage { get; init; }
    public DateTime? DateOfMarriageOrCivilPartnership { get; init; }
    public EntitlementResourceRepresentation? Entitlement { get; init; }
    public DateTime CreatedAt { get; init; }
}