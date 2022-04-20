using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Models;

public class CaseResourceRepresentation
{
    public Guid Id { get; init; }
    public DeceasedResourceRepresentation Deceased { get; init; } = null!;
    public IReadOnlyList<PersonResourceRepresentation> People { get; init; } = null!;
    public IReadOnlyList<EstateItemResourceRepresentation> EstateItems { get; init; } = null!;
    public IReadOnlyList<ExpenseResourceRepresentation> Expenses { get; init; } = null!;
}