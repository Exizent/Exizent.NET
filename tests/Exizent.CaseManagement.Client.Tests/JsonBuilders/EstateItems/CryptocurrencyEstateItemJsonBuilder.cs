using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class CryptocurrencyEstateItemJsonBuilder : EstateItemJsonBuilder<CryptocurrencyResourceRepresentation>
{
    public CryptocurrencyEstateItemJsonBuilder(CryptocurrencyResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        CryptocurrencyResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.Cryptocurrency));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("typeOfWallet", resourceRepresentation.TypeOfWallet);
        jsonObject.Add("locationOfWallet", resourceRepresentation.LocationOfWallet);
        jsonObject.Add("walletLoginInformation", resourceRepresentation.WalletLoginInformation);
        jsonObject.Add("walletAccessGuide", resourceRepresentation.WalletAccessGuide);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("investmentValue", resourceRepresentation.InvestmentValue);
        jsonObject.Add("grossSaleProceeds", resourceRepresentation.GrossSaleProceeds);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));

        return jsonObject;
    }
}