namespace Exizent.CaseManagement.Client.Models;

public class GetCaseOptions
{
    public bool ExpandCompany { get; set; }
    public EstateItemsFilter? EstateItemsFilter { get; set; }
}