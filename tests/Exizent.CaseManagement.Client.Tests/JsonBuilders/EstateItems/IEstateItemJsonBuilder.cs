using System.Text.Json.Nodes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.EstateItems;

public interface IEstateItemJsonBuilder
{
    JsonObject Build();
}