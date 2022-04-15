namespace Exizent.CaseManagement.Client.Models;

public enum  MaritalStatus
{
#pragma warning disable CA1720
    Single = 1,
#pragma warning restore CA1720
    Married = 2,
    CivilPartnership = 3,
    Divorced = 4,
    Widowed = 5,
    CivilPartnershipDissolved = 6,
    JudiciallySeparated = 7
}