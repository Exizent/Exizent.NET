using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class GettingACaseTests
{
    [Fact]
    public async Task ShouldReturnCase()
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
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}").UsingGet())
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""id"": ""{caseId}"", ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }} }}")
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

        var @case = await client.GetCase(caseId);
        @case!.Id.Should().Be(caseId);
        @case.Deceased.FirstName.Should().Be("Foo");
        @case.Deceased.LastName.Should().Be("Bar");
        @case.Deceased.DateOfDeath.Should().Be(new DateTime(1988, 02, 01));

        @case.Company.Should().BeNull();
    }
    
        [Fact]
    public async Task ShouldReturnCaseWithCompany()
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
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}").WithParam("expand", "company").UsingGet())
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""id"": ""{caseId}"", ""company"": {{ ""id"": 1, ""name"": ""Google"", ""officePhoneNumber"": ""07711123456"" }}, ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }} }}")
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

        var @case = await client.GetCase(caseId);
        @case!.Id.Should().Be(caseId);
        @case.Deceased.FirstName.Should().Be("Foo");
        @case.Deceased.LastName.Should().Be("Bar");
        @case.Deceased.DateOfDeath.Should().Be(new DateTime(1988, 02, 01));

        @case.Company!.Name.Should().Be("Google");
        @case.Company!.OfficePhoneNumber.Should().Be("07711123456");
    }
}