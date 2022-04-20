using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class BankAccountEstateItemJsonBuilder : EstateItemJsonBuilder<BankAccountResourceRepresentation>
{
    public BankAccountEstateItemJsonBuilder(BankAccountResourceRepresentation resourceRepresentation)
        : base(resourceRepresentation)
    {
    }

    protected override JsonObject InnerBuild(JsonObject jsonObject,
        BankAccountResourceRepresentation resourceRepresentation)
    {
        jsonObject.Add("type", nameof(EstateItemType.BankAccount));
        jsonObject.Add("estimatedBalance", resourceRepresentation.EstimatedBalance);
        jsonObject.Add("confirmedBalance", resourceRepresentation.ConfirmedBalance);
        jsonObject.Add("interestUpToDateOfDeath", resourceRepresentation.InterestUpToDateOfDeath);
        jsonObject.Add("bankCharges", resourceRepresentation.BankCharges);
        jsonObject.Add("address", AddressJsonBuilder.Build(resourceRepresentation.Address));
        jsonObject.Add("isPassedToSurvivingJointOwner", resourceRepresentation.IsPassedToSurvivingJointOwner);
        jsonObject.Add("sortCode", resourceRepresentation.SortCode);
        jsonObject.Add("accountNumber", resourceRepresentation.AccountNumber);
        jsonObject.Add("buildingSocietyRollNumber", resourceRepresentation.BuildingSocietyRollNumber);
        jsonObject.Add("nameOfBankOrBuildingSociety", resourceRepresentation.NameOfBankOrBuildingSociety);
        jsonObject.Add("typeOfAccount", resourceRepresentation.TypeOfAccount);
        jsonObject.Add("notPassedDetails", resourceRepresentation.NotPassedDetails);
        jsonObject.Add("proportionOwned", resourceRepresentation.ProportionOwned);

        return jsonObject;
    }
}