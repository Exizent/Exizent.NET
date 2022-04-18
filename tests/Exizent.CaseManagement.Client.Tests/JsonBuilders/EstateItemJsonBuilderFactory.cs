﻿using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests.JsonBuilders;

public static class EstateItemJsonBuilderFactory
{
    public static IEstateItemJsonBuilder Create(EstateItemResourceRepresentation resourceRepresentation) =>
        resourceRepresentation switch
        {
            BankAccountResourceRepresentation bankAccount => new BankAccountEstateItemJsonBuilder(bankAccount),
            BuildingResourceRepresentation building => new BuildingEstateItemJsonBuilder(building),
            BusinessInterestResourceRepresentation businessInterest => new BusinessInterestEstateItemJsonBuilder(businessInterest),
            CashIsaResourceRepresentation cashIsa => new CashIsaEstateItemJsonBuilder(cashIsa),
            CashSavingsAccountResourceRepresentation cashSavingsAccount => new CashSavingsAccountEstateItemJsonBuilder(cashSavingsAccount),
            CreditCardOrPersonalLoanResourceRepresentation creditCardOrPersonalLoan => new CreditCardOrPersonalLoanEstateItemJsonBuilder(creditCardOrPersonalLoan),
            CryptocurrencyResourceRepresentation cryptocurrency => new CryptocurrencyEstateItemJsonBuilder(cryptocurrency),
            EndowmentPolicyResourceRepresentation endowmentPolicy => new EndowmentPolicyEstateItemJsonBuilder(endowmentPolicy),
            IncomeBondResourceRepresentation incomeBond => new IncomeBondEstateItemJsonBuilder(incomeBond),
            _ => throw new NotImplementedException()
        };
}