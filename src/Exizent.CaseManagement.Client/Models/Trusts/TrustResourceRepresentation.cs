namespace Exizent.CaseManagement.Client.Models.Trusts;

public class TrustResourceRepresentation
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public EntitlementResourceRepresentation? Entitlement { get; init; }
    public IReadOnlyList<Guid> Beneficiaries { get; init; } = new List<Guid>();
    public bool? ShowBeneficiariesOnEstateAccounts { get; init; }
    public IReadOnlyList<Guid> Trustees { get; init; } = new List<Guid>();
    public bool? ShowTrusteesOnEstateAccounts { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public IReadOnlyList<NoteResourceRepresentation> Notes { get; init; } = new List<NoteResourceRepresentation>();
}