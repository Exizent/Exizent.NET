using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class ServiceCollectionBuilder
{
    public static ServiceCollection Create(WireMockServer authServer, 
        WireMockServer casesServer, string clientId, string clientSecret,  bool addUserAgent = false)
    {
        var baseUri = casesServer.Url;
        var baseAuthorizationUri = authServer.Url;

        var serviceContainer = new ServiceCollection();
        serviceContainer.AddExizentCaseManagementClient(
            clientId,
            clientSecret,
            settings =>
            {
                settings.BaseUri = new Uri(baseUri);
                settings.BaseAuthorizationUri = new Uri(baseAuthorizationUri);
                settings.Scope = ExizentScopes.All;
                if(addUserAgent)
                {
                    settings.UserAgent = "My browser";
                }
            }
        );

        return serviceContainer;
    }
}