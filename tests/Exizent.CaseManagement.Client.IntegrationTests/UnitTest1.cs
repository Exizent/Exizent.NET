using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var caseId = Guid.NewGuid();

        using var authApiServer = WireMockServer.Start();
        authApiServer
            .Given(Request.Create().WithPath("/oauth/token").UsingPost())
            .RespondWith(Response.Create()
                .WithBody("{ \"access_token\": \"123456\", \"expires_in\": 3600, \"token_type\": \"Bearer\" }")
            );

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}").UsingGet())
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""id"": ""{caseId}"", ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }} }}")
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = new ServiceCollection();
        serviceContainer.AddExizentCaseManagementClient(
            new Uri(casesApiServer.Url),
            new Uri(authApiServer.Url),
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            ExizentScopes.All
        );
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var @case = await client.GetCase(caseId);
        @case!.Id.Should().Be(caseId);
        @case.Deceased.FirstName.Should().Be("Foo");
        @case.Deceased.LastName.Should().Be("Bar");
        @case.Deceased.DateOfDeath.Should().Be(new DateTime(1988, 02, 01));
    }
}