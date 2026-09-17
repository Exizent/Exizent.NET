using System.Net;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class DeletingAnEstateItemDocument : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public DeletingAnEstateItemDocument(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldDeleteTheDocument()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        var documentId = Guid.NewGuid();

        _harness.ClientHandler.AddDeleteEstateItemDocumentResponse(caseId, estateItemId, documentId,
            HttpStatusCode.NoContent);

        await _harness.Client.DeleteEstateItemDocument(caseId, estateItemId, documentId);

        _harness.ClientHandler.Requests.Should().Contain(
            ("DELETE", TestHttpClientHandler.EstateItemDocumentRoute(caseId, estateItemId, documentId)));
    }

    [Fact]
    public async Task ShouldNotThrowWhenTheDocumentIsAlreadyGone()
    {
        // Unlike the legacy DeleteDocument, which throws on 404. Deleting something that is already gone has
        // reached the state the caller asked for, and two people deleting the same row should not error.
        var act = async () =>
            await _harness.Client.DeleteEstateItemDocument(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid());

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task ShouldThrowOnAnUnexpectedFailure()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        var documentId = Guid.NewGuid();

        _harness.ClientHandler.AddDeleteEstateItemDocumentResponse(caseId, estateItemId, documentId,
            HttpStatusCode.InternalServerError);

        var act = async () => await _harness.Client.DeleteEstateItemDocument(caseId, estateItemId, documentId);

        await act.Should().ThrowAsync<HttpRequestException>();
    }
}
