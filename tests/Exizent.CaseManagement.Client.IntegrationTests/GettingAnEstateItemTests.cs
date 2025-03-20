using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class GettingAnEstateItemTests
{
    [Fact]
    public async Task ShouldReturnEstateItem()
    {
        var estateItemId = Guid.NewGuid();
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/estateitems/{estateItemId}").UsingGet()
                    .WithHeader("User-Agent", "My browser")
                )
            .RespondWith(Response.Create()
                .WithBody(@$"{{ ""id"": ""{estateItemId}"", ""type"": ""BankAccount"" }}")
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var estateItem = await client.GetEstateItem(caseId, estateItemId);
        estateItem!.Id.Should().Be(estateItemId);
    }
}