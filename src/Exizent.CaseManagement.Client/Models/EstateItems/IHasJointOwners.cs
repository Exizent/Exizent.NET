namespace Exizent.CaseManagement.Client.Models.EstateItems;

public interface IHasJointOwners
{
    IReadOnlyList<Guid> JointOwnerIds { get; set; }
}