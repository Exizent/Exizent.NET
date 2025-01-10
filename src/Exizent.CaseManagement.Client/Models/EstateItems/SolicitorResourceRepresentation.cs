using System.Net;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class SolicitorResourceRepresentation
{
    public string? FullNameOrBusinessName { get; set; }
    public string? ContactName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmailAddress { get; set; }
    public string? Capacity { get; set; }
    public string? ReferenceNumber { get; set; }
    public AddressResourceRepresentation? Address { get; init; }
}