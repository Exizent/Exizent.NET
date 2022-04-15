namespace Exizent.CaseManagement.Client;

public class ExizentUris
{
    public static readonly Uri CaseManagementProduction = new("https://cases.api.exizent.com/");
    public static readonly Uri AuthorizationProduction = new("https://identity.exizent.com/");
}

public static class ExizentScopes
{
    public static readonly string ReadCase = "read:case";
    
    public static string All => string.Join(" ", ReadCase);
}