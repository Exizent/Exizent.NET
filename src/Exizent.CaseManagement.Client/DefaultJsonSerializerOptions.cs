using System.Text.Json;
using System.Text.Json.Serialization;
using Dahomey.Json;
using Dahomey.Json.Serialization.Conventions;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Incomes;

namespace Exizent.CaseManagement.Client;

internal static class DefaultJsonSerializerOptions
{
    public static JsonSerializerOptions Instance { get; } = CreateJsonSerializerOptions();

    private static JsonSerializerOptions CreateJsonSerializerOptions()
    {
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

        var jsonSerializerOptions = options.SetupExtensions();
        var registry = jsonSerializerOptions.GetDiscriminatorConventionRegistry();
        registry.ClearConventions();
        registry.RegisterConvention(new DefaultDiscriminatorConvention<string>(jsonSerializerOptions, "type"));
        registry.RegisterType<BankAccountResourceRepresentation>();
        registry.RegisterType<BuildingResourceRepresentation>();
        registry.RegisterType<BusinessInterestResourceRepresentation>();
        registry.RegisterType<CashIsaResourceRepresentation>();
        registry.RegisterType<CashSavingsAccountResourceRepresentation>();
        registry.RegisterType<CreditCardOrPersonalLoanResourceRepresentation>();
        registry.RegisterType<CryptocurrencyResourceRepresentation>();
        registry.RegisterType<EndowmentPolicyResourceRepresentation>();
        registry.RegisterType<FinancialGiftResourceRepresentation>();
        registry.RegisterType<IncomeBondResourceRepresentation>();
        registry.RegisterType<InvestmentBondResourceRepresentation>();
        registry.RegisterType<LandResourceRepresentation>();
        registry.RegisterType<LifePolicyResourceRepresentation>();
        registry.RegisterType<MiscellaneousAssetResourceRepresentation>();
        registry.RegisterType<MiscellaneousDebtResourceRepresentation>();
        registry.RegisterType<MoneyOwedResourceRepresentation>();
        registry.RegisterType<MortgageResourceRepresentation>();
        registry.RegisterType<NationalSavingsAndInvestmentsProductResourceRepresentation>();
        registry.RegisterType<NomineeInvestmentAccountResourceRepresentation>();
        registry.RegisterType<PensionResourceRepresentation>();
        registry.RegisterType<PhysicalShareholdingResourceRepresentation>();
        registry.RegisterType<PremiumBondResourceRepresentation>();
        registry.RegisterType<SecuredLoanResourceRepresentation>();
        registry.RegisterType<StateBenefitOverpaymentResourceRepresentation>();
        registry.RegisterType<StoreCardOrCatalogueAccountResourceRepresentation>();
        registry.RegisterType<UKGovAndMunicipalSecuritiesResourceRepresentation>();
        registry.RegisterType<UnitTrustResourceRepresentation>();
        registry.RegisterType<VehicleFinanceResourceRepresentation>();
        registry.RegisterType<VehicleResourceRepresentation>();
        
        registry.RegisterType<IncomeResourceRepresentation>();
        registry.RegisterType<ReceiptIncomeResourceRepresentation>();
        registry.RegisterType<SumsReceivedIncomeResourceRepresentation>();
        

        return options;
    }
}