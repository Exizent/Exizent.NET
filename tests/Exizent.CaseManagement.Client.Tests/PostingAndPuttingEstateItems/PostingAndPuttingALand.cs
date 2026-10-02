using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

public sealed class PostingAndPuttingALand : IClassFixture<Harness>
{
    private static readonly Guid CaseItemId = Guid.Parse("5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f");

    private const string ExpectedBody = @"{
        ""landType"": ""Agricultural"",
        ""landRegistryNumber"": ""WYK123456"",
        ""conveyancingDescription"": ""Field to the north of the farmhouse"",
        ""isMainResidence"": false,
        ""isVacant"": true,
        ""isRented"": true,
        ""purpose"": ""Residential"",
        ""proprietorship"": ""SoleOwnership"",
        ""executorEstimatedValue"": 250000.00,
        ""surveyorFormalValue"": 275000.50,
        ""formalValuationBy"": ""A Surveyor"",
        ""hasAdvisedInsurance"": true,
        ""hasAdvisedCouncil"": false,
        ""proportionOwned"": 1,
        ""isValidForInheritanceTax"": false,
        ""grossSaleProceeds"": 280000.75,
        ""isFarmOrFarmhouse"": true,
        ""isFreehold"": false,
        ""lengthOfLease"": 99,
        ""annualRent"": 1200.00,
        ""dateTenancyBegan"": ""2020-01-01T00:00:00"",
        ""dateTenancyEnds"": ""2030-12-31T00:00:00"",
        ""monthlyRent"": 100.00,
        ""agriculturalBusinessOrHeritageReliefExemption"": true,
        ""heritageExemptionValue"": 1000.00,
        ""woodlandsReliefValue"": 2000.00,
        ""isSubjectToSpecialFactors"": true,
        ""specialFactorsDescription"": ""Right of way"",
        ""isCharityDonation"": false,
        ""isClaimingResidenceNilRateBand"": false,
        ""isHeritable"": true,
        ""realisation"": {
            ""receivedAt"": ""2024-03-01T00:00:00"",
            ""value"": 270000.25,
            ""destination"": ""BankAccount"",
            ""otherDestination"": ""Somewhere else"",
            ""caseItemId"": ""5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f""
        },
        ""jointOwnerIds"": [],
        ""agriculturalReliefValueAt100Percent"": 3000.00,
        ""businessReliefValueAt100Percent"": null,
        ""agriculturalReliefValueAt50Percent"": 4000.00,
        ""businessReliefValueAt50Percent"": null,
        ""type"": ""Land"",
        ""location"": ""EnglandWales"",
        ""notes"": ""Some notes""
    }";

    private readonly Harness _harness;

    public PostingAndPuttingALand(Harness harness) => _harness = harness;

    public static IEnumerable<object[]> Proprietorships() =>
        Enum.GetValues<PropertyProprietorship>().Select(p => new object[] { p });

    public static IEnumerable<object[]> Purposes() =>
        Enum.GetValues<PropertyPurpose>().Select(p => new object[] { p });

    [Fact]
    public async Task ShouldPostTheLand()
    {
        await Post(Populate(new PostLandResourceRepresentation()));

        AssertBodySent();
    }

    [Fact]
    public async Task ShouldPutTheLand()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId, Populate(new PutLandResourceRepresentation()));

        AssertBodySent();
    }

    [Theory]
    [MemberData(nameof(Proprietorships))]
    public async Task ShouldSendTheProprietorshipByName(PropertyProprietorship proprietorship)
    {
        await Post(new PostLandResourceRepresentation { Proprietorship = proprietorship });

        SentBody()["proprietorship"]!.GetValue<string>().Should().Be(proprietorship.ToString());
    }

    [Theory]
    [MemberData(nameof(Purposes))]
    public async Task ShouldSendThePurposeByName(PropertyPurpose purpose)
    {
        await Post(new PostLandResourceRepresentation { Purpose = purpose });

        SentBody()["purpose"]!.GetValue<string>().Should().Be(purpose.ToString());
    }

    /// <summary>
    /// Neither enum has a zero member, so an unset value must not go as a number the API cannot bind.
    /// </summary>
    [Fact]
    public async Task ShouldSendDefaultsForAnUnsetProprietorshipAndPurpose()
    {
        await Post(new PostLandResourceRepresentation());

        var body = SentBody();
        using var _ = new AssertionScope();
        body["proprietorship"]!.GetValue<string>().Should().Be(nameof(PropertyProprietorship.SoleOwnership));
        body["purpose"]!.GetValue<string>().Should().Be(nameof(PropertyPurpose.Residential));
    }

    [Fact]
    public async Task ShouldSendEmptyStringsForUnsetText()
    {
        await Post(new PostLandResourceRepresentation());

        var body = SentBody();
        using var _ = new AssertionScope();
        body["landType"]!.GetValue<string>().Should().BeEmpty();
        body["landRegistryNumber"]!.GetValue<string>().Should().BeEmpty();
        body["conveyancingDescription"]!.GetValue<string>().Should().BeEmpty();
    }

    private async Task Post(PostLandResourceRepresentation land)
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId, land);
    }

    private static T Populate<T>(T land) where T : LandResourceRepresentationBase
    {
        land.Location = Location.EnglandWales;
        land.Notes = "Some notes";
        land.LandType = "Agricultural";
        land.LandRegistryNumber = "WYK123456";
        land.ConveyancingDescription = "Field to the north of the farmhouse";
        land.IsMainResidence = false;
        land.IsVacant = true;
        land.IsRented = true;
        land.Purpose = PropertyPurpose.Residential;
        land.Proprietorship = PropertyProprietorship.SoleOwnership;
        land.ExecutorEstimatedValue = 250000.00m;
        land.SurveyorFormalValue = 275000.50m;
        land.FormalValuationBy = "A Surveyor";
        land.HasAdvisedInsurance = true;
        land.HasAdvisedCouncil = false;
        land.ProportionOwned = 1;
        land.IsValidForInheritanceTax = false;
        land.GrossSaleProceeds = 280000.75m;
        land.IsFarmOrFarmhouse = true;
        land.IsFreehold = false;
        land.LengthOfLease = 99;
        land.AnnualRent = 1200.00m;
        land.DateTenancyBegan = new DateTime(2020, 1, 1);
        land.DateTenancyEnds = new DateTime(2030, 12, 31);
        land.MonthlyRent = 100.00m;
        land.AgriculturalBusinessOrHeritageReliefExemption = true;
        land.HeritageExemptionValue = 1000.00m;
        land.WoodlandsReliefValue = 2000.00m;
        land.IsSubjectToSpecialFactors = true;
        land.SpecialFactorsDescription = "Right of way";
        land.IsCharityDonation = false;
        land.IsClaimingResidenceNilRateBand = false;
        land.IsHeritable = true;
        land.Realisation = new EstateItemRealisationResourceRepresentation
        {
            ReceivedAt = new DateTime(2024, 3, 1),
            Value = 270000.25m,
            Destination = RealisationDestination.BankAccount,
            OtherDestination = "Somewhere else",
            CaseItemId = CaseItemId
        };
        land.JointOwnerIds = Array.Empty<Guid>();
        land.AgriculturalReliefValueAt100Percent = 3000.00m;
        land.AgriculturalReliefValueAt50Percent = 4000.00m;

        return land;
    }

    private JsonObject SentBody() => JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.AsObject();

    private void AssertBodySent() =>
        SentBody().ToJsonString().Should().Be(JsonNode.Parse(ExpectedBody)!.ToJsonString());
}
