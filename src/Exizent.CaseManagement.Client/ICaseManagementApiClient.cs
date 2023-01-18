using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client;

public interface ICaseManagementApiClient
{
    Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null, GetCaseOptions? options = null,
        CancellationToken cancellationToken = default);
}