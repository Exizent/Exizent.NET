using System.Net;
using Microsoft.Extensions.DependencyInjection;
using WireMock;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class RefreshingCaseFormsTests
{
    [Fact]
    public async Task ShouldReturnSuccess()
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();

        using var authApiServer = WireMockServer.Start();
        authApiServer
            .Given(Request.Create().WithPath("/oauth/token")
                .WithBody(new JsonPartialMatcher($@"{{ ""grant_type"": ""client_credentials"", ""client_id"": ""{clientId}"", ""client_secret"": ""{clientSecret}"", ""scope"": ""{ExizentScopes.All}"", ""audience"": ""https://resources.exizent.com"" }}"))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithBody("{ \"access_token\": \"123456\", \"expires_in\": 3600, \"token_type\": \"Bearer\" }")
            );
        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}/forms/refresh").UsingPost())
            .RespondWith(Response.Create(new ResponseMessage{StatusCode = (int) HttpStatusCode.OK})
                .WithHeader("Authorization", "Bearer 123456")
            );

        var baseUri = casesApiServer.Url;
        var baseAuthorizationUri = authApiServer.Url;

        var serviceContainer = new ServiceCollection();
        serviceContainer.AddExizentCaseManagementClient(
            clientId,
            clientSecret,
            settings =>
            {
                settings.BaseUri = new Uri(baseUri);
                settings.BaseAuthorizationUri = new Uri(baseAuthorizationUri);
                settings.Scope = ExizentScopes.All;
            }
        );
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        await client.RefreshForms(caseId);
    }
}