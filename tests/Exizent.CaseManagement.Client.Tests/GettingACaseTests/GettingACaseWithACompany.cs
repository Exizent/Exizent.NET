using AutoFixture;
using Exizent.CaseManagement.Client.Models;
using Exizent.CaseManagement.Client.Models.Company;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public class GettingACaseWithACompany : IClassFixture<Harness>
{
    private readonly Harness _harness;
    public GettingACaseWithACompany(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnACaseWithCompanyDetails()
    {

        var expectedCompany = _harness.Fixture.Create<CompanyResourceRepresentation>();

        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(expectedCompany)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseWithCompanyResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id, new GetCaseOptions {ExpandCompany = true} );

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Company.Should().BeEquivalentTo(caseResourceRepresentation.Company);
    }
    
    [Fact]
    public async Task ShouldReturnACaseWithNullCompany()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id, new GetCaseOptions {ExpandCompany = false} );

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.Id.Should().Be(caseResourceRepresentation.Id);
        caseDetails.Company.Should().BeNull();
    }

}