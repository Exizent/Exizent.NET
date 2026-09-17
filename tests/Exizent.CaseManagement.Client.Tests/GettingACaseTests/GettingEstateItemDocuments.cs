using System.Net;
using System.Text.Json;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class GettingEstateItemDocuments : IClassFixture<Harness>
{
    private const int PageSize = 100;

    private readonly Harness _harness;

    public GettingEstateItemDocuments(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnTheDocumentsOnASinglePage()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        var documentId = Guid.NewGuid();

        _harness.ClientHandler.AddGetEstateItemDocumentsResponse(caseId, estateItemId, 1, Page(
            new EstateItemDocumentResourceRepresentation
            {
                Id = documentId,
                FileName = "valuation.pdf",
                ContentType = "application/pdf",
                SizeBytes = 2048,
                Status = EstateItemDocumentStatus.Clean
            }));

        var documents = await _harness.Client.GetEstateItemDocuments(caseId, estateItemId);

        using var _ = new AssertionScope();
        documents.Should().HaveCount(1);
        documents[0].Id.Should().Be(documentId);
        // The uploaded name, not the last segment of the storage key, which for a document the Cases API
        // wrote is the document's id.
        documents[0].FileName.Should().Be("valuation.pdf");
        documents[0].ContentType.Should().Be("application/pdf");
        documents[0].SizeBytes.Should().Be(2048);
        documents[0].Status.Should().Be(EstateItemDocumentStatus.Clean);
    }

    [Fact]
    public async Task ShouldPageUntilAShortPageEndsIt()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        // A full page has to be followed by another request; the short page after it is what stops the loop.
        _harness.ClientHandler.AddGetEstateItemDocumentsResponse(caseId, estateItemId, 1, Page(FullPage()));
        _harness.ClientHandler.AddGetEstateItemDocumentsResponse(caseId, estateItemId, 2,
            Page(Document("last.pdf")));

        var documents = await _harness.Client.GetEstateItemDocuments(caseId, estateItemId);

        using var _ = new AssertionScope();
        documents.Should().HaveCount(PageSize + 1);
        documents[^1].FileName.Should().Be("last.pdf");
        _harness.ClientHandler.Requests.Should()
            .Contain(("GET", TestHttpClientHandler.EstateItemDocumentsUrl(caseId, estateItemId, 1)))
            .And.Contain(("GET", TestHttpClientHandler.EstateItemDocumentsUrl(caseId, estateItemId, 2)));
    }

    [Fact]
    public async Task ShouldStopOnAnExactlyFullFinalPage()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        // The boundary: a full page means "ask again", and the empty page that follows ends it rather than
        // the client deciding a full page was the last one.
        _harness.ClientHandler.AddGetEstateItemDocumentsResponse(caseId, estateItemId, 1, Page(FullPage()));
        _harness.ClientHandler.AddGetEstateItemDocumentsResponse(caseId, estateItemId, 2, "[]");

        var documents = await _harness.Client.GetEstateItemDocuments(caseId, estateItemId);

        documents.Should().HaveCount(PageSize);
    }

    [Fact]
    public async Task ShouldReturnEmptyWhenTheEstateItemIsNotFound()
    {
        // Nothing registered, so the handler answers 404 — the same shape as a case or item that is gone.
        var documents = await _harness.Client.GetEstateItemDocuments(Guid.NewGuid(), Guid.NewGuid());

        documents.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldThrowRatherThanTruncateWhenPagingNeverEnds()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();

        // A server repeating a full page forever. Returning what had been collected would hand the caller
        // thousands of duplicates that still looked like a complete, successful read.
        for (var pageId = 1; pageId <= 101; pageId++)
        {
            _harness.ClientHandler.AddGetEstateItemDocumentsResponse(caseId, estateItemId, pageId,
                Page(FullPage()));
        }

        var act = async () => await _harness.Client.GetEstateItemDocuments(caseId, estateItemId);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"*{estateItemId}*did not finish paging*");
    }

    private static EstateItemDocumentResourceRepresentation Document(string fileName) =>
        new()
        {
            Id = Guid.NewGuid(),
            FileName = fileName,
            ContentType = "application/pdf",
            Status = EstateItemDocumentStatus.Clean
        };

    private static EstateItemDocumentResourceRepresentation[] FullPage() =>
        Enumerable.Range(0, PageSize).Select(i => Document($"document-{i}.pdf")).ToArray();

    private static string Page(params EstateItemDocumentResourceRepresentation[] documents) =>
        JsonSerializer.Serialize(documents, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() }
        });
}
