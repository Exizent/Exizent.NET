namespace Exizent.CaseManagement.Client.Models.Expenses;

public class ExpenseReimbursementResourceRepresentation
{
    public decimal Value { get; init; }
    public SourceOfFunds SourceOfFunds { get; init; }
    public DateTime At { get; init; }
    public Guid? EstateItemId { get; init; }
    public Guid? PersonId { get; init; }
}
