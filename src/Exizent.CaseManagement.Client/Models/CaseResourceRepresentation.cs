using Exizent.CaseManagement.Client.Models.Company;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Models.Organisations;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Models;

public class CaseResourceRepresentation
{
    public Guid Id { get; init; }

    public CompanyResourceRepresentation? Company { get; init; } = null!;
    public DeceasedResourceRepresentation Deceased { get; init; } = null!;
    public IReadOnlyList<PersonResourceRepresentation> People { get; init; } = null!;
    public IReadOnlyList<OrganisationResourceRepresentation> Organisations { get; init; } = null!;
    public IReadOnlyList<EstateItemResourceRepresentation> EstateItems { get; init; } = null!;
    public IReadOnlyList<ExpenseResourceRepresentation> Expenses { get; init; } = null!;
}