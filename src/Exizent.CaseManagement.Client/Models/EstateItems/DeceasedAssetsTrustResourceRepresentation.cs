using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.DeceasedAssetsTrust))]
public class DeceasedAssetsTrustResourceRepresentation : EstateItemResourceRepresentation
{
    public DeceasedAssetsTrustType TrustType { get; init; }
    public string? NameOfSettlor { get; init; }
    public DateTime? SettlorDateOfDeathOrSettlementDate { get; init; }
    public string? Name { get; init; }
    public string? NameAndAgeOfPersonReceivingBenefit { get; init; }
    public string? NameOfPersonReceivingBenefit { get; init; }
    public int? AgeOfPersonReceivingBenefit { get; init; }
    public SolicitorResourceRepresentation? Solicitor { get; init; }
    public List<TrusteeResourceRepresentation> Trustees { get; init; } = new List<TrusteeResourceRepresentation>();
    public string? UniqueTaxReferenceNumber { get; init; }
    public DateTime? TrustCreationDate { get; init; }
    public bool HasDetailsOfAssets { get; init; }
    public TrustAssetDetailsResourceRepresentation PropertyBusinessSharesAssets { get; init; } = null!;
    public TrustAssetDetailsResourceRepresentation OtherAssets { get; init; } = null!;
    public decimal? TotalValue { get; init; }
    public bool IsTaxOnTotalValueToBePaidNow { get; init; }
    public decimal? EstimatedValue { get; init; }
    public IhtFormApplicantCapacity? Capacity { get; init; }
}