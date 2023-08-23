namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class EstateItemResourceRepresentationBase
{
    protected EstateItemResourceRepresentationBase(EstateItemType type)
    {
        Type = type;
    }
    
    public EstateItemType Type { get; }

    public Guid Id { get; set; }
    public Location Location { get; set; }
    public string? Notes { get; set; }
}