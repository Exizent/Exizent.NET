using System.ComponentModel;

namespace Exizent.CaseManagement.Client.Models.Distributions;

public enum DistributionType
{
    [Description("Residue")] Residue = 1,
    [Description("Pecuniary legacy")] PecuniaryLegacy,
    [Description("Specific gift")] SpecificGift
}