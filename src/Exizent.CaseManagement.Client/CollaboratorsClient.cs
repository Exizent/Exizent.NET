using System.Net.Http.Json;
using System.Text;

namespace Exizent.CaseManagement.Client;

internal class CollaboratorsClient
{
    private readonly HttpClient _client;

    public CollaboratorsClient(HttpClient client) => _client = client;

    public async Task UpdateCaseOwner(Guid caseId, int ownerId, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/owner");
        request.Content = JsonContent.Create(ownerId);

        using var response = await _client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }
    
    public async Task UpdateCaseCollaborators(Guid caseId, List<int> collaboratorIds, CancellationToken cancellationToken = default)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, $"/cases/{caseId}/collaborators");
        request.Content = JsonContent.Create(collaboratorIds);

        using var response = await _client.SendAsync(request, cancellationToken);

        response.EnsureSuccessStatusCode();
    }
}