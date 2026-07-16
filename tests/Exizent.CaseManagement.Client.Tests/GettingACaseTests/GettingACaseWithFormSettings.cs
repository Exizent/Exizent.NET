using Exizent.CaseManagement.Client.Models.FormSettings;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

public sealed class GettingACaseWithFormSettings : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithFormSettings(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReturnEmptyRegistrarsToGroup()
    {
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .Build();

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id,
            CaseJsonBuilder.Build(caseResourceRepresentation).ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.FormSettings.Should().NotBeNull();
        caseDetails.FormSettings.C1.RegistrarsToGroup.Should().BeEmpty();
    }

    [Fact]
    public async Task ShouldDeserialiseRegistrarsToGroupOnC1FormSettings()
    {
        var expectedRegistrars = new[] { "Equiniti", "Computershare" };
        var formSettings = new FormSettingsResourceRepresentation
        {
            C1 = new C1FormSettingsResourceRepresentation
            {
                RegistrarsToGroup = expectedRegistrars
            }
        };
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(formSettings)
            .Build();

        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id,
            CaseJsonBuilder.Build(caseResourceRepresentation).ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails.Should().NotBeNull();
        caseDetails!.FormSettings.C1.RegistrarsToGroup.Should()
            .BeEquivalentTo(expectedRegistrars);
    }
}
