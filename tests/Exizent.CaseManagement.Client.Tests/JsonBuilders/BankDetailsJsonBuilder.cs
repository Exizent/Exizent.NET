using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class BankDetailsJsonBuilder
{
    public static JsonObject Build(BankAccountDetailsResourceRepresentation bankAccountDetails)
    {
        var jsonObject = new JsonObject();
 
        jsonObject.Add("name", bankAccountDetails.Name);
        jsonObject.Add("nameOnAccount", bankAccountDetails.NameOnAccount);
        jsonObject.Add("accountNumber", bankAccountDetails.AccountNumber);
        jsonObject.Add("sortCode", bankAccountDetails.SortCode);
 
        return jsonObject;
    }
}