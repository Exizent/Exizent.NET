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

    public CaseManagementApiClient(HttpClient httpClient)
    {
        _client = httpClient;
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
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/cases/{caseId}/estateitems/{estateItemId}");

        using var response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<EstateItemResourceRepresentation>(body,
            DefaultJsonSerializerOptions.Instance);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PostEstateItem(Guid caseId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(estateItem, estateItem.GetType(), DefaultJsonSerializerOptions.Instance);

        using var request = new HttpRequestMessage(HttpMethod.Post, $"/cases/{caseId}/estateitems");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<EstateItemResponseResourceRepresentation>(body,
            DefaultJsonSerializerOptions.Instance);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PutEstateItem(Guid caseId, Guid estateItemId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(estateItem, estateItem.GetType(), DefaultJsonSerializerOptions.Instance);
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/estateitems/{estateItemId}");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        if (response.StatusCode == HttpStatusCode.Created)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<EstateItemResponseResourceRepresentation>(body,
                DefaultJsonSerializerOptions.Instance);
        }

        return new EstateItemResponseResourceRepresentation { Id = estateItemId };
    }


    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ChangeEstateItemStatus(Guid caseId, Guid estateItemId, EstateItemStatusChange statusChange,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(statusChange, DefaultJsonSerializerOptions.Instance);
        using var request =
            new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/estateitems/{estateItemId}/status");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ArchiveEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.Archive, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task RestoreEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.Restore, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task CompleteEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.Complete, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ReopenEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.ReOpen, cancellationToken);
    }

    private async Task ChangeEstateItemStatus(Guid caseId, Guid estateItemId, EstateItemStatusChangeAction status,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/estateitems/{estateItemId}/status");
        request.Content = new StringContent(status.ToString(), Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
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
            switch (options.EstateItemsFilter)
            {
                case EstateItemsFilter.Archived:
                    uri =QueryHelpers.AddQueryString(uri,
                        new Dictionary<string, string>
                            { { "estateItemsFilter", EstateItemsFilter.Archived.ToString() } });
                    break;
                case EstateItemsFilter.Complete:
                    uri =QueryHelpers.AddQueryString(uri,
                        new Dictionary<string, string>
                            { { "estateItemsFilter",   EstateItemsFilter.Complete.ToString() } });
                    break;
                case EstateItemsFilter.Open:
                    uri =QueryHelpers.AddQueryString(uri,
                        new Dictionary<string, string>
                            { { "estateItemsFilter", EstateItemsFilter.Open.ToString() } });
                    break;
                case EstateItemsFilter.AllAssets:
                    uri =QueryHelpers.AddQueryString(uri,
                        new Dictionary<string, string>
                            { { "estateItemsFilter", EstateItemsFilter.AllAssets.ToString() } });
                    break;
            }
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