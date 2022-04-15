namespace Exizent.CaseManagement.Client;

public class AuthorizationToken
{
    public AuthorizationToken(string token, DateTime expires)
    {
        Token = token;
        Expires = expires;
    }

    public string Token { get; }
    public DateTime Expires { get; }
}