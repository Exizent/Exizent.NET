using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class GettingDocumentUrl: IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingDocumentUrl(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnDocumentUrl()
    {
        var caseId = Guid.NewGuid();
        var url = "develop/1/2/Assets/12345/Document.csv?token=ddsadsasd3ewfsgrg";

        var body = url;

        _harness.ClientHandler.AddGetDocumentUrlResponse(caseId, "DocumentKey", body);

        var documentUrl = await _harness.Client.GetDocumentUrl(caseId, "DocumentKey");

        using var _ = new AssertionScope();
        documentUrl.Should().NotBeNull();
        documentUrl.Should().Be(url);
    }
    
    [Fact]
    public async Task ShouldReturnEstateItemDocumentUploadUrl()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        var url = "develop/1/2/Assets/12345/Document.csv?token=ddsadsasd3ewfsgrg";

        var body = url;

        _harness.ClientHandler.AddGetDocumentUploadUrlResponse(caseId, estateItemId,"Document.csv", body);

        var documentUrl = await _harness.Client.GetDocumentUploadUrl(caseId, estateItemId,"Document.csv");

        using var _ = new AssertionScope();
        documentUrl.Should().NotBeNull();
        documentUrl.Should().Be(url);
    }
        
    [Fact]
    public async Task ShouldReturnCaseDocumentUploadUrl()
    {
        var caseId = Guid.NewGuid();
        var url = "develop/1/2/Executor-LoA/Document.csv?token=ddsadsasd3ewfsgrg";

        var body = url;

        _harness.ClientHandler.AddGetDocumentUploadUrlResponse(caseId, DocumentType.ExecutorsLetterOfAdministration, "Document.csv", body);

        var documentUrl = await _harness.Client.GetDocumentUploadUrl(caseId, DocumentType.ExecutorsLetterOfAdministration,"Document.csv");

        using var _ = new AssertionScope();
        documentUrl.Should().NotBeNull();
        documentUrl.Should().Be(url);
    }
}