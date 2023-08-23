using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.Incomes;

[JsonDiscriminator(nameof(IncomeType.Receipt))]
public class ReceiptGetIncomeResourceRepresentation : GetIncomeBaseResourceRepresentation
{
    public string CompanyName { get; init; } = null!;

    public string Description { get; init; } = null!;
    public DateTime? At { get; init; }
}