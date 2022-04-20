using AutoFixture;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Deceased;
using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Models.Expenses;
using Exizent.CaseManagement.Client.Models.People;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class CaseResourceRepresentationBuilder
{
    private readonly Fixture _fixture = new();
    private DeceasedResourceRepresentation? _deceased;
    private readonly List<PersonResourceRepresentation> _people = new();
    private readonly List<EstateItemResourceRepresentation> _estateItems = new();
    private readonly List<ExpenseResourceRepresentation> _expenses = new();

    public CaseResourceRepresentationBuilder With(DeceasedResourceRepresentation deceased)
    {
        _deceased = deceased;
        return this;
    }

    public CaseResourceRepresentationBuilder With(PersonResourceRepresentation person)
    {
        _people.Add(person);
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

    public CaseResourceRepresentation Build()
    {
        return new CaseResourceRepresentation
        {
            Id = Guid.NewGuid(),
            Deceased = _deceased ?? _fixture.Create<DeceasedResourceRepresentation>(),
            People = _people.AsReadOnly(),
            EstateItems = _estateItems,
            Expenses = _expenses
        };
    }
}