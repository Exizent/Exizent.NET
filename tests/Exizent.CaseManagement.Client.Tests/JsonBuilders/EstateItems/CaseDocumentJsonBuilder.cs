using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public class CaseDocumentJsonBuilder
{
    public static JsonObject Build(CaseDocumentResourceRepresentation resourceRepresentation) => new()
    {
        { "id", resourceRepresentation.Id },
        { "createdAt", resourceRepresentation.CreatedAt },
        { "key", resourceRepresentation.Key },
        { "fileName", resourceRepresentation.FileName },
        { "fileSize", resourceRepresentation.FileSize },
        { "updatedAt", resourceRepresentation.UpdatedAt },
        { "documentType", resourceRepresentation.DocumentType.ToString() },
        { "isInfected", resourceRepresentation.IsInfected },
        { "metadata", Build(resourceRepresentation.Metadata) }
    };

    public static JsonObject Build(Dictionary<string, string> metadata)
    {
        var obj = new JsonObject();
        foreach (var item in metadata)
        {
            obj.Add(item.Key, item.Value);
        }

        return obj;
    }
}