namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class EndowmentPolicyResourceRepresentationBase : EstateItemResourceRepresentationBase, IHasAddress, IRealisable
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? Provider { get; set; }
    public string? PolicyNumber { get; set; }
    public EndowmentPolicyType PolicyType { get; set; }
    public decimal? SumAssured { get; set; }
    public decimal? BonusDue { get; set; }
    public bool PaysOnDeathOfDeceased { get; set; }
    public string? Comments { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}