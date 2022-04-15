using AutoFixture;
using Exizent.CaseManagement.Client.Models.People;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithPeople : IDisposable
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
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.People.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnPerson()
    {
        var expectedPerson = _fixture.Create<PersonResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedPerson)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.People.Single().Should()
            .BeEquivalentTo(expectedPerson);
    }

    public void Dispose()
    {
        _httpClientHandler.Dispose();
    }
}