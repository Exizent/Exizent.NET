using Exizent.CaseManagement.Client;

namespace Microsoft.Extensions.DependencyInjection;

public class ExizentCaseManagementClientSettings
{
    public ExizentCaseManagementClientSettings(string clientId, string clientSecret)
    {
        ClientId = clientId;
        ClientSecret = clientSecret;
    }

    /// <summary>
    /// The client id for the application.
    /// </summary>
    public string ClientId { get; }
    /// <summary>
    /// The client secret for the application.
    /// </summary>
    public string ClientSecret { get; }
        
    /// <summary>
    /// The scope for the application.
    /// </summary>
    public string Scope { get; set; } = ExizentScopes.All;
        
    /// <summary>
    /// The base url for the api.
    /// </summary>
    public Uri BaseUri { get; set; } = ExizentUris.CaseManagementProduction;
        
    /// <summary>
    /// The base authorization url for the api.
    /// </summary>
    public Uri BaseAuthorizationUri { get; set; } = ExizentUris.AuthorizationProduction;
}