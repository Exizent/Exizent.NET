using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public abstract class EstateItemJsonBuilder<TEstateItem> : IEstateItemJsonBuilder
    where TEstateItem : EstateItemResourceRepresentation
{
    private readonly TEstateItem _resourceRepresentation;

    protected EstateItemJsonBuilder(TEstateItem resourceRepresentation)
    {
        _resourceRepresentation = resourceRepresentation;
    }

    public JsonObject Build()
    {
        var jObject = BuildEstateItem(_resourceRepresentation);
        return InnerBuild(jObject, _resourceRepresentation);
    }

    protected abstract JsonObject InnerBuild(JsonObject jsonObject, TEstateItem resourceRepresentation);

    private static JsonObject BuildEstateItem(EstateItemResourceRepresentation resourceRepresentation) =>
        
        // var documents = new JsonArray(resourceRepresentation.Documents
        // .Select(CaseDocumentJsonBuilder.Build).ToArray<JsonNode?>());

        new()
        {
            {"id", resourceRepresentation.Id},
            {"location", resourceRepresentation.Location.ToString("G")},
            {"createdAt", resourceRepresentation.CreatedAt},
            {"updatedAt", resourceRepresentation.UpdatedAt},
            {"notes", resourceRepresentation.Notes},
            {"isArchived", resourceRepresentation.IsArchived},
            {"isComplete", resourceRepresentation.IsComplete},
            {"dateOfDeathValue", resourceRepresentation.DateOfDeathValue},
            {"documents", new JsonArray(resourceRepresentation.Documents
                .Select(CaseDocumentJsonBuilder.Build).ToArray<JsonNode?>())}
        };
}