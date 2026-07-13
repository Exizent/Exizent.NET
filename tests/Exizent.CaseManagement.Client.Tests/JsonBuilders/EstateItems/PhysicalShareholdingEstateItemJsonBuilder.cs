using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class PhysicalShareholdingEstateItemJsonBuilder : EstateItemJsonBuilder<PhysicalShareholdingResourceRepresentation>
{
    public PhysicalShareholdingEstateItemJsonBuilder(PhysicalShareholdingResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        PhysicalShareholdingResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.PhysicalShareholding));
        jsonObject.Add("address", resourceRepresentation.Address is not null ? AddressJsonBuilder.Build(resourceRepresentation.Address) : null);
        jsonObject.Add("shareholdingName", resourceRepresentation.ShareholdingName);
        jsonObject.Add("lookupId", resourceRepresentation.LookupId);
        jsonObject.Add("shareholderReferenceNumber", resourceRepresentation.ShareholderReferenceNumber);
        jsonObject.Add("quantity", resourceRepresentation.Quantity);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("shareRegistrar", resourceRepresentation.ShareRegistrar);
        jsonObject.Add("valuation", BuildValuation(resourceRepresentation.Valuation));
        jsonObject.Add("price", resourceRepresentation.Price);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("dividendDue", resourceRepresentation.DividendDue);
        jsonObject.Add("valuationBy", resourceRepresentation.ValuationBy);
#pragma warning disable CS0618
        jsonObject.Add("isQuotedOnLondonStockExchange", resourceRepresentation.IsQuotedOnLondonStockExchange);
#pragma warning restore CS0618
        jsonObject.Add("isListedOnRecognisedStockExchange", resourceRepresentation.IsListedOnRecognisedStockExchange);
        jsonObject.Add("isTradedElsewhere", resourceRepresentation.IsTradedElsewhere);
        jsonObject.Add("isListedOnUkExchanges", resourceRepresentation.IsListedOnUkExchanges);
        jsonObject.Add("realisation", resourceRepresentation.Realisation is not null ? EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation) : null);
        jsonObject.Add("jointOwnerIds", new JsonArray(resourceRepresentation.JointOwnerIds.Select(x => (JsonNode)JsonValue.Create(x)!).ToArray()));
        jsonObject.Add("ownedForTwoYears", resourceRepresentation.OwnedForTwoYears);
        jsonObject.Add("businessRelief", resourceRepresentation.BusinessRelief);
        jsonObject.Add("tradingCurrencyCode", resourceRepresentation.TradingCurrencyCode.ToString());
        jsonObject.Add("exchangeRate", resourceRepresentation.ExchangeRate);
        jsonObject.Add("isValidForInheritanceTax", resourceRepresentation.IsValidForInheritanceTax);
        jsonObject.Add("isIncludedInEstateAccounts", resourceRepresentation.IsIncludedInEstateAccounts);
        jsonObject.Add("hadControlOfTheCompany", resourceRepresentation.HadControlOfTheCompany);
        jsonObject.Add("shouldGroupByRegistrar", resourceRepresentation.ShouldGroupByRegistrar);
        jsonObject.Add("realisations", new JsonArray(resourceRepresentation.Realisations
            .Select(r => (JsonNode)ShareRealisationJsonBuilder.Build(r)).ToArray()));

        return jsonObject;
    }

    private static JsonNode? BuildValuation(PhysicalShareholdingValuationResourceRepresentation? valuation) =>
        valuation switch
        {
            SingleSharePriceValuationResourceRepresentation single => new JsonObject
            {
                { "type", nameof(ShareValuationType.SinglePrice) },
                { "price", single.Price }
            },
            QuarterUpSharePriceValuationResourceRepresentation quarterUp => new JsonObject
            {
                { "type", nameof(ShareValuationType.QuarterUp) },
                { "lowPrice", quarterUp.LowPrice },
                { "highPrice", quarterUp.HighPrice }
            },
            _ => null
        };
}