using System.Net;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class EstateItemResponseResourceRepresentation
{
    public Guid Id { get; init; }

    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
}