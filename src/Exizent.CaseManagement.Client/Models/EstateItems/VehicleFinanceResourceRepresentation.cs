namespace Exizent.CaseManagement.Client.Models.EstateItems;

public class VehicleFinanceResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public string? Provider { get; init; }
    public bool HasProviderBeenAdvised { get; init; }
    public Guid? LinkedEstateItemId { get; init; }
    public decimal? DebtValue { get; init; }
}