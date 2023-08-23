using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.Incomes;

[JsonDiscriminator(nameof(IncomeType.Receipt))]
public class ReceiptIncomeResourceRepresentation : IncomeBaseResourceRepresentation
{
    public string CompanyName { get; init; } = null!;

    public string Description { get; init; } = null!;
    public DateTime? At { get; init; }
}