using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Microsoft.AspNetCore.WebUtilities;

namespace Exizent.CaseManagement.Client;

public class CaseManagementApiClient : ICaseManagementApiClient
{
    private readonly HttpClient _client;
    private readonly EstateItemsClient _estateItemsClient;
    private readonly DocumentsClient _documentsClient;
    private readonly CollaboratorsClient _collaboratorsClient;

    public CaseManagementApiClient(HttpClient httpClient)
    {
        _client = httpClient;
        _estateItemsClient = new EstateItemsClient(httpClient);
        _documentsClient = new DocumentsClient(httpClient);
        _collaboratorsClient = new CollaboratorsClient(httpClient);
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null,
        CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, companyId, new GetCaseOptions(), cancellationToken);
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int companyId,
        CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, companyId, new GetCaseOptions(), cancellationToken);
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int companyId, GetCaseOptions options,
        CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, companyId, options, cancellationToken);
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, GetCaseOptions options,
        CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, null, options, cancellationToken);
    }

    public async Task<EstateItemResourceRepresentation?> GetEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        return await _estateItemsClient.GetEstateItem(caseId, estateItemId, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PostEstateItem(Guid caseId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        return await _estateItemsClient.PostEstateItem(caseId, estateItem, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PutEstateItem(Guid caseId, Guid estateItemId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        return await _estateItemsClient.PutEstateItem(caseId, estateItemId, estateItem, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ChangeEstateItemStatus(Guid caseId, Guid estateItemId, EstateItemStatusChange statusChange,
        CancellationToken cancellationToken = default)
    {
        await _estateItemsClient.ChangeEstateItemStatus(caseId, estateItemId, statusChange, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ArchiveEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await _estateItemsClient.ArchiveEstateItem(caseId, estateItemId, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task RestoreEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await _estateItemsClient.RestoreEstateItem(caseId, estateItemId, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task CompleteEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await _estateItemsClient.CompleteEstateItem(caseId, estateItemId, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ReopenEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await _estateItemsClient.ReopenEstateItem(caseId, estateItemId, cancellationToken);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task UpdateEstateItemNotes(Guid caseId, Guid estateItemId, string notes,
        CancellationToken cancellationToken = default)
    {
        await _estateItemsClient.UpdateEstateItemNotes(caseId, estateItemId, notes, cancellationToken);
    }

    private async Task<CaseResourceRepresentation?> GetCaseInternal(Guid caseId, int? companyId, GetCaseOptions options,
        CancellationToken cancellationToken)
    {
        var uri = $"/cases/{caseId}";

        if (options.ExpandCompany)
        {
            var param = new Dictionary<string, string> { { "expand", "company" } };
            uri = QueryHelpers.AddQueryString(uri, param);
        }

        if (options.EstateItemsFilter is not null)
        {
            uri = QueryHelpers.AddQueryString(uri,
                new Dictionary<string, string>
                    { { "estateItemsFilter", options.EstateItemsFilter.ToString()! } });
        }

        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        if (companyId.HasValue)
        {
            request.Headers.Add("Company-Id", companyId.Value.ToString(CultureInfo.InvariantCulture));
        }

        using var response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<CaseResourceRepresentation>(body, DefaultJsonSerializerOptions.Instance);
    }

    public async Task RefreshForms(Guid caseId, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, $"/cases/{caseId}/forms/refresh");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<string?> GetDocumentUrl(Guid caseId, string documentKey,
        CancellationToken cancellationToken = default)
    {
        return await _documentsClient.GetDocumentUrl(caseId, documentKey, cancellationToken);
    }

    public async Task<string?> GetDocumentUploadUrl(Guid caseId, Guid estateItemId, string fileName,
        CancellationToken cancellationToken = default)
    {
        return await _documentsClient.GetDocumentUploadUrl(caseId, estateItemId, fileName, cancellationToken);
    }

    public async Task<string?> GetDocumentUploadUrl(Guid caseId, DocumentType documentType, string fileName,
        CancellationToken cancellationToken = default)
    {
        return await _documentsClient.GetDocumentUploadUrl(caseId, documentType, fileName, cancellationToken);
    }    
    
    public async Task DeleteDocument(Guid caseId, string documentKey,
        CancellationToken cancellationToken = default)
    {
        await _documentsClient.DeleteDocument(caseId, documentKey, cancellationToken);
    }
    
    public async Task UpdateCaseOwner(Guid caseId, int ownerId, CancellationToken cancellationToken = default)
    {
        await _collaboratorsClient.UpdateCaseOwner(caseId, ownerId, cancellationToken);
    }
    
    public async Task UpdateCaseCollaborators(Guid caseId, IEnumerable<int> collaboratorIds, CancellationToken cancellationToken = default)
    {
        await _collaboratorsClient.UpdateCaseCollaborators(caseId, collaboratorIds, cancellationToken);
    }
}