using System.Globalization;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client;

public class ExizentAuthorizationTokenStore : IExizentAuthorizationTokenStore
{
    private readonly IExizentAuthorizationTokenCache _cache;
    private readonly IExizentAuthorizationClient _authorizationClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _scopes;

    public ExizentAuthorizationTokenStore(IExizentAuthorizationTokenCache cache, IExizentAuthorizationClient authorizationClient,
        string clientId,
        string clientSecret,
        string scopes)
    {
        _cache = cache;
        _authorizationClient = authorizationClient;
        _clientId = clientId;
        _clientSecret = clientSecret;
        _scopes = scopes;
    }

    public async Task<string> GetToken()
    {
        if (await _cache.GetToken(_scopes) is { } token)
        {
            if (token.Expires - DateTime.UtcNow > TimeSpan.FromMinutes(1))
            {
                return token.Token;
            }
        }

        var authorizationToken = await _authorizationClient.GetToken(_clientId, _clientSecret, _scopes);

        await _cache.UpdateToken(_scopes, authorizationToken);
        return authorizationToken.Token;
    }

}

public class CaseManagementAuthorizationHandler : DelegatingHandler
{
    private readonly IExizentAuthorizationTokenStore _tokenStore;

    public CaseManagementAuthorizationHandler(IExizentAuthorizationTokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenStore.GetToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}

public class CaseManagementApiClient : ICaseManagementApiClient
{
    private readonly HttpClient _client;

    public CaseManagementApiClient(HttpClient httpClient)
    {
        _client = httpClient;
    }

    public async Task<CaseResourceRepresentation?> GetCase(Guid caseId, int? companyId = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/cases/{caseId}");
        if (companyId.HasValue)
        {
            request.Headers.Add("Company-Id", companyId.Value.ToString(CultureInfo.InvariantCulture));
        }

        using var response = await _client.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<CaseResourceRepresentation>(DefaultJsonSerializerOptions
            .Instance);
    }
}