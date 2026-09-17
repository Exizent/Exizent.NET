using System.Net;
using System.Text.Json;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// The endpoint answers four ways, and the caller acts differently on each — a scanning document is worth
/// retrying, an infected one never will be. These pin that mapping so it cannot quietly drift.
/// </summary>
public class GettingEstateItemDocumentUrl : IClassFixture<Harness>
{
    private const HttpStatusCode TooEarly = (HttpStatusCode)425;

    private readonly Harness _harness;

    public GettingEstateItemDocumentUrl(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnTheUrlWhenTheDocumentIsClean()
    {
        var (caseId, estateItemId, documentId) = Ids();
        const string url = "https://s3.example/develop/1/abc/EstateItems/def/doc.pdf?token=aaa";

        _harness.ClientHandler.AddGetEstateItemDocumentUrlStringResult(caseId, estateItemId, documentId, url);

        var result = await _harness.Client.GetEstateItemDocumentUrl(caseId, estateItemId, documentId);

        using var _ = new AssertionScope();
        result.Status.Should().Be(EstateItemDocumentUrlStatus.Available);
        result.Url.Should().Be(url);
    }

    [Fact]
    public async Task ShouldAskForJson()
    {
        var (caseId, estateItemId, documentId) = Ids();
        _harness.ClientHandler.AddGetEstateItemDocumentUrlStringResult(caseId, estateItemId, documentId,
            "https://s3.example/doc.pdf");

        await _harness.Client.GetEstateItemDocumentUrl(caseId, estateItemId, documentId);

        _harness.ClientHandler.LastAcceptHeader.Should().Contain("application/json",
            "the endpoint only returns JSON when the request asks for it, and the response is parsed as JSON");
    }

    [Fact]
    public async Task ShouldReportNotYetAvailableWhileScanning()
    {
        var (caseId, estateItemId, documentId) = Ids();

        _harness.ClientHandler.AddGetEstateItemDocumentUrlResponse(caseId, estateItemId, documentId, TooEarly);

        var result = await _harness.Client.GetEstateItemDocumentUrl(caseId, estateItemId, documentId);

        using var _ = new AssertionScope();
        result.Status.Should().Be(EstateItemDocumentUrlStatus.NotYetAvailable);
        result.Url.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReportUnavailableWhenInfectedOrUnscannable()
    {
        var (caseId, estateItemId, documentId) = Ids();

        _harness.ClientHandler.AddGetEstateItemDocumentUrlResponse(caseId, estateItemId, documentId,
            HttpStatusCode.Conflict);

        var result = await _harness.Client.GetEstateItemDocumentUrl(caseId, estateItemId, documentId);

        using var _ = new AssertionScope();
        result.Status.Should().Be(EstateItemDocumentUrlStatus.Unavailable);
        result.Url.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReportNotFoundWhenTheDocumentIsGone()
    {
        var (caseId, estateItemId, documentId) = Ids();

        // Nothing registered, so the handler answers 404.
        var result = await _harness.Client.GetEstateItemDocumentUrl(caseId, estateItemId, documentId);

        using var _ = new AssertionScope();
        result.Status.Should().Be(EstateItemDocumentUrlStatus.NotFound);
        result.Url.Should().BeNull();
    }

    [Fact]
    public async Task ShouldThrowOnAnUnexpectedFailure()
    {
        var (caseId, estateItemId, documentId) = Ids();

        _harness.ClientHandler.AddGetEstateItemDocumentUrlResponse(caseId, estateItemId, documentId,
            HttpStatusCode.InternalServerError);

        var act = async () => await _harness.Client.GetEstateItemDocumentUrl(caseId, estateItemId, documentId);

        await act.Should().ThrowAsync<HttpRequestException>();
    }

    private static (Guid CaseId, Guid EstateItemId, Guid DocumentId) Ids() =>
        (Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());
}
