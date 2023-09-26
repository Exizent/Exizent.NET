namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class EstateItemResourceRepresentation
{
    public Guid Id { get; init; }
    public Location Location { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public bool IsArchived { get; init; }
    public bool IsComplete { get; init; }
    public decimal DateOfDeathValue { get; init; }

    public IReadOnlyList<CaseDocumentResourceRepresentation> Documents { get; set; } =
        new List<CaseDocumentResourceRepresentation>();
}