using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

public sealed class PostingAndPuttingCash : IClassFixture<Harness>
{
    private static readonly Guid JointOwnerId = Guid.Parse("4b0c7c39-2f0e-4d4e-9d3c-6f1f0f6a2e11");

    private static readonly string[] ExpectedKeys =
    {
        "type", "location", "notes", "proportionOwned", "isPassedToSurvivingJointOwner", "notPassedDetails",
        "jointOwnerIds", "address", "description", "value", "isValidForInheritanceTax", "isCharityDonation",
        "realisation"
    };

    private readonly Harness _harness;

    public PostingAndPuttingCash(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldPostCash()
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId, Populate(new PostCashResourceRepresentation()));

        AssertCashSent();
    }

    [Fact]
    public async Task ShouldPutCash()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId, Populate(new PutCashResourceRepresentation()));

        AssertCashSent();
    }

    private static T Populate<T>(T cash) where T : CashResourceRepresentationBase
    {
        cash.Location = Location.Scotland;
        cash.Notes = "Kept in a safe";
        cash.ProportionOwned = 50m;
        cash.IsPassedToSurvivingJointOwner = false;
        cash.NotPassedDetails = "Severed tenancy";
        cash.JointOwnerIds = new[] { JointOwnerId };
        cash.Address = new AddressResourceRepresentation { Postcode = "LS1 1AA" };
        cash.Description = "Cash at home";
        cash.Value = 1234.56m;
        cash.IsValidForInheritanceTax = true;
        cash.IsCharityDonation = true;
        cash.Realisation = new EstateItemRealisationResourceRepresentation();
        return cash;
    }

    private void AssertCashSent()
    {
        var body = JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.AsObject();

        using var _ = new AssertionScope();
        body.Select(p => p.Key).Should().BeEquivalentTo(ExpectedKeys);
        body["type"]!.GetValue<string>().Should().Be("Cash");
        body["location"]!.GetValue<string>().Should().Be("Scotland");
        body["notes"]!.GetValue<string>().Should().Be("Kept in a safe");
        body["proportionOwned"]!.GetValue<decimal>().Should().Be(50m);
        body["isPassedToSurvivingJointOwner"]!.GetValue<bool>().Should().BeFalse();
        body["notPassedDetails"]!.GetValue<string>().Should().Be("Severed tenancy");
        body["jointOwnerIds"]!.AsArray().Select(x => x!.GetValue<Guid>()).Should().Equal(JointOwnerId);
        body["address"]!["postcode"]!.GetValue<string>().Should().Be("LS1 1AA");
        body["description"]!.GetValue<string>().Should().Be("Cash at home");
        body["value"]!.GetValue<decimal>().Should().Be(1234.56m);
        body["isValidForInheritanceTax"]!.GetValue<bool>().Should().BeTrue();
        body["isCharityDonation"]!.GetValue<bool>().Should().BeTrue();
        body["realisation"].Should().BeOfType<JsonObject>();
    }
}
