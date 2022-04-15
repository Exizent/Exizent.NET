namespace Exizent.CaseManagement.Client;

public interface IExizentAuthorizationTokenCache
{
    Task<AuthorizationToken?> GetToken(string scopes);
    Task UpdateToken(string scopes, AuthorizationToken authorizationToken);
}