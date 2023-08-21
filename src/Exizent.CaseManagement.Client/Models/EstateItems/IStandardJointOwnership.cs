namespace Exizent.CaseManagement.Client.Models.EstateItems;

public interface IStandardJointOwnership: IHasJointOwners, IPassableToJointOwner
{
    decimal ProportionOwned { get; set; }

}