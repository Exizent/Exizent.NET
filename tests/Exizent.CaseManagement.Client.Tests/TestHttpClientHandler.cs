using System.Text;
using Exizent.CaseManagement.Client.Models;

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
    public void AddGetDocumentUrlResponse(Guid caseId, string documentKey, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}/documents/{documentKey}/url"), response);
    }
    public void AddGetDocumentUploadUrlResponse(Guid caseId, Guid estateItemId, string fileName, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}/documents/uploadurl?documentType=Asset&estateItemId={estateItemId}&fileName={fileName}"), response);
    }    
    public void AddGetDocumentUploadUrlResponse(Guid caseId, DocumentType documentType, string fileName, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}/documents/uploadurl?documentType={documentType}&fileName={fileName}"), response);
    }
    
    public void AddGetCaseWithCompanyResponse(Guid caseId, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}?expand=company"), response);
    }
    
    public void AddGetCaseWithEstateItemsFilterResponse(Guid caseId, EstateItemsFilter estateItemsFilter, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}?estateItemsFilter={estateItemsFilter}"), response);
    }
    
    public void AddGetCaseWithCompanyAndEstateItemsFilterResponse(Guid caseId, EstateItemsFilter estateItemsFilter, string response)
    {
        _response.Add(("GET", $"/cases/{caseId}?expand=company&estateItemsFilter={estateItemsFilter}"), response);
    }
}