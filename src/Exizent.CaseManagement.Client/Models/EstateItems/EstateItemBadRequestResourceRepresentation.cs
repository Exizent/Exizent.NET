using System.Net;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class EstateItemBadRequestResourceRepresentation: EstateItemResponseResourceRepresentation
{
    public string? Body { get; set; }
}