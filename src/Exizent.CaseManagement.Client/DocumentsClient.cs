using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.Json;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client;

internal class DocumentsClient
{
    private readonly HttpClient _client;

    public DocumentsClient(HttpClient httpClient)
    {
        _client = httpClient;
    }
    
    public async Task<string?> GetDocumentUrl(Guid caseId, string documentKey,
        CancellationToken cancellationToken = default)
    {
        using var request =
            new HttpRequestMessage(HttpMethod.Get, $"/cases/{caseId}/documents/{documentKey}/url");

        using var response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return body;
    }

    public async Task<string?> GetDocumentUploadUrl(Guid caseId, Guid estateItemId, string fileName,
        CancellationToken cancellationToken = default)
    {
        var path = $"/cases/{caseId}/documents/uploadurl?documentType={DocumentType.Asset.ToString()}&estateItemId={estateItemId}&fileName={fileName}";
        using var request = new HttpRequestMessage(HttpMethod.Get, path);

        using var response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return body;
    }


    public async Task<string?> GetDocumentUploadUrl(Guid caseId, DocumentType documentType, string fileName,
        CancellationToken cancellationToken = default)
    {
        var path = $"/cases/{caseId}/documents/uploadurl?documentType={documentType}&fileName={fileName}";
        using var request = new HttpRequestMessage(HttpMethod.Get, path);

        using var response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return body;
    }    
    
    public async Task DeleteDocument(Guid caseId, string documentKey,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, $"/cases/{caseId}/documents/{documentKey}");

        using var response = await _client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    /// <summary>The estate item document page size. The API caps this at 100.</summary>
    private const int EstateItemDocumentsPageSize = 100;

    /// <summary>
    /// A stop on the paging loop below. At 100 a page that is more than a handful means something has gone
    /// wrong with the paging rather than that an estate item really has ten thousand documents, and looping
    /// forever against the API would be the worse failure.
    /// </summary>
    private const int MaxEstateItemDocumentPages = 100;

    /// <summary>
    /// Every document attached to an estate item, paging until the API runs out.
    /// </summary>
    /// <returns>An empty list when the case or estate item is not found, matching the other read methods here.</returns>
    public async Task<IReadOnlyList<EstateItemDocumentResourceRepresentation>> GetEstateItemDocuments(
        Guid caseId, Guid estateItemId, CancellationToken cancellationToken = default)
    {
        var documents = new List<EstateItemDocumentResourceRepresentation>();

        for (var pageId = 1; pageId <= MaxEstateItemDocumentPages; pageId++)
        {
            using var request = new HttpRequestMessage(HttpMethod.Get,
                $"/cases/{caseId}/estateitems/{estateItemId}/documents" +
                $"?pageId={pageId}&pageSize={EstateItemDocumentsPageSize}");

            using var response = await _client.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return documents;
            }

            response.EnsureSuccessStatusCode();

            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            var page = JsonSerializer.Deserialize<List<EstateItemDocumentResourceRepresentation>>(body,
                DefaultJsonSerializerOptions.Instance) ?? new List<EstateItemDocumentResourceRepresentation>();

            documents.AddRange(page);

            // A short page is the last one. Paging on the count rather than the Link headers keeps this
            // independent of how the API chooses to express paging.
            if (page.Count < EstateItemDocumentsPageSize)
            {
                break;
            }
        }

        return documents;
    }

    /// <summary>
    /// A presigned download URL for one estate item document.
    /// </summary>
    /// <remarks>
    /// A document that is still being scanned comes back as
    /// <see cref="EstateItemDocumentUrlStatus.NotYetAvailable"/> rather than as an exception, because that
    /// is a normal state for a document uploaded seconds ago and the caller will want to say so.
    /// </remarks>
    public async Task<EstateItemDocumentUrlResult> GetEstateItemDocumentUrl(Guid caseId, Guid estateItemId,
        Guid documentId, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"/cases/{caseId}/estateitems/{estateItemId}/documents/{documentId}/url");

        using var response = await _client.SendAsync(request, cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.NotFound:
                return EstateItemDocumentUrlResult.NotFound;
            // 425 Too Early — the malware scan has not finished.
            case (HttpStatusCode)425:
                return EstateItemDocumentUrlResult.NotYetAvailable;
            case HttpStatusCode.Conflict:
                return EstateItemDocumentUrlResult.Unavailable;
        }

        response.EnsureSuccessStatusCode();

        // The endpoint returns the URL as a bare JSON string, so it arrives quoted.
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var url = JsonSerializer.Deserialize<string>(body, DefaultJsonSerializerOptions.Instance) ?? body;

        return EstateItemDocumentUrlResult.Available(url);
    }

    /// <summary>
    /// Deletes one estate item document. Deleting something already gone is not an error.
    /// </summary>
    public async Task DeleteEstateItemDocument(Guid caseId, Guid estateItemId, Guid documentId,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete,
            $"/cases/{caseId}/estateitems/{estateItemId}/documents/{documentId}");

        using var response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return;
        }

        response.EnsureSuccessStatusCode();
    }
}