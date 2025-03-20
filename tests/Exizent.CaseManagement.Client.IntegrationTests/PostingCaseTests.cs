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
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

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

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

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
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

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

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

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