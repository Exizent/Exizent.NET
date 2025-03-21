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

public class PostingEstateItemTests
{
    [Fact]
    public async Task ShouldPostEstateItem()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/estateitems").UsingPost()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""id"": ""{estateItemId}""}}")
                .WithHeader("Authorization", "Bearer 123456")
                .WithStatusCode(HttpStatusCode.Created)
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var response = await client.PostEstateItem(caseId,
            new PostIncomeBondResourceRepresentation { AccountNumber = "12344" });
        response!.StatusCode.Should().Be(HttpStatusCode.Created);
        response!.Id.Should().Be(estateItemId);
    }
    
    [Fact]
    public async Task ShouldPostEstateItemAndReturnBadRequest()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/estateitems").UsingPost()
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

        var response = await client.PostEstateItem(caseId,
            new PostIncomeBondResourceRepresentation { AccountNumber = "12344" });
        response.Should().BeOfType<EstateItemBadRequestResourceRepresentation>();
        var errorResponse = (EstateItemBadRequestResourceRepresentation)response!;
        errorResponse!.Id.Should().Be(Guid.Empty);
        errorResponse!.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        errorResponse!.Body.Should().Be(@$"{{ ""Errors"": ""Input data had errors""}}");
    }

}