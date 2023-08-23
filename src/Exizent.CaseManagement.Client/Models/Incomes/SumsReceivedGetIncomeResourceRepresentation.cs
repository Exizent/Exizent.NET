using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.Incomes;

[JsonDiscriminator(nameof(IncomeType.SumsReceived))]
public class SumsReceivedGetIncomeResourceRepresentation : GetIncomeBaseResourceRepresentation
{
    public string Description { get; init; } = null!;

    public DateTime? At { get; init; }

    public Guid? EstateItemId { get; init; }

    public IncomeSource? Source { get; init; }
}