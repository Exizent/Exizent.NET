namespace Exizent.CaseManagement.Client.Models.Distributions;

public class DistributionBeneficiaryResourceRepresentation
{
    public Guid BeneficiaryId { get; init; }
    public string? DescriptionOfTrustOrContingency { get; init; }
    public Guid? LeadTrusteePersonId { get; init; }
}