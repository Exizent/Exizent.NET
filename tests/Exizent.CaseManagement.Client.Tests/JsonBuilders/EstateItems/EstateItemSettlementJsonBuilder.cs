using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public static class EstateItemSettlementJsonBuilder
{
    public static JsonObject Build(EstateItemSettlementResourceRepresentation resourceRepresentation) =>
        new()
        {
            {"receivedAt", resourceRepresentation.ReceivedAt},
            {"value", resourceRepresentation.Value},
            {"caseItemId", resourceRepresentation.CaseItemId},
            {"thirdPartyCreditorId", resourceRepresentation.ThirdPartyCreditorId}
        };
}