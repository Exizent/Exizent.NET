using System.Net;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class PostingCaseTests
{
    [Fact]
    public async Task ShouldPostCase()
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();

        using var authApiServer = WireMockServer.Start();
        authApiServer
            .Given(Request.Create().WithPath("/oauth/token")
                .WithBody(new JsonPartialMatcher(
                    $@"{{ ""grant_type"": ""client_credentials"", ""client_id"": ""{clientId}"", ""client_secret"": ""{clientSecret}"", ""scope"": ""{ExizentScopes.All}"", ""audience"": ""https://resources.exizent.com"" }}"))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithBody("{ \"access_token\": \"123456\", \"expires_in\": 3600, \"token_type\": \"Bearer\" }")
            );

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases").UsingPost()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""id"": ""{caseId}""}}")
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.Created)
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
                settings.UserAgent = "My browser";
            }
        );

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var response = await client.CreateCase("CoRef12345678",
            new PostDeceasedResourceRepresentation()
            {
                DateOfBirth = DateTime.Today.AddYears(-76),
                DateOfDeath = DateTime.Today.AddDays(-1),
                FirstName = "John",
                LastName = "Smith",
            });
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        response!.Id.Should().Be(caseId);
    }
    
    [Fact]
    public async Task ShouldPostCaseAndReturnBadRequest()
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();

        using var authApiServer = WireMockServer.Start();
        authApiServer
            .Given(Request.Create().WithPath("/oauth/token")
                .WithBody(new JsonPartialMatcher(
                    $@"{{ ""grant_type"": ""client_credentials"", ""client_id"": ""{clientId}"", ""client_secret"": ""{clientSecret}"", ""scope"": ""{ExizentScopes.All}"", ""audience"": ""https://resources.exizent.com"" }}"))
                .UsingPost())
            .RespondWith(Response.Create()
                .WithBody("{ \"access_token\": \"123456\", \"expires_in\": 3600, \"token_type\": \"Bearer\" }")
            );

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases").UsingPost()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""Errors"": ""Input data had errors""}}")
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.BadRequest)
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
                settings.UserAgent = "My browser";
            }
        );

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var response = await client.CreateCase("CoRef12345678",
            new PostDeceasedResourceRepresentation()
            {
                DateOfBirth = DateTime.Today.AddYears(-76),
                DateOfDeath = DateTime.Today.AddDays(-1),
                FirstName = "John",
                LastName = "Smith",
            });

        response.Should().BeOfType<CaseBadRequestResourceRepresentation>();
        var errorResponse = (CaseBadRequestResourceRepresentation)response!;
        errorResponse!.Id.Should().Be(Guid.Empty);
        errorResponse!.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errorResponse!.Body.Should().Be(@$"{{ ""Errors"": ""Input data had errors""}}");
    }

}