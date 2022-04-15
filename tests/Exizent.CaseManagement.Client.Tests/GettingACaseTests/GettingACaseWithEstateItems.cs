using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class GettingACaseWithEstateItems
{
    private readonly TestHttpClientHandler _httpClientHandler;
    private readonly ICaseManagementApiClient _client;
    private readonly Fixture _fixture = new();

    public GettingACaseWithEstateItems()
    {
        _httpClientHandler = new TestHttpClientHandler();
        _client = new CaseManagementApiClient(new HttpClient(_httpClientHandler)
        {
            BaseAddress = new Uri("https://testing.com")
        });
    }

    [Fact]
    public async Task ShouldReturnEmptyEstateItems()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.EstateItems.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldReturnBankAccount()
    {
        var estateItem = _fixture.Create<BankAccountResourceRepresentation>();
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(estateItem)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _httpClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.EstateItems.Single().Should()
            .BeEquivalentTo(estateItem);
    }
}