namespace Exizent.CaseManagement.Client.Models.Distributions;

public class DistributionResourceRepresentation
{
    public Guid Id { get; init; }
    public IReadOnlyList<Guid>? CaseItemIds { get; init; }
    public Boolean IsDeleted { get; init; }
    public DistributionType Type { get; init; }
    public decimal? InterimPaymentValue { get; init; }
    public IReadOnlyList<DistributionBeneficiaryResourceRepresentation>? DistributionBeneficiaries { get; init; }
}