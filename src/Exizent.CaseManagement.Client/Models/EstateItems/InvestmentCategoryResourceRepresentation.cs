namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class InvestmentCategoryResourceRepresentation
{
    public Guid Id { get; set; }
    public InvestmentType Category { get; set; }
    public bool? OwnedForTwoYears { get; set; }
    public bool? HadControlOfTheCompany { get; set; }
    public bool? IsListedOnRecognisedStockExchange { get; set; }
    public bool? IsTradedElsewhere { get; set; }
    public IReadOnlyList<ListedInvestmentResourceRepresentation> Investments { get; set; } = null!;
};