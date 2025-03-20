using System.Net;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class UpdatingCollaborators
{
    [Fact]
    public async Task ShouldUpdateCaseOwner()
    {
        var caseId = Guid.NewGuid();
        var ownerId = 1;

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/owner").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.OK)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        await client.UpdateCaseOwner(caseId, ownerId);
    }
    
    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Response status code does not indicate success: 404 (Not Found).")]
    [InlineData(HttpStatusCode.BadRequest, "Response status code does not indicate success: 400 (Bad Request).")]
    public async Task ShouldThrowExceptionWhenUpdateCaseOwnerFails(HttpStatusCode statusCode, string expectedMessage)
    {
        var caseId = Guid.NewGuid();
        var ownerId = 1;

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/owner").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(statusCode)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var action = async () => await client.UpdateCaseOwner(caseId, ownerId);
        await action.Should().ThrowAsync<HttpRequestException>().WithMessage(expectedMessage);
    }

    [Fact]
    public async Task ShouldUpdateCaseCollaborators()
    {
        var caseId = Guid.NewGuid();
        var collaborators = new List<int> { 1, 2, 3 };

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/collaborators").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.OK)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        await client.UpdateCaseCollaborators(caseId, collaborators);
    }

    [Theory]
    [InlineData(HttpStatusCode.NotFound, "Response status code does not indicate success: 404 (Not Found).")]
    [InlineData(HttpStatusCode.BadRequest, "Response status code does not indicate success: 400 (Bad Request).")]
    public async Task ShouldThrowExceptionWhenUpdateCaseCollaboratorsFails(HttpStatusCode statusCode, string expectedMessage)
    {
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/collaborators").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(statusCode)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var action = async () => await client.UpdateCaseCollaborators(caseId, new List<int> { 1, 2, 3 });
        await action.Should().ThrowAsync<HttpRequestException>().WithMessage(expectedMessage);
    }
}