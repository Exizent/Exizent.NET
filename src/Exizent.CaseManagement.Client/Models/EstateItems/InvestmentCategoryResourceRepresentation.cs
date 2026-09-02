namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class InvestmentCategoryResourceRepresentation
{
    public Guid Id { get; set; }
    public InvestmentCategoryType Category { get; set; }
    public IReadOnlyList<ListedInvestmentResourceRepresentation> Investments { get; set; } = null!;
};