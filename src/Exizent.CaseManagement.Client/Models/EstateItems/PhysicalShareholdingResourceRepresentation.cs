namespace Exizent.CaseManagement.Client.Models;

public class PhysicalShareholdingResourceRepresentation : EstateItemResourceRepresentation
{
    public AddressResourceRepresentation Address { get; init; } = null!;
    public string? ShareholderName { get; init; }
    public string? ShareholderReferenceNumber { get; init; }
    public decimal? Quantity { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal? DividendDue { get; init; }
    public bool IsQuotedOnLondonStockExchange { get; init; }
}