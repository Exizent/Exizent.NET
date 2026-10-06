using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

public sealed class PostingAndPuttingAPension : IClassFixture<Harness>
{
    private static readonly Guid CaseItemId = Guid.Parse("5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f");

    private const string ExpectedBody = @"{
        ""address"": {
            ""streetName"": ""High Street"",
            ""buildingNumber"": ""12"",
            ""buildingNameOrFlatNumber"": ""Flat 3"",
            ""city"": ""Leeds"",
            ""postcode"": ""LS1 1AA""
        },
        ""pensionType"": ""Defined benefit"",
        ""provider"": ""Acme Pensions"",
        ""planReference"": ""PLAN-123"",
        ""hasValidNominationForm"": true,
        ""deathBenefitValuePayable"": 125000.50,
        ""beneficiaryDetails"": ""Spouse"",
        ""isValidForInheritanceTax"": false,
        ""realisation"": {
            ""receivedAt"": ""2024-03-01T00:00:00"",
            ""value"": 120000.25,
            ""destination"": ""BankAccount"",
            ""otherDestination"": ""Somewhere else"",
            ""caseItemId"": ""5f0c2a8e-3b1d-4c6a-9e2f-7a8b9c0d1e2f""
        },
        ""type"": ""Pension"",
        ""location"": ""EnglandWales"",
        ""notes"": ""Some notes""
    }";

    private readonly Harness _harness;

    public PostingAndPuttingAPension(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldPostThePension()
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId, Populate(new PostPensionResourceRepresentation()));

        AssertBodySent();
    }

    [Fact]
    public async Task ShouldPutThePension()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId, Populate(new PutPensionResourceRepresentation()));

        AssertBodySent();
    }

    /// <summary>The API excludes an item from IHT only on an explicit false.</summary>
    [Fact]
    public async Task ShouldPostValidForInheritanceTaxWhenUnset()
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId, new PostPensionResourceRepresentation());

        SentIsValidForInheritanceTax().Should().BeTrue();
    }

    [Fact]
    public async Task ShouldPutValidForInheritanceTaxWhenUnset()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId, new PutPensionResourceRepresentation());

        SentIsValidForInheritanceTax().Should().BeTrue();
    }

    private bool SentIsValidForInheritanceTax() =>
        JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!["isValidForInheritanceTax"]!.GetValue<bool>();

    private static T Populate<T>(T pension) where T : PensionResourceRepresentationBase
    {
        pension.Location = Location.EnglandWales;
        pension.Notes = "Some notes";
        pension.Address = new AddressResourceRepresentation
        {
            BuildingNameOrFlatNumber = "Flat 3",
            BuildingNumber = "12",
            StreetName = "High Street",
            City = "Leeds",
            Postcode = "LS1 1AA"
        };
        pension.PensionType = "Defined benefit";
        pension.Provider = "Acme Pensions";
        pension.PlanReference = "PLAN-123";
        pension.HasValidNominationForm = true;
        pension.DeathBenefitValuePayable = 125000.50m;
        pension.BeneficiaryDetails = "Spouse";
        pension.IsValidForInheritanceTax = false;
        pension.Realisation = new EstateItemRealisationResourceRepresentation
        {
            ReceivedAt = new DateTime(2024, 3, 1),
            Value = 120000.25m,
            Destination = RealisationDestination.BankAccount,
            OtherDestination = "Somewhere else",
            CaseItemId = CaseItemId
        };

        return pension;
    }

    private void AssertBodySent() =>
        JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.ToJsonString()
            .Should().Be(JsonNode.Parse(ExpectedBody)!.ToJsonString());
}
