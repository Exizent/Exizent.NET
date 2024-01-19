using System.Net;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class TrusteeOrSolicitorResourceRepresentation
{
    public string Name { get; init; } = null!;
    public AddressResourceRepresentation? Address { get; init; }
}