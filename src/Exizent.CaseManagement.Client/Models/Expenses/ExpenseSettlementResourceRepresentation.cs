namespace Exizent.CaseManagement.Client.Models.Expenses;

public class ExpenseSettlementResourceRepresentation
{
    private readonly Guid? _caseItemId;
    private readonly Guid? _estateItemId;

    public Guid Id { get; init; }
    public decimal Value { get; init; }
    public SourceOfFunds SourceOfFunds { get; init; }
    public DateTime At { get; init; }

    // Falls back to estateItemId so settlements that only carry the older name are still read,
    // whichever order the two properties arrive in.
    public Guid? CaseItemId
    {
        get => _caseItemId ?? _estateItemId;
        init => _caseItemId = value;
    }

    [Obsolete("Use CaseItemId instead.")]
    public Guid? EstateItemId
    {
        get => CaseItemId;
        init => _estateItemId = value;
    }

    public Guid? PersonId { get; init; }
    public bool? GenerateIht423 { get; init; }
    public ShareSettlementDetailsResourceRepresentation? ShareSettlement { get; init; }
}
