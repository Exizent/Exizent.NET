using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class CashAccountJsonBuilder
{
    public static JsonObject Build(CashAccountResourceRepresentation account) => new()
    {
        { "id", account.Id },
        { "name", account.Name },
        { "isDefault", account.IsDefault },
        { "linkedTo", new JsonArray() },
        { "balance", 123.45m }
    };
}
