namespace Exizent.CaseManagement.Client.Models.EstateItems;

public interface IHasJointOwners
{
    /// <remarks>This property is always null when <see cref="IPassableToJointOwner.IsPassedToSurvivingJointOwner"/> is false.</remarks>
    IReadOnlyList<Guid>? JointOwnerIds { get; set; }
}