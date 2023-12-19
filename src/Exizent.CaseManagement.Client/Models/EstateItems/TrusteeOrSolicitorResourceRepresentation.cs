using System.Net;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class TrusteeOrSolicitorResourceRepresentation
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public AddressResourceRepresentation? Address { get; init; }
}