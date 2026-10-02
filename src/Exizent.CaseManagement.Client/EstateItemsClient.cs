using System.ComponentModel;
using System.Net;
using System.Text;
using System.Text.Json;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.EstateItemValuations;

namespace Exizent.CaseManagement.Client;

internal class EstateItemsClient
{
    private readonly HttpClient _client;

    public EstateItemsClient(HttpClient httpClient)
    {
        _client = httpClient;
    }
    
    public async Task<EstateItemResourceRepresentation?> GetEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"/cases/{caseId}/estateitems/{estateItemId}");

        using var response = await _client.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<EstateItemResourceRepresentation>(body,
            DefaultJsonSerializerOptions.Instance);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PostEstateItem(Guid caseId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(estateItem, estateItem.GetType(), DefaultJsonSerializerOptions.Instance);

        using var request = new HttpRequestMessage(HttpMethod.Post, $"/cases/{caseId}/estateitems");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadAsStringAsync(cancellationToken);
            return new EstateItemBadRequestResourceRepresentation { StatusCode = HttpStatusCode.BadRequest, Body = errors};
        }
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        var estateItemResponse = JsonSerializer.Deserialize<EstateItemResponseResourceRepresentation>(body,
            DefaultJsonSerializerOptions.Instance);
        estateItemResponse!.StatusCode = response.StatusCode;
        return estateItemResponse;

    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemResponseResourceRepresentation?> PutEstateItem(Guid caseId, Guid estateItemId,
        EstateItemResourceRepresentationBase estateItem, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(estateItem, estateItem.GetType(), DefaultJsonSerializerOptions.Instance);
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/estateitems/{estateItemId}");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var errors = await response.Content.ReadAsStringAsync(cancellationToken);
            return new EstateItemBadRequestResourceRepresentation { StatusCode = HttpStatusCode.BadRequest, Body = errors};
        }

        if (response.StatusCode == HttpStatusCode.Created)
        {
            var body = await response.Content.ReadAsStringAsync(cancellationToken);
            var estateItemCreatedResponse = JsonSerializer.Deserialize<EstateItemResponseResourceRepresentation>(body,
                DefaultJsonSerializerOptions.Instance);
            estateItemCreatedResponse!.StatusCode = HttpStatusCode.Created;
            return estateItemCreatedResponse;
        }

        response.EnsureSuccessStatusCode();

        return new EstateItemResponseResourceRepresentation { Id = estateItemId, StatusCode = response.StatusCode};
    }


    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ChangeEstateItemStatus(Guid caseId, Guid estateItemId, EstateItemStatusChange statusChange,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(statusChange, DefaultJsonSerializerOptions.Instance);
        using var request =
            new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/estateitems/{estateItemId}/status");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ArchiveEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.Archive, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task RestoreEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.Restore, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task CompleteEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.Complete, cancellationToken);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task ReopenEstateItem(Guid caseId, Guid estateItemId,
        CancellationToken cancellationToken = default)
    {
        await ChangeEstateItemStatus(caseId, estateItemId, EstateItemStatusChangeAction.ReOpen, cancellationToken);
    }

    private async Task ChangeEstateItemStatus(Guid caseId, Guid estateItemId, EstateItemStatusChangeAction status,
        CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/estateitems/{estateItemId}/status");
        request.Content = new StringContent(status.ToString(), Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }
    
    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task UpdateEstateItemNotes(Guid caseId, Guid estateItemId, string notes,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(notes, DefaultJsonSerializerOptions.Instance);
        using var request =
            new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/estateitems/{estateItemId}/notes");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemValuationRequestResponse> RequestEstateItemValuation(Guid caseId, Guid estateItemId,
        EstateItemValuationProvider provider, CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(new PostEstateItemValuationResourceRepresentation { Provider = provider },
            DefaultJsonSerializerOptions.Instance);
        using var request =
            new HttpRequestMessage(HttpMethod.Post, $"/cases/{caseId}/estateitems/{estateItemId}/valuations");
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        using var response = await _client.SendAsync(request, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);

        switch (response.StatusCode)
        {
            case HttpStatusCode.Accepted:
                return new EstateItemValuationRequestResponse
                {
                    StatusCode = HttpStatusCode.Accepted,
                    Valuation = JsonSerializer.Deserialize<EstateItemValuationResourceRepresentation>(body,
                        DefaultJsonSerializerOptions.Instance)
                };
            case HttpStatusCode.BadRequest or HttpStatusCode.Conflict:
                var (detail, errors) = ReadProblem(body);
                return new EstateItemValuationRequestResponse
                    { StatusCode = response.StatusCode, Detail = detail, Errors = errors };
            case HttpStatusCode.NotFound:
                return new EstateItemValuationRequestResponse { StatusCode = HttpStatusCode.NotFound };
        }

        response.EnsureSuccessStatusCode();
        throw new HttpRequestException(
            $"Requesting a valuation returned an unexpected {(int)response.StatusCode} {response.StatusCode}.");
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public async Task<EstateItemValuationResourceRepresentation?> GetEstateItemValuation(Guid caseId,
        Guid estateItemId, Guid valuationId, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get,
            $"/cases/{caseId}/estateitems/{estateItemId}/valuations/{valuationId}");
        using var response = await _client.SendAsync(request, cancellationToken);
        if (response.StatusCode == HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        return JsonSerializer.Deserialize<EstateItemValuationResourceRepresentation>(body,
            DefaultJsonSerializerOptions.Instance);
    }

    private static (string? Detail, IReadOnlyDictionary<string, string[]> Errors) ReadProblem(string body)
    {
        var errors = new Dictionary<string, string[]>();
        if (string.IsNullOrWhiteSpace(body))
            return (null, errors);

        try
        {
            using var document = JsonDocument.Parse(body);
            var root = document.RootElement;
            var detail = root.TryGetProperty("detail", out var d) && d.ValueKind == JsonValueKind.String
                ? d.GetString()
                : null;

            if (root.TryGetProperty("errors", out var e) && e.ValueKind == JsonValueKind.Object)
            {
                foreach (var field in e.EnumerateObject())
                {
                    errors[field.Name] = field.Value.ValueKind == JsonValueKind.Array
                        ? field.Value.EnumerateArray().Select(m => m.GetString() ?? string.Empty).ToArray()
                        : new[] { field.Value.ToString() };
                }
            }

            return (detail, errors);
        }
        catch (JsonException)
        {
            return (null, errors);
        }
    }
}