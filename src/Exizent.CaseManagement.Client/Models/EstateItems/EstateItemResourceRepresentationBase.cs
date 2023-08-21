namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class EstateItemResourceRepresentationBase
{
    public Guid Id { get; init; }
    public Location Location { get; init; }
    public string? Notes { get; init; }
}