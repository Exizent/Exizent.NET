using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Organisations;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class IndividualSignatoryDetailsJsonBuilder
{
    public static JsonObject Build(IndividualSignatoryDetailsResourceRepresentation? signatoryDetails)
    {
        var jsonObject = new JsonObject
        {
            { "personId", signatoryDetails?.PersonId },
            { "companyRole", signatoryDetails?.CompanyRole }
        };

        return jsonObject;
    }
}