namespace Exizent.CaseManagement.Client;

public interface IAuthorizationTokenCache
{
    Task<AuthorizationToken?> GetToken(string scopes);
    Task UpdateToken(string scopes, AuthorizationToken authorizationToken);
}