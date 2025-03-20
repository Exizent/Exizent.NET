using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class DeletingADocumentTests
{
    [Fact]
    public async Task ShouldDeleteDocument()
    {
        var documentKey = Guid.NewGuid().ToString();
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/documents/{documentKey}")
                    .UsingDelete()
                    .WithHeader("User-Agent", "My browser")
                )
            .RespondWith(Response.Create()
                .WithStatusCode(StatusCodes.Status204NoContent)
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        await client.DeleteDocument(caseId, documentKey);
    }
    
    [Fact]
    public async Task ShouldThrowExceptionIfNotFound()
    {
        var documentKey = Guid.NewGuid().ToString();
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/documents/{documentKey}")
                    .UsingDelete()
                    .WithHeader("User-Agent", "My browser")
                )
            .RespondWith(Response.Create()
                .WithStatusCode(StatusCodes.Status404NotFound)
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        Func<Task> action = async () => await client.DeleteDocument(caseId, documentKey);

        await action.Should().ThrowAsync<HttpRequestException>().WithMessage("Response status code does not indicate success: 404 (Not Found).");
        
    }

}