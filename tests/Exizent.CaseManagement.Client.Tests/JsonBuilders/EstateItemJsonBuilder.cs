using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

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

    private static JsonObject BuildEstateItem(EstateItemResourceRepresentation resourceRepresentation)
    {
        var jsonObject = new JsonObject();

        jsonObject.Add("id", resourceRepresentation.Id);
        jsonObject.Add("location", resourceRepresentation.Location.ToString("G"));
        jsonObject.Add("createdAt", resourceRepresentation.CreatedAt);
        jsonObject.Add("updatedAt", resourceRepresentation.UpdatedAt);
        jsonObject.Add("notes", resourceRepresentation.Notes);
        jsonObject.Add("isArchived", resourceRepresentation.IsArchived);
        jsonObject.Add("isComplete", resourceRepresentation.IsComplete);
        jsonObject.Add("realisation",
            resourceRepresentation.Realisation is null
                ? null
                : EstateItemRealisationJsonBuilder.Build(resourceRepresentation.Realisation));

        return jsonObject;
    }
}