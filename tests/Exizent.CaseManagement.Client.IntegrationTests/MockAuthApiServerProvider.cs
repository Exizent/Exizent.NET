using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class MockAuthApiServerProvider
{
    public static WireMockServer Create(out string clientId, out string clientSecret)
    {
        clientId = Guid.NewGuid().ToString();
        clientSecret = Guid.NewGuid().ToString();

        var authApiServer = WireMockServer.Start();
        authApiServer
            .Given(Request.Create().WithPath("/oauth/token")
                .WithBody(new JsonPartialMatcher(
                    $@"{{ ""grant_type"": ""client_credentials"", ""client_id"": ""{clientId}"", ""client_secret"": ""{clientSecret}"", ""scope"": ""{ExizentScopes.All}"", ""audience"": ""https://resources.exizent.com"" }}"))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithBody("{ \"access_token\": \"123456\", \"expires_in\": 3600, \"token_type\": \"Bearer\" }")
            );
        return authApiServer!;
    }
}