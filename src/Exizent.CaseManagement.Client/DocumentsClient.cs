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
   
}