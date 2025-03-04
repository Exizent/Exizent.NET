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

public class PostCaseResourceRepresentation
{
    public string CompanyCaseId { get; init; } = null!;
    public string? CaseOwnerUsername { get; init; } 
    public PostDeceasedResourceRepresentation Deceased { get; init; } = null!;
}