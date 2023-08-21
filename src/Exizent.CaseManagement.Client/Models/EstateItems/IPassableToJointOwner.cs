namespace Exizent.CaseManagement.Client.Models.EstateItems;

public interface IPassableToJointOwner
{
    bool? IsPassedToSurvivingJointOwner { get; set; }
    string? NotPassedDetails { get; set; }
}