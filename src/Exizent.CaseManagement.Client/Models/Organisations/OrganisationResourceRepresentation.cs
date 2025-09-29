using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Models.Organisations;

public class OrganisationResourceRepresentation
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Type { get; init; }
    public IReadOnlyList<OrganisationRole> Roles { get; init; } = null!;
    public ExecutorStatus? ExecutorStatus { get; init; }
    public IndividualSignatoryDetailsResourceRepresentation? IndividualSignatoryDetails { get; init; }
    public string? RegisteredCharityNumber { get; init; }
    public string? CompaniesHouseNumber { get; init; }
    
    public string? UniqueTaxpayerReference { get; init; }
    public AddressResourceRepresentation? Address { get; init; }
    public BankAccountDetailsResourceRepresentation? BankDetails { get; init; }
    public string? ContactNumber { get; init; }
    public string? EmailAddress { get; init; }
    public string? Notes { get; init; }
    public bool IsSignatory { get; init; }
    public EntitlementResourceRepresentation? Entitlement { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}