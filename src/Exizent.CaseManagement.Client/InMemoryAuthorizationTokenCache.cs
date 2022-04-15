using System.Collections.Concurrent;

namespace Exizent.CaseManagement.Client;

public class InMemoryAuthorizationTokenCache : IAuthorizationTokenCache
{
    private readonly ConcurrentDictionary<string, AuthorizationToken> _cache = new();

    public Task<AuthorizationToken?> GetToken(string scopes)
    {
        _cache.TryGetValue(scopes, out var token);

        return Task.FromResult(token);
    }

    public Task UpdateToken(string scopes, AuthorizationToken authorizationToken)
    {
        _cache.AddOrUpdate(scopes, authorizationToken, (_, _) => authorizationToken);
        
        return Task.CompletedTask;
    }
}