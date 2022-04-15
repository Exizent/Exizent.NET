namespace Exizent.CaseManagement.Client;

public interface IAuthorizationTokenStore
{
    Task<string> GetToken();
}