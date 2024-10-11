using Exizent.CaseManagement.Client.Models.Collaborators;
using Exizent.CaseManagement.Client.Models.Company;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.Distributions;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Models.Organisations;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Models;

public class CaseResourceRepresentation
{
    public Guid Id { get; init; }
    public decimal TaxThreshold { get; init; }
    public decimal ClientAccountBalance { get; init; }
    public CompanyResourceRepresentation? Company { get; init; }
    public DeceasedResourceRepresentation Deceased { get; init; } = null!;
    public IReadOnlyList<PersonResourceRepresentation> People { get; init; } = null!;
    public IReadOnlyList<OrganisationResourceRepresentation> Organisations { get; init; } = null!;
    public IReadOnlyList<EstateItemResourceRepresentation> EstateItems { get; init; } = null!;
    public IReadOnlyList<ExpenseResourceRepresentation> Expenses { get; init; } = null!;
    public IReadOnlyList<IncomeBaseResourceRepresentation> Incomes { get; init; } = null!;
    public IReadOnlyList<DistributionResourceRepresentation> Distributions { get; init; } = null!;
    public FinancialGiftsDetailsResourceRepresentation FinancialGiftsDetails { get; init; } = null!;
    public IReadOnlyList<CaseDocumentResourceRepresentation> Documents { get; init; } = null!;
    public CollaboratorResourceRepresentation Owner { get; init; } = null!;
    public IReadOnlyList<CollaboratorResourceRepresentation> Collaborators { get; init; } = null!;
    public CompanyAddressResourceRepresentation Address { get; init; } = null!;
}