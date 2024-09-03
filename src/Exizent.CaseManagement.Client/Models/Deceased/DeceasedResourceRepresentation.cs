namespace Exizent.CaseManagement.Client.Models.Deceased;

public class DeceasedResourceRepresentation
{
    public string? Title { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public IEnumerable<string> OtherNames { get; init; } = Array.Empty<string>();
    public string? MaidenName { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? NiNumber { get; init; }
    public string? Occupation { get; init; }
    public AddressResourceRepresentation? Address { get; init; }
    public WillDetailsResourceRepresentation? WillDetails { get; init; }
    public DateTime DateOfDeath { get; init;}
    public string? PlaceOfDeath { get;init; }
    public string? Domicile { get; init;}
    public string? OtherDomicile { get; init;}
    public Sheriffdom? Sheriffdom { get; init; }
    public string? OtherSheriffdom { get; init; }
    public bool? HasDeathCertificate { get;init; }
    public string? IhtReferenceNumber { get; init;}
    public string? UniqueTaxpayerReference { get; init;}
    public bool? HasSurvivingSpouseOrCivilPartner { get; init;}
    public MaritalStatus? MaritalStatus { get; init;}
    public DateTime? MaritalStatusDate { get; init;}
    public string? Notes { get;init; }

}