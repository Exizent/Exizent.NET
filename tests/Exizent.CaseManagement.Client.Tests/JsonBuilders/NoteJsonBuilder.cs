using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class NoteJsonBuilder
{
    public static JsonObject Build(NoteResourceRepresentation note)
    {
        var jsonObject = new JsonObject
        {
            { "id", note.Id },
            { "actorId", note.ActorId},
            { "createdAt", note.CreatedAt},
            { "message", note.Message},
            { "isDeleted", note.IsDeleted}
        };
        return jsonObject;
    }
}