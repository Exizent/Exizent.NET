namespace Exizent.CaseManagement.Client.Models.EstateItems;

public abstract class UKGovAndMunicipalSecuritiesResourceRepresentationBase : EstateItemResourceRepresentationBase, IRealisable, IHasAddress
{
    public AddressResourceRepresentation Address { get; set; } = null!;
    public string? NameOfShareholding { get; set; }
    public decimal? Quantity { get; set; }
    public string? DescriptionOfStock { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? InterestDue { get; set; }
    public EstateItemRealisationResourceRepresentation Realisation { get; set; } = null!;
}