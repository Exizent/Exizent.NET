using System.Net;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class ChangingEstateItemStatus
{
    [Fact]
    public async Task ShouldUpdateEstateItemStatus()
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

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
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}/status").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.NoContent)
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

        await client.ChangeEstateItemStatus(caseId, estateItemId, new EstateItemStatusChange { Status = EstateItemStatusChangeAction.Complete });
    }
    
        [Fact]
    public async Task ShouldErrorWhenUpdateEstateItemStatusFailsWith404NotFound()
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

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
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}/status").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.NotFound)
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

        
        
        Func<Task> action = async () => await client.ChangeEstateItemStatus(caseId, estateItemId, new EstateItemStatusChange { Status = EstateItemStatusChangeAction.Complete });

        await action.Should().ThrowAsync<HttpRequestException>().WithMessage("Response status code does not indicate success: 404 (Not Found).");
    }
    
    [Theory]
    [InlineData(EstateItemStatusChangeAction.Archive)]
    [InlineData(EstateItemStatusChangeAction.Restore)]
    [InlineData(EstateItemStatusChangeAction.Complete)]
    [InlineData(EstateItemStatusChangeAction.ReOpen)]
    public async Task ShouldArchiveEstateItem(EstateItemStatusChangeAction status)
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

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
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}/status").UsingPut()
                    .WithBody(status.ToString())
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.NoContent)
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

        switch (status)
        {
            case EstateItemStatusChangeAction.Archive:
                await client.ArchiveEstateItem(caseId, estateItemId);
                break;
            case EstateItemStatusChangeAction.Restore:
                await client.RestoreEstateItem(caseId, estateItemId);
                break;
            case EstateItemStatusChangeAction.Complete:
                await client.CompleteEstateItem(caseId, estateItemId);
                break;
            case EstateItemStatusChangeAction.ReOpen:
                await client.ReopenEstateItem(caseId, estateItemId);
                break;
        }
    }


    [Theory]
    [InlineData(EstateItemStatusChangeAction.Archive)]
    [InlineData(EstateItemStatusChangeAction.Restore)]
    [InlineData(EstateItemStatusChangeAction.Complete)]
    [InlineData(EstateItemStatusChangeAction.ReOpen)]
    public async Task ShouldThrow404NotFoundExceptionIfDocumentIsNotFound(EstateItemStatusChangeAction status)
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

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
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}/status").UsingPut()
                    .WithBody(status.ToString())
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.NotFound)
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

        Func<Task>? action = status switch
        {
            EstateItemStatusChangeAction.Archive => async () => await client.ArchiveEstateItem(caseId, estateItemId),
            EstateItemStatusChangeAction.Restore => async () => await client.RestoreEstateItem(caseId, estateItemId),
            EstateItemStatusChangeAction.Complete => async () => await client.CompleteEstateItem(caseId, estateItemId),
            EstateItemStatusChangeAction.ReOpen => async () => await client.ReopenEstateItem(caseId, estateItemId),
            _ => null
        };
        
        await action.Should().ThrowAsync<HttpRequestException>().WithMessage("Response status code does not indicate success: 404 (Not Found).");

    }
    
    [Theory]
    [InlineData(EstateItemStatusChangeAction.Archive)]
    [InlineData(EstateItemStatusChangeAction.Restore)]
    [InlineData(EstateItemStatusChangeAction.Complete)]
    [InlineData(EstateItemStatusChangeAction.ReOpen)]
    public async Task ShouldThrow404NotFoundExceptionIfWrongBodySent(EstateItemStatusChangeAction status)
    {
        var clientId = Guid.NewGuid().ToString();
        var clientSecret = Guid.NewGuid().ToString();

        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

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
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}/status").UsingPut()
                    .WithBody("BadBody")
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.NoContent)
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

        Func<Task>? action = status switch
        {
            EstateItemStatusChangeAction.Archive => async () => await client.ArchiveEstateItem(caseId, estateItemId),
            EstateItemStatusChangeAction.Restore => async () => await client.RestoreEstateItem(caseId, estateItemId),
            EstateItemStatusChangeAction.Complete => async () => await client.CompleteEstateItem(caseId, estateItemId),
            EstateItemStatusChangeAction.ReOpen => async () => await client.ReopenEstateItem(caseId, estateItemId),
            _ => null
        };
        
        await action.Should().ThrowAsync<HttpRequestException>().WithMessage("Response status code does not indicate success: 404 (Not Found).");

    }
    
}