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

public class UpdatingCaseStatusTests
{
    [Theory]
    [InlineData(CaseStatus.Open, "\"Open\"")]
    [InlineData(CaseStatus.WithHmrc, "\"WithHmrc\"")]
    public async Task ShouldUpdateCaseStatus(CaseStatus status, string expectedBody)
    {
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/status").UsingPut()
                    .WithBody(expectedBody)
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.NoContent)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        await client.UpdateCaseStatus(caseId, status);
    }
    
    [Fact]
    public async Task ShouldThrowExceptionWhenUpdateCaseStatusFailsWith404NotFound()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/status").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.NotFound)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();
        
        Func<Task> action = async () => await client.UpdateCaseStatus(caseId, CaseStatus.Open);

        await action.Should().ThrowAsync<HttpRequestException>().WithMessage("Response status code does not indicate success: 404 (Not Found).");
    }
}