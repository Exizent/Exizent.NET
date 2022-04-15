namespace Exizent.CaseManagement.Client;

public static class ExizentScopes
{
    public static readonly string ReadCase = "read:case";
    
    public static string All => string.Join(" ", ReadCase);
}