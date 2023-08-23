namespace Exizent.CaseManagement.Client.Models.Incomes;

public abstract class IncomeBaseResourceRepresentation: IIncomeResourceRepresentation
{
    public Guid Id { get; init; }
    public decimal? Value { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
    public bool IsDeleted { get; init; }
}