namespace Exizent.CaseManagement.Client.Models.Deceased;

public class PostDeceasedResourceRepresentation
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public DateTime? DateOfBirth { get; init; }
    public string? NiNumber { get; init; }
    public string? Occupation { get; init; }
    public DateTime DateOfDeath { get; init;}
}