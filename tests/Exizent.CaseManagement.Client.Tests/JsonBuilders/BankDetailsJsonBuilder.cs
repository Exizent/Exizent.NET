using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class BankDetailsJsonBuilder
{
    public static JsonObject Build(BankAccountDetailsResourceRepresentation bankAccountDetails)
    {
        var jsonObject = new JsonObject
        {
            { "name", bankAccountDetails.Name },
            { "nameOnAccount", bankAccountDetails.NameOnAccount },
            { "accountNumber", bankAccountDetails.AccountNumber },
            { "sortCode", bankAccountDetails.SortCode }
        };

        return jsonObject;
    }
}