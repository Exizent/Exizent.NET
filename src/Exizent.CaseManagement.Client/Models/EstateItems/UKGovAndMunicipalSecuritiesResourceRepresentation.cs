using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.UkGovAndMunicipalSecurities))]
public class UKGovAndMunicipalSecuritiesResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? NameOfShareholding { get; init; }
    public decimal? Quantity { get; init; }
    public string? DescriptionOfStock { get; init; }
    public decimal? UnitPrice { get; init; }
    public decimal? InterestDue { get; init; }
}