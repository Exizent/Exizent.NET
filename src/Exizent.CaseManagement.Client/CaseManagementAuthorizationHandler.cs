using System.Net.Http.Headers;

namespace Exizent.CaseManagement.Client;

public class CaseManagementAuthorizationHandler : DelegatingHandler
{
    private readonly IAuthorizationTokenStore _tokenStore;

    public CaseManagementAuthorizationHandler(IAuthorizationTokenStore tokenStore)
    {
        _tokenStore = tokenStore;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenStore.GetToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}