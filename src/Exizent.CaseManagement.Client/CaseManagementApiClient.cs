using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client;

public class CaseManagementApiClient : ICaseManagementApiClient
{
    private readonly HttpClient _client;

    public CaseManagementApiClient(HttpClient httpClient)
    {
        _client = httpClient;
    }
    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null, CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, companyId,  new GetCaseOptions(), cancellationToken);
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int companyId, CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, companyId, new GetCaseOptions(), cancellationToken);
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int companyId, GetCaseOptions options, CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, companyId, options, cancellationToken);
    }

    public async  Task<CaseResourceRepresentation?> GetCase(Guid caseId, GetCaseOptions options, CancellationToken cancellationToken = default)
    {
        return await GetCaseInternal(caseId, null, options, cancellationToken);
    }


    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PostEstateItem(Guid caseId, EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        return await PostPutEstateItem(HttpMethod.Post, caseId, estateItem, cancellationToken);
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PutEstateItem(Guid caseId, EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        return await PostPutEstateItem(HttpMethod.Put, caseId, estateItem, cancellationToken);
    }
    
    private async Task<EstateItemResponseResourceRepresentation?> PostPutEstateItem(HttpMethod httpMethod, Guid caseId, EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(estateItem, estateItem.GetType(),DefaultJsonSerializerOptions.Instance);
        
        using var request = new HttpRequestMessage(httpMethod, $"/cases/{caseId}/estateitems");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
        
        if (response.StatusCode == HttpStatusCode.Created)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<EstateItemResponseResourceRepresentation>(body, DefaultJsonSerializerOptions.Instance);
        }

        return new EstateItemResponseResourceRepresentation { Id = estateItem.Id };
    }
    
    
    private async Task<CaseResourceRepresentation?> GetCaseInternal(Guid caseId, int? companyId, GetCaseOptions options, CancellationToken cancellationToken)
    {
        var expandCompany = options.ExpandCompany ? "company" : null;

        var query = expandCompany is null ? "" : $"?expand={expandCompany}";
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/cases/{caseId}{query}");
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
}