using System.Net;
using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

public sealed class PostingAndPuttingACashSavingsAccount : IClassFixture<Harness>
{
    private static readonly AddressResourceRepresentation Address = new()
    {
        BuildingNameOrFlatNumber = "Flat 3",
        BuildingNumber = "12",
        StreetName = "High Street",
        City = "Leeds",
        Postcode = "LS1 1AA"
    };

    private readonly Harness _harness;

    public PostingAndPuttingACashSavingsAccount(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldPostTheAddressAsInstitutionAddress()
    {
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId,
            new PostCashSavingsAccountResourceRepresentation { InstitutionAddress = Address });

        AssertInstitutionAddressSent();
    }

    [Fact]
    public async Task ShouldPutTheAddressAsInstitutionAddress()
    {
        var caseId = Guid.NewGuid();
        var estateItemId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("PUT", $"/cases/{caseId}/estateitems/{estateItemId}",
            HttpStatusCode.NoContent);

        await _harness.Client.PutEstateItem(caseId, estateItemId,
            new PutCashSavingsAccountResourceRepresentation { InstitutionAddress = Address });

        AssertInstitutionAddressSent();
    }

    [Fact]
    public void ShouldExposeTheInstitutionAddressThroughIHasAddress()
    {
        var cashSavingsAccount = new PostCashSavingsAccountResourceRepresentation();

        ((IHasAddress)cashSavingsAccount).Address = Address;

        cashSavingsAccount.InstitutionAddress.Should().BeSameAs(Address);
    }

    private void AssertInstitutionAddressSent()
    {
        var body = JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.AsObject();

        using var _ = new AssertionScope();
        body.ContainsKey("address").Should().BeFalse();
        var address = body["institutionAddress"]!;
        address["buildingNameOrFlatNumber"]!.GetValue<string>().Should().Be(Address.BuildingNameOrFlatNumber);
        address["buildingNumber"]!.GetValue<string>().Should().Be(Address.BuildingNumber);
        address["streetName"]!.GetValue<string>().Should().Be(Address.StreetName);
        address["city"]!.GetValue<string>().Should().Be(Address.City);
        address["postcode"]!.GetValue<string>().Should().Be(Address.Postcode);
    }
}
