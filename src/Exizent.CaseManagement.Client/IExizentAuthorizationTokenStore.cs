namespace Exizent.CaseManagement.Client;

public interface IExizentAuthorizationTokenStore
{
    Task<string> GetToken();
}