using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

public sealed class PostingAndPuttingAnIncomeBond : IClassFixture<Harness>
{
    private static readonly Guid CaseItemId = Guid.Parse("5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f");
    private static readonly Guid JointOwnerId = Guid.Parse("0b7e3c1a-9d24-4f6b-8a51-2c3d4e5f6a7b");

    private const string ExpectedBody = @"{
        ""address"": {
            ""streetName"": ""High Street"",
            ""buildingNumber"": ""12"",
            ""buildingNameOrFlatNumber"": ""Flat 3"",
            ""city"": ""Leeds"",
            ""postcode"": ""LS1 1AA""
        },
        ""institution"": ""Acme Building Society"",
        ""accountNumber"": ""IB-123456"",
        ""confirmedBalance"": 25000.50,
        ""estimatedBalance"": 24000.25,
        ""interestUpToDateOfDeath"": 125.75,
        ""isNationalSavingsAndInvestments"": false,
        ""proportionOwned"": 0.5,
        ""isPassedToSurvivingJointOwner"": false,
        ""notPassedDetails"": ""Held as tenants in common"",
        ""realisation"": {
            ""receivedAt"": ""2024-03-01T00:00:00"",
            ""value"": 25126.25,
            ""destination"": ""BankAccount"",
            ""otherDestination"": ""Somewhere else"",
            ""caseItemId"": ""5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f""
        },
        ""jointOwnerIds"": [""0b7e3c1a-9d24-4f6b-8a51-2c3d4e5f6a7b""],
        ""type"": ""IncomeBond"",
        ""location"": ""Scotland"",
        ""notes"": ""Some notes""
    }";

    private readonly Harness _harness;

    public PostingAndPuttingAnIncomeBond(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldPostTheIncomeBond()
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId, Populate(new PostIncomeBondResourceRepresentation()));

        AssertBodySent();
    }

    [Fact]
    public async Task ShouldPutTheIncomeBond()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId, Populate(new PutIncomeBondResourceRepresentation()));

        AssertBodySent();
    }

    private static T Populate<T>(T incomeBond) where T : IncomeBondResourceRepresentationBase
    {
        incomeBond.Location = Location.Scotland;
        incomeBond.Notes = "Some notes";
        incomeBond.Address = new AddressResourceRepresentation
        {
            BuildingNameOrFlatNumber = "Flat 3",
            BuildingNumber = "12",
            StreetName = "High Street",
            City = "Leeds",
            Postcode = "LS1 1AA"
        };
        incomeBond.Institution = "Acme Building Society";
        incomeBond.AccountNumber = "IB-123456";
        incomeBond.ConfirmedBalance = 25000.50m;
        incomeBond.EstimatedBalance = 24000.25m;
        incomeBond.InterestUpToDateOfDeath = 125.75m;
        incomeBond.IsNationalSavingsAndInvestments = false;
        incomeBond.ProportionOwned = 0.5m;
        incomeBond.IsPassedToSurvivingJointOwner = false;
        incomeBond.NotPassedDetails = "Held as tenants in common";
        incomeBond.Realisation = new EstateItemRealisationResourceRepresentation
        {
            ReceivedAt = new DateTime(2024, 3, 1),
            Value = 25126.25m,
            Destination = RealisationDestination.BankAccount,
            OtherDestination = "Somewhere else",
            CaseItemId = CaseItemId
        };
        incomeBond.JointOwnerIds = new[] { JointOwnerId };

        return incomeBond;
    }

    private void AssertBodySent() =>
        JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.ToJsonString()
            .Should().Be(JsonNode.Parse(ExpectedBody)!.ToJsonString());
}
