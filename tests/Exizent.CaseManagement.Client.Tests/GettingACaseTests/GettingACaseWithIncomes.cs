using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithIncomes : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithIncomes(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyIncomes()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Incomes.Should().BeEmpty();
    }

    [Theory]
    [AllIncomeResourceRepresentationTypesData]
    public async Task ShouldReturnIncome(Type incomeResourceRepresentationType)
    {
        var income = _harness.CreateIncome(incomeResourceRepresentationType);

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(income)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Incomes.Single().Should()
            .BeEquivalentTo(income, o => o.RespectingRuntimeTypes());
    }
}