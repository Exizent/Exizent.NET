namespace Exizent.CaseManagement.Client.Models.Expenses;

public enum SourceOfFunds
{
    DeceasedBankAccount = 1,
    CashSavingsAccount,
    CashIsa,
    ClientCashAccount,
    ProceedsOfSale,
    PrepaidFuneralPlan,
    PremiumBond,
    NationalSavingsAndInvestmentsProduct,
    NationalSavingsAndInvestmentsIncomeBond = 9,
    Pension,
    LifePolicy,
#pragma warning disable CA1069 // Enums values should not be duplicated
    IncomeBond = 9
}