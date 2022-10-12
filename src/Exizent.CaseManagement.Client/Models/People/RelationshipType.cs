using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Exizent.CaseManagement.Client.Models.People;

public enum RelationshipType
{

    Husband = 1,
    Wife = 2,
    BiologicalChild = 3,
    AdoptedChild = 4,
    StepChild = 5,
    Niece = 6,
    Nephew = 7,
    Sister = 8,
    Brother = 9,
    Mother = 10,
    Father = 11,
    Grandchild = 12,
    GreatGrandchild = 13,
    Cousin = 14,
    Friend = 15,
    Other = 16,
    Organisation = 17,
    Solicitor = 18,
    MotherInLaw = 19,
    FatherInLaw = 20,
    SonInLaw = 21,
    DaughterInLaw = 22,
    BrotherInLaw = 23,
    SisterInLaw = 24,
    Partner = 25,


}