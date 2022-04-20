using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class UKGovAndMunicipalSecuritiesEstateItemJsonBuilder : EstateItemJsonBuilder<UKGovAndMunicipalSecuritiesResourceRepresentation>
{
    public UKGovAndMunicipalSecuritiesEstateItemJsonBuilder(UKGovAndMunicipalSecuritiesResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
           
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        UKGovAndMunicipalSecuritiesResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.UKGovAndMunicipalSecurities));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("nameOfShareholding", resourceRepresentation.NameOfShareholding);
        jsonObject.Add("quantity", resourceRepresentation.Quantity);
        jsonObject.Add("descriptionOfStock", resourceRepresentation.DescriptionOfStock);
        jsonObject.Add("unitPrice", resourceRepresentation.UnitPrice);
        jsonObject.Add("interestDue", resourceRepresentation.InterestDue);
 
        return jsonObject;
    }
}