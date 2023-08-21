namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class EstateItemResourceRepresentation: EstateItemResourceRepresentationBase
{
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public bool IsArchived { get; init; }
    public bool IsComplete { get; init; }
    public decimal DateOfDeathValue { get; init; }
}