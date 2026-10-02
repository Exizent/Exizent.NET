using System.Collections;
using System.Net;
using System.Reflection;
using System.Text.Json;
using JsonNode = System.Text.Json.Nodes.JsonNode;
using JsonObject = System.Text.Json.Nodes.JsonObject;
using Exizent.CaseManagement.Client.Models.EstateItems;
using FluentAssertions;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.PostingAndPuttingEstateItems;

/// <summary>
/// The API ignores JSON properties it does not bind, so a request property named differently from the
/// API's is dropped without an error. Every property the client sends, including those of nested objects
/// such as an address or a realisation, must therefore also be one the GET representation of that item
/// reads back.
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
            (EstateItemResourceRepresentationBase)CreateWithNestedObjects(requestType));

        var sent = JsonNode.Parse(_harness.ClientHandler.LastRequestBody!)!.AsObject();
        sent.Remove("type");

        PropertiesNotReadBack(sent, resourceRepresentationType, string.Empty).Should().BeEmpty(
            $"the API reads {requestType.Name} under the names {resourceRepresentationType.Name} returns");
    }

    /// <summary>
    /// A nested object left null serialises as <c>null</c>, which says nothing about its keys, so every
    /// nested object is instantiated before sending.
    /// </summary>
    private static object CreateWithNestedObjects(Type type)
    {
        var instance = Activator.CreateInstance(type)!;
        foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                     .Where(p => p.CanWrite && IsNestedObject(p.PropertyType) && p.GetValue(instance) is null))
        {
            property.SetValue(instance, CreateWithNestedObjects(property.PropertyType));
        }

        return instance;
    }

    private static bool IsNestedObject(Type type) =>
        type is { IsClass: true, IsAbstract: false }
        && type != typeof(string)
        && !typeof(IEnumerable).IsAssignableFrom(type)
        && type.GetConstructor(Type.EmptyTypes) is not null;

    private static IEnumerable<string> PropertiesNotReadBack(JsonObject sent, Type readBackType, string path)
    {
        var readBack = readBackType
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .ToDictionary(p => JsonNamingPolicy.CamelCase.ConvertName(p.Name), p => p.PropertyType);

        foreach (var (key, value) in sent)
        {
            if (!readBack.TryGetValue(key, out var readBackPropertyType))
            {
                yield return path + key;
            }
            else if (value is JsonObject nested)
            {
                foreach (var missing in PropertiesNotReadBack(nested, readBackPropertyType, $"{path}{key}."))
                {
                    yield return missing;
                }
            }
        }
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
