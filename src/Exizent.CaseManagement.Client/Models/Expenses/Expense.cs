namespace Exizent.CaseManagement.Client.Models.Expenses;

public class ExpenseResourceRepresentation
{
    public Guid Id { get; init; }
    public string Description { get; init; } = null!;
    public Guid? CaseItemId { get; init; }
    public decimal Value { get; init; }
    public DateTime From { get; init; }
    public DateTime To { get; init; }
    public string Supplier { get; init; } = null!;
    public bool PaidByThirdParty { get; init; }
    public ExpenseReimbursementResourceRepresentation? Reimbursement { get; init; }
    public string? Notes { get; }
    public ExpensePurposeType? Purpose { get; init; }   
    public bool? GenerateIht423 { get; init; }   
}