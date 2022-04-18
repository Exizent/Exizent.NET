using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public class StateBenefitOverpaymentEstateItemJsonBuilder : EstateItemJsonBuilder<StateBenefitOverpaymentResourceRepresentation>
{
    public StateBenefitOverpaymentEstateItemJsonBuilder(StateBenefitOverpaymentResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
         
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        StateBenefitOverpaymentResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.StateBenefitOverpayment));
        jsonObject.Add("amount", resourceRepresentation.Amount);
        jsonObject.Add("hasBeenRepaid", resourceRepresentation.HasBeenRepaid);

        return jsonObject;
    }
}