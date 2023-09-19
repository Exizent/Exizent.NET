using System.Text;

namespace Exizent.CaseManagement.Client.Tests;

public class TestHttpClientHandler : HttpMessageHandler
{
    private readonly Dictionary<(string verb, string url), string> _response = new();

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        if (_response.TryGetValue((request.Method.Method, request.RequestUri?.PathAndQuery ?? string.Empty), out var response))
        {
            return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(response, Encoding.UTF8, "application/json")
            });
        }

        return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.NotFound));
    }

    public void AddGetCaseResponse(Guid caseId, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}"), response);
    }
    
    public void AddGetEstateItemResponse(Guid caseId, Guid estateItemId, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}/estateitems/{estateItemId}"), response);
    }
    
    public void AddGetCaseWithCompanyResponse(Guid caseId, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}?expand=company"), response);
    }
}