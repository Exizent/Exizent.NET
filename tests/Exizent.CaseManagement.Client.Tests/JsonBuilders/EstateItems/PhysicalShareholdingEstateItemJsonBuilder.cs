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
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("shareholderName", resourceRepresentation.ShareholderName);
        jsonObject.Add("shareholderReferenceNumber", resourceRepresentation.ShareholderReferenceNumber);
        jsonObject.Add("quantity", resourceRepresentation.Quantity);
        jsonObject.Add("description", resourceRepresentation.Description);
        jsonObject.Add("price", resourceRepresentation.Price);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("dividendDue", resourceRepresentation.DividendDue);
        jsonObject.Add("isQuotedOnLondonStockExchange", resourceRepresentation.IsQuotedOnLondonStockExchange);
        jsonObject.Add("realisation", EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));
 
        return jsonObject;
    }
}