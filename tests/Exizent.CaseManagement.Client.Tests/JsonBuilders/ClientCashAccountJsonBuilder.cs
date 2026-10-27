using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class ClientCashAccountJsonBuilder
{
    public static JsonObject Build(ClientCashAccountResourceRepresentation account) => new()
    {
        { "id", account.Id },
        { "name", account.Name },
        { "isDefault", account.IsDefault },
        { "linkedTo", new JsonArray() },
        { "balance", 123.45m }
    };
}
