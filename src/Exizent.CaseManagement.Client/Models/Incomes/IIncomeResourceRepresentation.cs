namespace Exizent.CaseManagement.Client.Models.Incomes;

public interface IIncomeResourceRepresentation
{
    decimal? Value { get; }
    
    string? Notes { get; }
}