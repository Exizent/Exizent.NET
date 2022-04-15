using System;
using System.Net.Http;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using AutoFixture;
using Exizent.CaseManagement.Client.Models;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests;

public class GettingACaseWithDeceased
{
    private readonly TestHttpClientHandler _httpClientHandler;
    private readonly ICaseManagementApiClient _client;
    private readonly Fixture _fixture = new();

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
    public async Task ShouldReturnACaseWithBasicDeceasedDetails()
    {
        var caseId = Guid.NewGuid();
        var root = new JsonObject();
        root.Add("id", caseId.ToString());
        var deceasedJson = new JsonObject();
        deceasedJson.Add("firstName", "John");
        deceasedJson.Add("lastName", "Smith");

        root.Add("deceased", deceasedJson);
        _httpClientHandler.AddGetCaseResponse(caseId, root.ToJsonString());

        var caseDetails = await _client.GetCase(caseId);
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseId);
        caseDetails.Deceased.Should().BeEquivalentTo(new
        {
            FirstName = "John",
            LastName = "Smith"
        });
    }


    [Fact]
    public async Task ShouldReturnACaseWithDeceasedDetails()
    {
        var caseId = Guid.NewGuid();
        var expectedDeceased = _fixture.Create<DeceasedResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentation
        {
            Id = caseId,
            Deceased = expectedDeceased,
        };

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseId);


        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseId);
        caseDetails.Deceased.Should().BeEquivalentTo(expectedDeceased);
    }
}