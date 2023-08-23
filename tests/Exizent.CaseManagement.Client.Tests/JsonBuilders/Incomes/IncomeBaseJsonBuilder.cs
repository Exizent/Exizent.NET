using System.Text.Json.Nodes;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Incomes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public abstract class IncomeBaseJsonBuilder<TIncome> : IIncomeBaseJsonBuilder
    where TIncome : IncomeBaseResourceRepresentation
{
    private readonly TIncome _resourceRepresentation;

    protected IncomeBaseJsonBuilder(TIncome resourceRepresentation)
    {
        _resourceRepresentation = resourceRepresentation;
    }

    public JsonObject Build()
    {
        var jObject = BuildEstateItem(_resourceRepresentation);
        return InnerBuild(jObject, _resourceRepresentation);
    }

    protected abstract JsonObject InnerBuild(JsonObject jsonObject, TIncome resourceRepresentation);

    private static JsonObject BuildEstateItem(IncomeBaseResourceRepresentation resourceRepresentation) =>
        new()
        {
            {"id", resourceRepresentation.Id},
            {"createdAt", resourceRepresentation.CreatedAt},
            {"updatedAt", resourceRepresentation.UpdatedAt},
            {"notes", resourceRepresentation.Notes},
            {"value", resourceRepresentation.Value},
            {"isDeleted", resourceRepresentation.IsDeleted}
        };
}