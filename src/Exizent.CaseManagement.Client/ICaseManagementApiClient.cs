using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client;

public interface ICaseManagementApiClient
{
    Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null,
        CancellationToken cancellationToken = default);
}