﻿using System.ComponentModel;
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
    Solicitor = 17,
    MotherInLaw = 18,
    FatherInLaw = 19,
    SonInLaw = 20,
    DaughterInLaw = 21,
    BrotherInLaw = 22,
    SisterInLaw = 23,
    Partner = 24

}