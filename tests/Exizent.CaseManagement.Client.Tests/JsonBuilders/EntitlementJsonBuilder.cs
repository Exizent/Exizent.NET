using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders
{
    public class EntitlementJsonBuilder
    {
        public static JsonObject Build(EntitlementResourceRepresentation entitlement)
        {
            var jsonObject = new JsonObject
        {
            { "pecuniaryAmount", entitlement.PecuniaryAmount },
            { "residuaryType", entitlement.ResiduaryType.ToString()},
            { "residuaryAmount", entitlement.ResiduaryAmount},
        };

            return jsonObject;
        }
    }

}
