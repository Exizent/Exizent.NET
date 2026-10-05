using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.EstateItemValuations;

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

    /// <summary>
    /// Asks for an automated valuation of an estate item. It is processed in the background: poll
    /// <see cref="GetEstateItemValuation"/> until it is no longer pending. While one is pending, a repeated request
    /// returns that one. Any status other than those <see cref="EstateItemValuationRequestResponse"/> describes
    /// throws.
    /// </summary>
    Task<EstateItemValuationRequestResponse> RequestEstateItemValuation(Guid caseId, Guid estateItemId,
        EstateItemValuationProvider provider, CancellationToken cancellationToken = default);

    /// <returns>
    /// The estate item's valuations, newest request first and pending ones included, or null if the case or estate
    /// item is not found or no provider values that type of item.
    /// </returns>
    Task<IReadOnlyList<EstateItemValuationResourceRepresentation>?> ListEstateItemValuations(Guid caseId,
        Guid estateItemId, CancellationToken cancellationToken = default);

    /// <returns>
    /// The valuation, or null if it, its estate item or its case is not found, or no provider values that type of
    /// item.
    /// </returns>
    Task<EstateItemValuationResourceRepresentation?> GetEstateItemValuation(Guid caseId, Guid estateItemId,
        Guid valuationId, CancellationToken cancellationToken = default);
    Task<string?> GetDocumentUrl(Guid caseId, string documentKey, CancellationToken cancellationToken = default);
    Task<string?> GetDocumentUploadUrl(Guid caseId, Guid estateItemId, string fileName,
        CancellationToken cancellationToken = default);
    Task<string?> GetDocumentUploadUrl(Guid caseId, DocumentType documentType, string fileName,
        CancellationToken cancellationToken = default);
    Task DeleteDocument(Guid caseId, string documentKey, CancellationToken cancellationToken = default);

    /// <summary>
    /// Every document attached to an estate item, from the Cases API's document metadata store — whichever
    /// system uploaded it.
    /// </summary>
    Task<IReadOnlyList<EstateItemDocumentResourceRepresentation>> GetEstateItemDocuments(Guid caseId,
        Guid estateItemId, CancellationToken cancellationToken = default);

    /// <summary>
    /// A presigned download URL for one estate item document, or why there isn't one.
    /// </summary>
    Task<EstateItemDocumentUrlResult> GetEstateItemDocumentUrl(Guid caseId, Guid estateItemId, Guid documentId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes one estate item document.
    /// </summary>
    Task DeleteEstateItemDocument(Guid caseId, Guid estateItemId, Guid documentId,
        CancellationToken cancellationToken = default);
    Task UpdateCaseOwner(Guid caseId, int ownerId, CancellationToken cancellationToken = default);
    Task UpdateCaseCollaborators(Guid caseId, IEnumerable<int> collaboratorIds, CancellationToken cancellationToken = default);
}