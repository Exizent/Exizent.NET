using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.Collaborators;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public class CollaboratorJsonBuilder
{
    public static JsonObject Build(CollaboratorResourceRepresentation resourceRepresentation) => new()
    {
        { "id", resourceRepresentation.Id },
        { "fullName", resourceRepresentation.FullName }
    };
}