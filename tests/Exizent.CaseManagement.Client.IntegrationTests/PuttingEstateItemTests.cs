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

public class PuttingEstateItemTests
{
    [Fact]
    public async Task ShouldUpdateEstateItem()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}").UsingPut()
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

        var response = await client.PutEstateItem(caseId, estateItemId,
            new PostIncomeBondResourceRepresentation { AccountNumber = "12344" });
        response!.Id.Should().Be(estateItemId);
    }
    
    [Fact]
    public async Task ShouldPutEstateItemAndReturnBadRequest()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}").UsingPut()
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

        var response = await client.PutEstateItem(caseId, estateItemId,
            new PostIncomeBondResourceRepresentation { AccountNumber = "12344" });
        response.Should().BeOfType<EstateItemBadRequestResourceRepresentation>();
        var errorResponse = (EstateItemBadRequestResourceRepresentation)response!;
        errorResponse!.Id.Should().Be(Guid.Empty);
        errorResponse!.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errorResponse!.Body.Should().Be(@$"{{ ""Errors"": ""Input data had errors""}}");

    }
    
    [Fact]
    public async Task ShouldCreateEstateItemIfEstateItemDoesNotExist()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        var createdEstateItemId = Guid.NewGuid();
        
        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}").UsingPut()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""id"": ""{createdEstateItemId}""}}")
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.Created)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var response = await client.PutEstateItem(caseId, estateItemId,
            new PostIncomeBondResourceRepresentation {AccountNumber = "12344" });
        response!.Id.Should().Be(createdEstateItemId);
    }
}