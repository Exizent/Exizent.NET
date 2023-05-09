using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client;

public interface ICaseManagementApiClient
{
    Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null, CancellationToken cancellationToken = default);
    Task<CaseResourceRepresentation?> GetCase(Guid caseId, int companyId, CancellationToken cancellationToken = default);
    Task<CaseResourceRepresentation?> GetCase(Guid caseId, int companyId, GetCaseOptions options, CancellationToken cancellationToken = default);
    Task<CaseResourceRepresentation?> GetCase(Guid caseId, GetCaseOptions options, CancellationToken cancellationToken = default);

    Task RefreshForms(Guid caseId, CancellationToken cancellationToken = default);

}