using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public class PremiumBondEstateItemJsonBuilder : EstateItemJsonBuilder<PremiumBondResourceRepresentation>
{
    public PremiumBondEstateItemJsonBuilder(PremiumBondResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
       
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        PremiumBondResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.PremiumBond));
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("bondHolderNumber", resourceRepresentation.BondHolderNumber);
        jsonObject.Add("bondNumber", resourceRepresentation.BondNumber);
        jsonObject.Add("valueAtDateOfDeath", resourceRepresentation.ValueAtDateOfDeath);
        jsonObject.Add("valueOfUnclaimedPrizes", resourceRepresentation.ValueOfUnclaimedPrizes);

        return jsonObject;
    }
}