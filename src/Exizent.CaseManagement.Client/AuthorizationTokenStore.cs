namespace Exizent.CaseManagement.Client;

public class AuthorizationTokenStore : IAuthorizationTokenStore
{
    private readonly IAuthorizationTokenCache _cache;
    private readonly IExizentAuthorizationClient _authorizationClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _scopes;

    public AuthorizationTokenStore(IAuthorizationTokenCache cache, IExizentAuthorizationClient authorizationClient,
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