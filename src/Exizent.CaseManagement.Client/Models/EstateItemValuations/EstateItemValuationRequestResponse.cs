using System.Net;

namespace Exizent.CaseManagement.Client.Models.EstateItemValuations;

/// <summary>
/// How a valuation request ended. <see cref="StatusCode"/> tells the outcomes apart:
/// <list type="bullet">
/// <item>Accepted: <see cref="Valuation"/> is the valuation, pending or already finished.</item>
/// <item>BadRequest: the provider is not recognised or not available.</item>
/// <item>Conflict: the provider cannot value the estate item as it stands. <see cref="Errors"/> names what is
/// missing, keyed by field.</item>
/// <item>NotFound: the case or estate item does not exist, or no provider values that type of item.</item>
/// </list>
/// </summary>
public class EstateItemValuationRequestResponse
{
    public HttpStatusCode StatusCode { get; init; }
    public EstateItemValuationResourceRepresentation? Valuation { get; init; }
    public string? Detail { get; init; }

    public IReadOnlyDictionary<string, string[]> Errors { get; init; } = new Dictionary<string, string[]>();

    public bool IsAccepted => StatusCode == HttpStatusCode.Accepted && Valuation is not null;
}
