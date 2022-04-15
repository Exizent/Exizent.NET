using AutoFixture;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithDeceased : IDisposable
{
    private readonly Fixture _fixture = new();
    private readonly TestHttpClientHandler _httpClientHandler;
    private readonly ICaseManagementApiClient _client;

    public GettingACaseWithDeceased()
    {
        _httpClientHandler = new TestHttpClientHandler();
        _client = new CaseManagementApiClient(new HttpClient(_httpClientHandler)
        {
            BaseAddress = new Uri("https://testing.com")
        });
    }

    [Fact]
    public async Task ShouldReturnNullWhen404NotFound()
    {
        var caseId = Guid.NewGuid();

        var caseDetails = await _client.GetCase(caseId);
        caseDetails.Should().BeNull();
    }

    [Fact]
    public async Task ShouldReturnACaseWithDeceasedDetails()
    {
        
        var expectedDeceased = _fixture.Create<DeceasedResourceRepresentation>();

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedDeceased)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Deceased.Should().BeEquivalentTo(caseResourceRepresentation.Deceased);
    }

    public void Dispose()
    {
        _httpClientHandler.Dispose();
    }
}