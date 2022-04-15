namespace Exizent.CaseManagement.Client;

public interface IExizentAuthorizationClient
{
    Task<AuthorizationToken> GetToken(string clientId, string clientSecret, string scopes);
}