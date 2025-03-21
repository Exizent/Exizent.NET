using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client;

public interface ICaseManagementApiClient
{
    Task<CaseResponseResourceRepresentation> CreateCase(string companyCaseId,
        PostDeceasedResourceRepresentation deceased, CancellationToken cancellationToken = default);

    Task UpdateCaseStatus(Guid caseId, CaseStatus status, CancellationToken cancellationToken = default);

    Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null,
        CancellationToken cancellationToken = default);

    Task<CaseResourceRepresentation?>
        GetCase(Guid caseId, int companyId, CancellationToken cancellationToken = default);

    Task<CaseResourceRepresentation?> GetCase(Guid caseId, int companyId, GetCaseOptions options,
        CancellationToken cancellationToken = default);

    Task<CaseResourceRepresentation?> GetCase(Guid caseId, GetCaseOptions options,
        CancellationToken cancellationToken = default);

    Task<EstateItemResourceRepresentation?> GetEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default);

    Task RefreshForms(Guid caseId, CancellationToken cancellationToken = default);

    Task<EstateItemResponseResourceRepresentation?> PostEstateItem(Guid caseId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default);

    Task<EstateItemResponseResourceRepresentation?> PutEstateItem(Guid caseId, Guid estateItemId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default);

    Task ChangeEstateItemStatus(Guid caseId, Guid estateItemId, EstateItemStatusChange statusChange,
        CancellationToken cancellationToken = default);

    Task ArchiveEstateItem(Guid caseId, Guid estateItemId, CancellationToken cancellationToken = default);
    Task RestoreEstateItem(Guid caseId, Guid estateItemId, CancellationToken cancellationToken = default);
    Task CompleteEstateItem(Guid caseId, Guid estateItemId, CancellationToken cancellationToken = default);
    Task ReopenEstateItem(Guid caseId, Guid estateItemId, CancellationToken cancellationToken = default);
    Task UpdateEstateItemNotes(Guid caseId, Guid estateItemId, string notes,
        CancellationToken cancellationToken = default);
    Task<string?> GetDocumentUrl(Guid caseId, string documentKey, CancellationToken cancellationToken = default);
    Task<string?> GetDocumentUploadUrl(Guid caseId, Guid estateItemId, string fileName,
        CancellationToken cancellationToken = default);
    Task<string?> GetDocumentUploadUrl(Guid caseId, DocumentType documentType, string fileName,
        CancellationToken cancellationToken = default);
    Task DeleteDocument(Guid caseId, string documentKey, CancellationToken cancellationToken = default);
    Task UpdateCaseOwner(Guid caseId, int ownerId, CancellationToken cancellationToken = default);
    Task UpdateCaseCollaborators(Guid caseId, IEnumerable<int> collaboratorIds, CancellationToken cancellationToken = default);
}