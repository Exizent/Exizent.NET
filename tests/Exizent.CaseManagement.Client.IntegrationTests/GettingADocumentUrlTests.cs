using Exizent.CaseManagement.Client.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using WireMock.Matchers;
using WireMock.Server;
using Xunit;
using Request = WireMock.RequestBuilders.Request;
using Response = WireMock.ResponseBuilders.Response;

namespace Exizent.CaseManagement.Client.IntegrationTests;

public class GettingADocumentUrlTests
{
    [Fact]
    public async Task ShouldReturnDocumentUrl()
    {
        var documentKey = Guid.NewGuid().ToString();
        var caseId = Guid.NewGuid();
        var expectedDocumentUrl = "/develop/1/2/Assets/2121332?token=3fdwrfsdww3234hgf654";

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/documents/{documentKey}/url").UsingGet()
                    .WithHeader("User-Agent", "My browser")
                )
            .RespondWith(Response.Create()
                .WithBody(expectedDocumentUrl)
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var documentUrl = await client.GetDocumentUrl(caseId, documentKey);
        documentUrl.Should().Be(expectedDocumentUrl);
    }
    
    [Fact]
    public async Task ShouldReturnAnUploadUrlForEstateItem()
    {
       var estateItemId = Guid.NewGuid();
        var caseId = Guid.NewGuid();
        var expectedDocumentUrl = $"/develop/1/2/Assets/{estateItemId}/document.pdf?token=3fdwrfsdww3234hgf654";

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/documents/uploadurl")
                    .UsingGet()
                    .WithParam("documentType", MatchBehaviour.AcceptOnMatch, true, "Asset")
                    .WithParam("fileName", MatchBehaviour.AcceptOnMatch, true, "Document.pdf")
                    .WithParam("estateItemId", MatchBehaviour.AcceptOnMatch, true, estateItemId.ToString())
                    .WithHeader("User-Agent", "My browser")
                )
            .RespondWith(Response.Create()
                .WithBody(expectedDocumentUrl)
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var documentUrl = await client.GetDocumentUploadUrl(caseId, estateItemId, "Document.pdf");
        documentUrl.Should().Be(expectedDocumentUrl);
    }
    
    [Fact]
    public async Task ShouldReturnAnUploadUrlForCase()
    {
        var estateItemId = Guid.NewGuid();
        var caseId = Guid.NewGuid();
        var expectedDocumentUrl = $"/develop/1/2/Executor=LoA/document.pdf?token=3fdwrfsdww3234hgf654";

        using var authApiServer = MockAuthApiServerProvider.Create(out var clientId, out var clientSecret);

        using var casesApiServer = WireMockServer.Start();
        casesApiServer.Given(
                Request.Create().WithPath($"/cases/{caseId}/documents/uploadurl")
                    .UsingGet()
                    .WithParam("documentType", MatchBehaviour.AcceptOnMatch, true, DocumentType.ExecutorsLetterOfAdministration.ToString())
                    .WithParam("fileName", MatchBehaviour.AcceptOnMatch, true, "Document.pdf")
                    .WithHeader("User-Agent", "My browser")
                )
            .RespondWith(Response.Create()
                .WithBody(expectedDocumentUrl)
                .WithHeader("Authorization", "Bearer 123456")
            );

        var serviceContainer = ServiceCollectionBuilder
            .Create(authApiServer, casesApiServer, clientId, clientSecret, true);
        
        await using var serviceProvider = serviceContainer.BuildServiceProvider();
        using var scope = serviceProvider.CreateScope();
        var client = scope.ServiceProvider.GetRequiredService<ICaseManagementApiClient>();

        var documentUrl = await client.GetDocumentUploadUrl(caseId, DocumentType.ExecutorsLetterOfAdministration, "Document.pdf");
        documentUrl.Should().Be(expectedDocumentUrl);
    }    
}