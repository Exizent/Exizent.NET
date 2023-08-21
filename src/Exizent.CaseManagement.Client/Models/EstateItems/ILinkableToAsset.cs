namespace Exizent.CaseManagement.Client.Models.EstateItems;

public interface ILinkableToAsset
{
    Guid? LinkedEstateItemId { get; set; }
}