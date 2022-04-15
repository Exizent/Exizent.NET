using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Exizent.CaseManagement.Client.Models.People;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class GettingACaseWithPeople
{
    private readonly TestHttpClientHandler _httpClientHandler;
    private readonly ICaseManagementApiClient _client;
    private readonly Fixture _fixture = new();

    public GettingACaseWithPeople()
    {
        _httpClientHandler = new TestHttpClientHandler();
        _client = new CaseManagementApiClient(new HttpClient(_httpClientHandler)
        {
            BaseAddress = new Uri("https://testing.com")
        });
    }

    [Fact]
    public async Task ShouldReturnEmptyPersonCollection()
    {
        var caseId = Guid.NewGuid();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseId);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseId);
        caseDetails.People.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnPerson()
    {
        var caseId = Guid.NewGuid();
        var expectedPerson = _fixture.Create<PersonResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedPerson)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseId, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseId);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseId);
        caseDetails.People.Single().Should()
            .BeEquivalentTo(expectedPerson);
    }
}