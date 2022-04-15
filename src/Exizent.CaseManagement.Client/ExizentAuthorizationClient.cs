using System.Net.Http.Json;
using System.Text.Json;

namespace Exizent.CaseManagement.Client;

public class ExizentAuthorizationClient : IExizentAuthorizationClient
{
    private readonly HttpClient _httpClient;

    public ExizentAuthorizationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AuthorizationToken> GetToken(string clientId, string clientSecret, string scopes)
    {
        using var tokenResponse = await _httpClient.PostAsJsonAsync("/oauth/token", new
        {
            grant_type = "client_credentials",
            client_id = clientId,
            client_secret = clientSecret,
            scope = scopes,
            audience = "https://resources.exizent.com"
        });

        tokenResponse.EnsureSuccessStatusCode();

        var jsonDocument = await tokenResponse.Content.ReadFromJsonAsync<JsonDocument>();
        var accessToken = jsonDocument!.RootElement.GetProperty("access_token").GetString()
                          ?? throw new InvalidOperationException("No access token found");
        var expiresIn = jsonDocument.RootElement.GetProperty("expires_in").GetInt32();
        var authorizationToken = new AuthorizationToken(accessToken, DateTime.UtcNow.AddSeconds(expiresIn));
        return authorizationToken;
    }
}