using AutoFixture;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Company;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Models.Incomes;
using Exizent.CaseManagement.Client.Models.Organisations;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class CaseResourceRepresentationBuilder
{
    private readonly Fixture _fixture = new();
    private CompanyResourceRepresentation? _company;
    private DeceasedResourceRepresentation? _deceased;
    private FinancialGiftsDetailsResourceRepresentation? _financialGiftsDetails;
    private readonly List<PersonResourceRepresentation> _people = new();
    private readonly List<OrganisationResourceRepresentation> _organisations = new();
    private readonly List<EstateItemResourceRepresentation> _estateItems = new();
    private readonly List<ExpenseResourceRepresentation> _expenses = new();
    private readonly List<GetIncomeBaseResourceRepresentation> _incomes = new();

    public CaseResourceRepresentationBuilder With(CompanyResourceRepresentation? company)
    {
        _company = company;
        return this;
    }

    public CaseResourceRepresentationBuilder With(DeceasedResourceRepresentation deceased)
    {
        _deceased = deceased;
        return this;
    }

    public CaseResourceRepresentationBuilder With(FinancialGiftsDetailsResourceRepresentation gift)
    {
        _financialGiftsDetails = gift;
        return this;
    }

    public CaseResourceRepresentationBuilder With(PersonResourceRepresentation person)
    {
        _people.Add(person);
        return this;
    }

    public CaseResourceRepresentationBuilder With(OrganisationResourceRepresentation organisation)
    {
        _organisations.Add(organisation);
        return this;
    }

    public CaseResourceRepresentationBuilder With(EstateItemResourceRepresentation estateItem)
    {
        _estateItems.Add(estateItem);
        return this;
    }

    public CaseResourceRepresentationBuilder With(ExpenseResourceRepresentation expense)
    {
        _expenses.Add(expense);
        return this;
    }

    public CaseResourceRepresentationBuilder With(GetIncomeBaseResourceRepresentation getIncome)
    {
        _incomes.Add(getIncome);
        return this;
    }

    public CaseResourceRepresentation Build()
    {
        return new CaseResourceRepresentation
        {
            Id = Guid.NewGuid(),
            Company = _company,
            Deceased = _deceased ?? _fixture.Create<DeceasedResourceRepresentation>(),
            FinancialGiftsDetails = _financialGiftsDetails = new FinancialGiftsDetailsResourceRepresentation(),
            People = _people.AsReadOnly(),
            EstateItems = _estateItems,
            Organisations = _organisations,
            Expenses = _expenses,
            Incomes = _incomes,
        };
    }
}