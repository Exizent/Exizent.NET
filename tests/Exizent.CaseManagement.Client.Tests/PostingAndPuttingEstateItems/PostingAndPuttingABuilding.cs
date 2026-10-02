using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

public sealed class PostingAndPuttingABuilding : IClassFixture<Harness>
{
    private static readonly Guid CaseItemId = Guid.Parse("5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f");
    private static readonly Guid JointOwnerId = Guid.Parse("9a1b2c3d-4e5f-4a6b-8c7d-0e1f2a3b4c5d");

    private const string ExpectedBody = @"{
        ""address"": {
            ""streetName"": ""High Street"",
            ""buildingNumber"": ""12"",
            ""buildingNameOrFlatNumber"": ""Flat 3"",
            ""city"": ""Leeds"",
            ""postcode"": ""LS1 1AA""
        },
        ""residenceType"": ""Terraced house"",
        ""conveyancingDescription"": ""Freehold house and garden"",
        ""isMainResidence"": true,
        ""isVacant"": false,
        ""isRented"": true,
        ""proprietorship"": ""JointTenants"",
        ""purpose"": ""Commercial"",
        ""executorEstimatedValue"": 350000.00,
        ""surveyorFormalValue"": 375000.50,
        ""formalValuationBy"": ""A Surveyor"",
        ""hasAdvisedInsurance"": true,
        ""hasAdvisedUtilities"": false,
        ""hasAdvisedCouncil"": true,
        ""hasAdvisedCommsSuppliers"": false,
        ""containsMoveableItems"": true,
        ""proportionOwned"": 0.5,
        ""isValidForInheritanceTax"": false,
        ""grossSaleProceeds"": 380000.75,
        ""isFarmOrFarmhouse"": false,
        ""isFreehold"": true,
        ""lengthOfLease"": 125,
        ""annualRent"": 1200.00,
        ""dateTenancyBegan"": ""2020-01-01T00:00:00"",
        ""dateTenancyEnds"": ""2030-12-31T00:00:00"",
        ""monthlyRent"": 100.00,
        ""agriculturalBusinessOrHeritageReliefExemption"": true,
        ""heritageExemptionValue"": 1000.00,
        ""woodlandsReliefValue"": 2000.00,
        ""isSubjectToSpecialFactors"": true,
        ""specialFactorsDescription"": ""Subsidence"",
        ""isCharityDonation"": false,
        ""isClaimingResidenceNilRateBand"": true,
        ""isHeritable"": true,
        ""realisation"": {
            ""receivedAt"": ""2024-03-01T00:00:00"",
            ""value"": 370000.25,
            ""destination"": ""BankAccount"",
            ""otherDestination"": ""Somewhere else"",
            ""caseItemId"": ""5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f""
        },
        ""jointOwnerIds"": [""9a1b2c3d-4e5f-4a6b-8c7d-0e1f2a3b4c5d""],
        ""agriculturalReliefValueAt100Percent"": null,
        ""businessReliefValueAt100Percent"": 3000.00,
        ""agriculturalReliefValueAt50Percent"": null,
        ""businessReliefValueAt50Percent"": 4000.00,
        ""type"": ""Building"",
        ""location"": ""EnglandWales"",
        ""notes"": ""Some notes""
    }";

    private readonly Harness _harness;

    public PostingAndPuttingABuilding(Harness harness) => _harness = harness;

    public static IEnumerable<object[]> Proprietorships() =>
        Enum.GetValues<PropertyProprietorship>().Select(p => new object[] { p });

    public static IEnumerable<object[]> Purposes() =>
        Enum.GetValues<PropertyPurpose>().Select(p => new object[] { p });

    [Fact]
    public async Task ShouldPostTheBuilding()
    {
        await Post(Populate(new PostBuildingResourceRepresentation()));

        AssertBodySent();
    }

    [Fact]
    public async Task ShouldPutTheBuilding()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId,
            Populate(new PutBuildingResourceRepresentation()));

        AssertBodySent();
    }

    [Theory]
    [MemberData(nameof(Proprietorships))]
    public async Task ShouldSendTheProprietorshipByName(PropertyProprietorship proprietorship)
    {
        await Post(new PostBuildingResourceRepresentation { Proprietorship = proprietorship });

        SentBody()["proprietorship"]!.GetValue<string>().Should().Be(proprietorship.ToString());
    }

    [Theory]
    [MemberData(nameof(Purposes))]
    public async Task ShouldSendThePurposeByName(PropertyPurpose purpose)
    {
        await Post(new PostBuildingResourceRepresentation { Purpose = purpose });

        SentBody()["purpose"]!.GetValue<string>().Should().Be(purpose.ToString());
    }

    /// <summary>
    /// Neither enum has a zero member, so an unset value must go as null for the API to reject it as
    /// required, rather than as a number it cannot bind.
    /// </summary>
    [Fact]
    public async Task ShouldSendAnUnsetProprietorshipAndPurposeAsNull()
    {
        await Post(new PostBuildingResourceRepresentation());

        var body = SentBody();
        using var _ = new AssertionScope();
        body.ContainsKey("proprietorship").Should().BeTrue();
        body["proprietorship"].Should().BeNull();
        body.ContainsKey("purpose").Should().BeTrue();
        body["purpose"].Should().BeNull();
    }

    /// <summary>The API owns the Zoopla estimate, so the client never sends one.</summary>
    [Fact]
    public async Task ShouldNotSendAZooplaEstimatedValue()
    {
        await Post(Populate(new PostBuildingResourceRepresentation()));

        SentBody().ContainsKey("zooplaEstimatedValue").Should().BeFalse();
    }

    private async Task Post(PostBuildingResourceRepresentation building)
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId, building);
    }

    private static T Populate<T>(T building) where T : BuildingResourceRepresentationBase
    {
        building.Location = Location.EnglandWales;
        building.Notes = "Some notes";
        building.Address = new AddressResourceRepresentation
        {
            BuildingNameOrFlatNumber = "Flat 3",
            BuildingNumber = "12",
            StreetName = "High Street",
            City = "Leeds",
            Postcode = "LS1 1AA"
        };
        building.ResidenceType = "Terraced house";
        building.ConveyancingDescription = "Freehold house and garden";
        building.IsMainResidence = true;
        building.IsVacant = false;
        building.IsRented = true;
        building.Proprietorship = PropertyProprietorship.JointTenants;
        building.Purpose = PropertyPurpose.Commercial;
        building.ExecutorEstimatedValue = 350000.00m;
        building.SurveyorFormalValue = 375000.50m;
        building.FormalValuationBy = "A Surveyor";
        building.HasAdvisedInsurance = true;
        building.HasAdvisedUtilities = false;
        building.HasAdvisedCouncil = true;
        building.HasAdvisedCommsSuppliers = false;
        building.ContainsMoveableItems = true;
        building.ProportionOwned = 0.5m;
        building.IsValidForInheritanceTax = false;
        building.GrossSaleProceeds = 380000.75m;
        building.IsFarmOrFarmhouse = false;
        building.IsFreehold = true;
        building.LengthOfLease = 125;
        building.AnnualRent = 1200.00m;
        building.DateTenancyBegan = new DateTime(2020, 1, 1);
        building.DateTenancyEnds = new DateTime(2030, 12, 31);
        building.MonthlyRent = 100.00m;
        building.AgriculturalBusinessOrHeritageReliefExemption = true;
        building.HeritageExemptionValue = 1000.00m;
        building.WoodlandsReliefValue = 2000.00m;
        building.IsSubjectToSpecialFactors = true;
        building.SpecialFactorsDescription = "Subsidence";
        building.IsCharityDonation = false;
        building.IsClaimingResidenceNilRateBand = true;
        building.IsHeritable = true;
        building.Realisation = new EstateItemRealisationResourceRepresentation
        {
            ReceivedAt = new DateTime(2024, 3, 1),
            Value = 370000.25m,
            Destination = RealisationDestination.BankAccount,
            OtherDestination = "Somewhere else",
            CaseItemId = CaseItemId
        };
        building.JointOwnerIds = new[] { JointOwnerId };
        building.BusinessReliefValueAt100Percent = 3000.00m;
        building.BusinessReliefValueAt50Percent = 4000.00m;

        return building;
    }

    private JsonObject SentBody() => JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.AsObject();

    private void AssertBodySent() =>
        SentBody().ToJsonString().Should().Be(JsonNode.Parse(ExpectedBody)!.ToJsonString());
}
