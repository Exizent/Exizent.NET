using Dahomey.Json.Attributes;

namespace Exizent.CaseManagement.Client.Models.EstateItems;

[JsonDiscriminator(nameof(EstateItemType.BankAccount))]
public class BankAccountResourceRepresentation : EstateItemResourceRepresentation
{
    public decimal? EstimatedBalance { get; init; }
    public decimal? ConfirmedBalance { get; init; }
    public decimal? InterestUpToDateOfDeath { get; init; }
    public decimal? BankCharges { get; init; }
    public AddressResourceRepresentation Address { get; init; } = null!;
    public bool? IsPassedToSurvivingJointOwner { get; init; }
    public string? SortCode { get; init; }
    public string? AccountNumber { get; init; }
    public string? BuildingSocietyRollNumber { get; init; }
    public string? NameOfBankOrBuildingSociety { get; init; }
    public string? TypeOfAccount { get; init; }
    public string? NotPassedDetails { get; init; }
    public decimal ProportionOwned { get; init; }
    public EstateItemRealisationResourceRepresentation? Realisation { get; init; }
}