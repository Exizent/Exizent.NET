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
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}/forms/refresh").UsingPost())
            .RespondWith(Response.Create(new ResponseMessage{StatusCode = (int) HttpStatusCode.OK})
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret);
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        await client.RefreshForms(caseId);
    }
}