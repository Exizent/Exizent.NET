namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class ListedInvestmentResourceRepresentation
{
    public string? Identifier { get; init; }
    public decimal? Quantity { get; init; }
    public string? ShareDescription { get; init; }
    public decimal? SharePrice { get; init; }
    public decimal? DividendDue { get; init; }
    public decimal? InterestDue { get; init; }
    public decimal? BusinessReliefAt50Percent { get; init; }
    public decimal? BusinessReliefAt100Percent { get; init; }
}