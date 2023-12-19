using System.Net;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class AssetResourceRepresentation
{
    public string Description { get; init; } = null!;
    public decimal Value { get; init; }
}