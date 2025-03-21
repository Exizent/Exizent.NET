using Exizent.CaseManagement.Client.Models;
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
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}").UsingGet()
                    .WithHeader("User-Agent", "My browser")
            )
            .RespondWith(Response.Create()
                .WithBody(
                    @$"{{ ""id"": ""{caseId}"", ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }} }}")
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

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
    public async Task ShouldReturnCaseOverload2()
    {
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}").UsingGet())
            .RespondWith(Response.Create()
                .WithBody(
                    @$"{{ ""id"": ""{caseId}"", ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }} }}")
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var @case = await client.GetCase(caseId, 0);
        @case!.Id.Should().Be(caseId);
        @case.Deceased.FirstName.Should().Be("Foo");
        @case.Deceased.LastName.Should().Be("Bar");
        @case.Deceased.DateOfDeath.Should().Be(new DateTime(1988, 02, 01));

        @case.Company.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnCaseWithCompany()
    {
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}").WithParam("expand", "company").UsingGet())
            .RespondWith(Response.Create()
                .WithBody(
                    @$"{{ ""id"": ""{caseId}"", ""company"": {{ ""id"": 1, ""name"": ""Google"", ""officePhoneNumber"": ""07711123456"" }}, ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }} }}")
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var @case = await client.GetCase(caseId, new GetCaseOptions { ExpandCompany = true });
        @case!.Id.Should().Be(caseId);
        @case.Deceased.FirstName.Should().Be("Foo");
        @case.Deceased.LastName.Should().Be("Bar");
        @case.Deceased.DateOfDeath.Should().Be(new DateTime(1988, 02, 01));

        @case.Company!.Name.Should().Be("Google");
        @case.Company!.OfficePhoneNumber.Should().Be("07711123456");
    }

    [Theory]
    [InlineData(true, false, EstateItemsFilter.Complete)]
    [InlineData(false, true, EstateItemsFilter.Archived)]
    [InlineData(true, true, EstateItemsFilter.AllAssets)]
    [InlineData(false, false, EstateItemsFilter.Open)]
    public async Task ShouldReturnCaseWithEstateItems(bool isComplete, bool isArchived, EstateItemsFilter estateItemsFilter)
    {
        var caseId = Guid.NewGuid();

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}").WithParam("estateItemsFilter",
                    estateItemsFilter.ToString())
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(
                    @$"{{ ""id"": ""{caseId}"", ""company"": {{ ""id"": 1, ""name"": ""Google"", ""officePhoneNumber"": ""07711123456"" }}, ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }},""estateItems"": [ {{ ""id"": ""{Guid.NewGuid()}"", ""account"": ""4444"", ""product"": ""1"", ""type"": ""NationalSavingsAndInvestmentsProduct"", ""isComplete"": {isComplete.ToString().ToLowerInvariant()}, ""isArchived"": {isArchived.ToString().ToLowerInvariant()} }} ] }}")
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var @case = await client.GetCase(caseId, new GetCaseOptions { EstateItemsFilter = estateItemsFilter });
        @case!.Id.Should().Be(caseId);
        @case.EstateItems.Count.Should().Be(1);
        @case.EstateItems[0].IsArchived.Should().Be(isArchived);
        @case.EstateItems[0].IsComplete.Should().Be(isComplete);
    }
    
    [Theory]
    [InlineData(true, false, EstateItemsFilter.Complete)]
    [InlineData(false, true, EstateItemsFilter.Archived)]
    [InlineData(true, true, EstateItemsFilter.AllAssets)]
    [InlineData(false, false, EstateItemsFilter.Open)]
    public async Task ShouldReturnCaseWithCompanyAndEstateItems(bool isComplete, bool isArchived, EstateItemsFilter estateItemsFilter)
    {
        var caseId = Guid.NewGuid();
        
        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(Request.Create().WithPath($"/cases/{caseId}").WithParam("estateItemsFilter",
                    estateItemsFilter.ToString())
                .UsingGet())
            .RespondWith(Response.Create()
                .WithBody(
                    @$"{{ ""id"": ""{caseId}"", ""company"": {{ ""id"": 1, ""name"": ""Google"", ""officePhoneNumber"": ""07711123456"" }}, ""deceased"": {{ ""firstName"": ""Foo"", ""lastName"": ""Bar"", ""dateOfDeath"": ""1988-02-01"" }},""estateItems"": [ {{ ""id"": ""{Guid.NewGuid()}"", ""account"": ""4444"", ""product"": ""1"", ""type"": ""NationalSavingsAndInvestmentsProduct"", ""isComplete"": {isComplete.ToString().ToLowerInvariant() }, ""isArchived"": {isArchived.ToString().ToLowerInvariant() } }} ] }}")
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret);

        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var @case = await client.GetCase(caseId, new GetCaseOptions { ExpandCompany = true, EstateItemsFilter = estateItemsFilter });
        @case!.Id.Should().Be(caseId);
        @case.Deceased.FirstName.Should().Be("Foo");
        @case.Deceased.LastName.Should().Be("Bar");
        @case.Deceased.DateOfDeath.Should().Be(new DateTime(1988, 02, 01));

        @case.Company!.Name.Should().Be("Google");
        @case.Company!.OfficePhoneNumber.Should().Be("07711123456");
        @case.EstateItems.Count.Should().Be(1);
        @case.EstateItems[0].IsArchived.Should().Be(isArchived);
        @case.EstateItems[0].IsComplete.Should().Be(isComplete);
    }
}