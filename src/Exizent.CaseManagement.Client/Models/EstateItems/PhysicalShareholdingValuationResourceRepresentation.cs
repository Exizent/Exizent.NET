using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

public enum ShareValuationType
{
    SinglePrice,
    QuarterUp
}

public abstract class PhysicalShareholdingValuationResourceRepresentation { }

[JsonDiscriminator(nameof(ShareValuationType.SinglePrice))]
public class SingleSharePriceValuationResourceRepresentation : PhysicalShareholdingValuationResourceRepresentation
{
    public decimal? Price { get; init; }
}

[JsonDiscriminator(nameof(ShareValuationType.QuarterUp))]
public class QuarterUpSharePriceValuationResourceRepresentation : PhysicalShareholdingValuationResourceRepresentation
{
    public decimal? LowPrice { get; init; }
    public decimal? HighPrice { get; init; }
}
