using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.UpdatingACaseTests;

public sealed class UpdatingFinancialGiftsDetails : IClassFixture<Harness>
{
    private const string ExpectedBody = @"{
        ""hasMadeGiftsOverAnnualLimit"": true,
        ""hasGivenUpTrustAssetsWithinTimeLimit"": false,
        ""continuedToBenefitFromGift"": true,
        ""madeGiftWhereRecipientDidNotTakeFullPossession"": false,
        ""electedThatIhtShouldNotApplyToAssetsPreviouslyOwned"": true
    }";

    private static readonly PutFinancialGiftsDetailsResourceRepresentation Details = new()
    {
        HasMadeGiftsOverAnnualLimit = true,
        HasGivenUpTrustAssetsWithinTimeLimit = false,
        ContinuedToBenefitFromGift = true,
        MadeGiftWhereRecipientDidNotTakeFullPossession = false,
        ElectedThatIhtShouldNotApplyToAssetsPreviouslyOwned = true
    };

    private readonly Harness _harness;

    public UpdatingFinancialGiftsDetails(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldPutTheFinancialGiftsDetails()
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/financialGiftsDetails", HttpStatusCode.NoContent);

        await _harness.Client.UpdateFinancialGiftsDetails(caseId, Details);

        _harness.ClientHandler.Requests.Should().Contain(("PUT", $"/cases/{caseId}/financialGiftsDetails"));
        JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.ToJsonString()
            .Should().Be(JsonNode.Parse(ExpectedBody)!.ToJsonString());
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest, "Response status code does not indicate success: 400 (Bad Request).")]
    [InlineData(HttpStatusCode.Forbidden, "Response status code does not indicate success: 403 (Forbidden).")]
    [InlineData(HttpStatusCode.NotFound, "Response status code does not indicate success: 404 (Not Found).")]
    public async Task ShouldThrowWhenTheUpdateFails(HttpStatusCode statusCode, string expectedMessage)
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/financialGiftsDetails", statusCode);

        var act = async () => await _harness.Client.UpdateFinancialGiftsDetails(caseId, Details);

        await act.Should().ThrowAsync<HttpRequestException>().WithMessage(expectedMessage);
    }
}
