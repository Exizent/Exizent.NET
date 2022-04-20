using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class CashSavingsAccountEstateItemJsonBuilder : EstateItemJsonBuilder<CashSavingsAccountResourceRepresentation>
{
    public CashSavingsAccountEstateItemJsonBuilder(CashSavingsAccountResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }
 
    protected override JsonObject InnerBuild(JsonObject jsonObject,
        CashSavingsAccountResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.CashSavingsAccount));
        jsonObject.Add("institution", resourceRepresentation.Institution);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("sortCode", resourceRepresentation.SortCode);
        jsonObject.Add("isFixedTerm", resourceRepresentation.IsFixedTerm);
        jsonObject.Add("isNationalSavingsAndInvestments", resourceRepresentation.IsNationalSavingsAndInvestments);
        jsonObject.Add("nationalSavingsAndInvestmentsProduct", resourceRepresentation.NationalSavingsAndInvestmentsProduct?.ToString());
        jsonObject.Add("estimatedBalance", resourceRepresentation.EstimatedBalance);
        jsonObject.Add("confirmedBalance", resourceRepresentation.ConfirmedBalance);
        jsonObject.Add("interestUpToDateOfDeath", resourceRepresentation.InterestUpToDateOfDeath);
        jsonObject.Add("institutionAddress", AddressJsonBuilder.Build(resourceRepresentation.InstitutionAddress));
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);
 
        return jsonObject;
    }
}