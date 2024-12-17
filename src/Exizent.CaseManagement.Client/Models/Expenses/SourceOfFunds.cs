namespace Exizent.CaseManagement.Client.Models.Expenses;

public enum SourceOfFunds
{
    DeceasedBankAccount = 1,
    CashSavingsAccount = 2,
    CashIsa = 3,
    ClientCashAccount = 4,
    ProceedsOfSale = 5,
    PrepaidFuneralPlan = 6,
    PremiumBond = 7,
    NationalSavingsAndInvestmentsProduct = 8,
    NationalSavingsAndInvestmentsIncomeBond = 9,
#pragma warning disable CA1069 // Enums values should not be duplicated
    IncomeBond = 9,
    Pension = 10,
    LifePolicy = 11,
    UnitTrust = 12,
    InvestmentBond = 13,
    NomineeInvestmentAccount = 14,
    PhysicalShareholding = 15
}