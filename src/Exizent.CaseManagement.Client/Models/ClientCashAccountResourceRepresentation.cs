namespace Exizent.CaseManagement.Client.Models;

public class ClientCashAccountResourceRepresentation
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public bool IsDefault { get; init; }
}
