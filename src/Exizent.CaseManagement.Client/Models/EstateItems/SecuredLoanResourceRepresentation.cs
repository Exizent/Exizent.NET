namespace Exizent.CaseManagement.Client.Models;

public class SecuredLoanResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal ProportionOwned { get; init; }
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? NotPassedDetails { get; init; }
    public string? Provider { get; init; }
    public string? AccountNumber { get; init; }
    public bool HasProviderBeenAdvised { get; init; }
    public Guid? LinkedEstateItemId { get; init; }
    public decimal? DebtValue { get; init; }
}