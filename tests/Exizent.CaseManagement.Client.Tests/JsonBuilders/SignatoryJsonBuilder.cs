using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Organisations;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class SignatoryJsonBuilder
{
    public static JsonObject Build(ProfessionalExecutorSignatoryResourceRepresentation? signatoryDetails)
    {
        var jsonObject = new JsonObject
        {
            { "personId", signatoryDetails?.PersonId },
            { "companyRole", signatoryDetails?.CompanyRole }
        };

        return jsonObject;
    }
}