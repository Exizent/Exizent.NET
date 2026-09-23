using System.Net;
using System.Reflection;
using System.Text.Json;
using JsonNode = System.Text.Json.Nodes.JsonNode;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

/// <summary>
/// The API ignores JSON properties it does not bind, so a request property named differently from the
/// API's is dropped without an error. Every property the client sends must therefore also be one the GET
/// representation of that item reads back.
/// </summary>
public sealed class EstateItemRequestBodiesMatchTheirResourceRepresentation : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public EstateItemRequestBodiesMatchTheirResourceRepresentation(Harness harness) => _harness = harness;

    public static IEnumerable<object[]> PostAndPutTypes() =>
        typeof(EstateItemResourceRepresentationBase).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false }
                        && typeof(EstateItemResourceRepresentationBase).IsAssignableFrom(t)
                        && (t.Name.StartsWith("Post", StringComparison.Ordinal) || t.Name.StartsWith("Put", StringComparison.Ordinal)))
            .OrderBy(t => t.Name)
            .Select(t => new object[] { t });

    [Theory]
    [MemberData(nameof(PostAndPutTypes))]
    public async Task ShouldOnlySendPropertiesTheResourceRepresentationHas(Type requestType)
    {
        var resourceRepresentationType = ResourceRepresentationFor(requestType);
        var caseId = Guid.NewGuid();
        _harness.ClientHandler.AddResponse("POST", $"/cases/{caseId}/estateitems", HttpStatusCode.Created,
            $@"{{ ""id"": ""{Guid.NewGuid()}"" }}");

        await _harness.Client.PostEstateItem(caseId,
            (EstateItemResourceRepresentationBase)Activator.CreateInstance(requestType)!);

        var sent = JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.AsObject()
            .Select(p => p.Key)
            .Where(k => k != "type");
        var readBack = resourceRepresentationType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(p => JsonNamingPolicy.CamelCase.ConvertName(p.Name));

        sent.Except(readBack).Should().BeEmpty(
            $"the API reads {requestType.Name} under the names {resourceRepresentationType.Name} returns");
    }

    private static Type ResourceRepresentationFor(Type requestType)
    {
        var name = requestType.Name.StartsWith("Post", StringComparison.Ordinal)
            ? requestType.Name["Post".Length..]
            : requestType.Name["Put".Length..];

        return requestType.Assembly.GetType($"{requestType.Namespace}.{name}")
               ?? throw new InvalidOperationException($"{requestType.Name} has no {name} to compare against.");
    }
}
