using System.Net;
using System.Text;
using Exizent.CaseManagement.Client.Models;

namespace Exizent.CaseManagement.Client.Tests;

public class TestHttpClientHandler : HttpMessageHandler
{
    private readonly Dictionary<(string verb, string url), (HttpStatusCode status, string? body)> _response = new();
    private readonly List<(string verb, string url)> _requests = new();

    /// <summary>
    /// Every request the client made, in order, so a test can assert on the URLs a multi-request call built
    /// rather than only on what it returned.
    /// </summary>
    public IReadOnlyList<(string Verb, string Url)> Requests => _requests;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var key = (request.Method.Method, request.RequestUri?.PathAndQuery ?? string.Empty);
        _requests.Add(key);

        if (_response.TryGetValue(key, out var response))
        {
            return Task.FromResult(new HttpResponseMessage(response.status)
            {
                Content = response.body is null
                    ? new StringContent(string.Empty)
                    : new StringContent(response.body, Encoding.UTF8, "application/json")
            });
        }

        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
    }

    /// <summary>
    /// Registers a response for one verb and url, with the status code the API would answer with. The
    /// helpers below cover the happy path; this is for the ones where the status is the thing under test.
    /// </summary>
    public void AddResponse(string verb, string url, HttpStatusCode status, string? body = null)
    {
        _response[(verb, url)] = (status, body);
    }

    public void AddGetCaseResponse(Guid caseId, string response)
    {
        _response[("GET", $"/cases/{caseId}")] = (HttpStatusCode.OK, response);
    }
    
    public void AddGetEstateItemResponse(Guid caseId, Guid estateItemId, string response)
    {
        _response[("GET", $"/cases/{caseId}/estateitems/{estateItemId}")] = (HttpStatusCode.OK, response);
    }
    public void AddGetDocumentUrlResponse(Guid caseId, string documentKey, string response)
    {
        _response[("GET", $"/cases/{caseId}/documents/{documentKey}/url")] = (HttpStatusCode.OK, response);
    }
    public void AddGetDocumentUploadUrlResponse(Guid caseId, Guid estateItemId, string fileName, string response)
    {
        _response[("GET", $"/cases/{caseId}/documents/uploadurl?documentType=Asset&estateItemId={estateItemId}&fileName={fileName}")] = (HttpStatusCode.OK, response);
    }    
    public void AddGetDocumentUploadUrlResponse(Guid caseId, DocumentType documentType, string fileName, string response)
    {
        _response[("GET", $"/cases/{caseId}/documents/uploadurl?documentType={documentType}&fileName={fileName}")] = (HttpStatusCode.OK, response);
    }
    
    /// <summary>The estate item documents route for one page. Page size mirrors the client's own.</summary>
    public static string EstateItemDocumentsUrl(Guid caseId, Guid estateItemId, int pageId, int pageSize = 100) =>
        $"/cases/{caseId}/estateitems/{estateItemId}/documents?pageId={pageId}&pageSize={pageSize}";

    public void AddGetEstateItemDocumentsResponse(Guid caseId, Guid estateItemId, int pageId, string response)
    {
        _response[("GET", EstateItemDocumentsUrl(caseId, estateItemId, pageId))] = (HttpStatusCode.OK, response);
    }

    public static string EstateItemDocumentUrlRoute(Guid caseId, Guid estateItemId, Guid documentId) =>
        $"/cases/{caseId}/estateitems/{estateItemId}/documents/{documentId}/url";

    public void AddGetEstateItemDocumentUrlResponse(Guid caseId, Guid estateItemId, Guid documentId,
        HttpStatusCode status, string? response = null)
    {
        _response[("GET", EstateItemDocumentUrlRoute(caseId, estateItemId, documentId))] = (status, response);
    }

    public static string EstateItemDocumentRoute(Guid caseId, Guid estateItemId, Guid documentId) =>
        $"/cases/{caseId}/estateitems/{estateItemId}/documents/{documentId}";

    public void AddDeleteEstateItemDocumentResponse(Guid caseId, Guid estateItemId, Guid documentId,
        HttpStatusCode status)
    {
        _response[("DELETE", EstateItemDocumentRoute(caseId, estateItemId, documentId))] = (status, null);
    }

    public void AddGetCaseWithCompanyResponse(Guid caseId, string response)
    {
        _response[("GET", $"/cases/{caseId}?expand=company")] = (HttpStatusCode.OK, response);
    }
    
    public void AddGetCaseWithEstateItemsFilterResponse(Guid caseId, EstateItemsFilter estateItemsFilter, string response)
    {
        _response[("GET", $"/cases/{caseId}?estateItemsFilter={estateItemsFilter}")] = (HttpStatusCode.OK, response);
    }
    
    public void AddGetCaseWithCompanyAndEstateItemsFilterResponse(Guid caseId, EstateItemsFilter estateItemsFilter, string response)
    {
        _response[("GET", $"/cases/{caseId}?expand=company&estateItemsFilter={estateItemsFilter}")] = (HttpStatusCode.OK, response);
    }
}