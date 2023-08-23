using System.Text.Json.Nodes;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders.Incomes;

public interface IIncomeBaseJsonBuilder
{
    JsonObject Build();
}