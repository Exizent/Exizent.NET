using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client;

public class CaseManagementApiClient : ICaseManagementApiClient
{
    private readonly HttpClient _client;

    public CaseManagementApiClient(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null, GetCaseOptions? options = null, CancellationToken cancellationToken = default)
    {
        var expandCompany = options?.ExpandCompany ?? false ? null : "company";

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
}