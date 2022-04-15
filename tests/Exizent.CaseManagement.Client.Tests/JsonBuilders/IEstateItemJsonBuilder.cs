using System.Text.Json.Nodes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public interface IEstateItemJsonBuilder
{
    JsonObject Build();
}