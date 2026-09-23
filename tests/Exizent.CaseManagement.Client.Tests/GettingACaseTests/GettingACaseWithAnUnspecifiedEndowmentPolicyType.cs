using Exizent.CaseManagement.Client.Models.EstateItems;
using Exizent.CaseManagement.Client.Tests.JsonBuilders;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Exizent.CaseManagement.Client.Tests.GettingACaseTests;

/// <summary>
/// Endowment policies back-filled from the legacy backend can have no policy type, which the API returns as
/// "NotSpecified". One such item must not stop the rest of the case from being read.
/// </summary>
public sealed class GettingACaseWithAnUnspecifiedEndowmentPolicyType : IClassFixture<Harness>
{
    private readonly Harness _harness;

    public GettingACaseWithAnUnspecifiedEndowmentPolicyType(Harness harness) => _harness = harness;

    [Fact]
    public async Task ShouldReadTheCaseWithThePolicyTypeNotSpecified()
    {
        var endowmentPolicy = _harness.CreateEstateItem(typeof(EndowmentPolicyResourceRepresentation));
        var bankAccount = _harness.CreateEstateItem(typeof(BankAccountResourceRepresentation));
        var caseResourceRepresentation = new CaseResourceRepresentationBuilder()
            .With(endowmentPolicy)
            .With(bankAccount)
            .Build();

        var body = CaseJsonBuilder.Build(caseResourceRepresentation);
        body["estateItems"]![0]!["policyType"] = "NotSpecified";
        _harness.ClientHandler.AddGetCaseResponse(caseResourceRepresentation.Id, body.ToJsonString());

        var caseDetails = await _harness.Client.GetCase(caseResourceRepresentation.Id);

        using var _ = new AssertionScope();
        caseDetails!.EstateItems.Should().HaveCount(2);
        caseDetails.EstateItems.OfType<EndowmentPolicyResourceRepresentation>().Single().PolicyType
            .Should().Be(EndowmentPolicyType.NotSpecified);
        caseDetails.EstateItems.OfType<BankAccountResourceRepresentation>().Single()
            .Should().BeEquivalentTo(bankAccount);
    }

    [Fact]
    public async Task ShouldReadTheEstateItemWithThePolicyTypeNotSpecified()
    {
        var endowmentPolicy = (EndowmentPolicyResourceRepresentation)_harness.CreateEstateItem(
            typeof(EndowmentPolicyResourceRepresentation));
        var caseId = Guid.NewGuid();

        var body = CaseJsonBuilder.Build(new CaseResourceRepresentationBuilder().With(endowmentPolicy).Build());
        var estateItem = body["estateItems"]![0]!;
        estateItem["policyType"] = "NotSpecified";
        _harness.ClientHandler.AddGetEstateItemResponse(caseId, endowmentPolicy.Id, estateItem.ToJsonString());

        var result = await _harness.Client.GetEstateItem(caseId, endowmentPolicy.Id);

        result.Should().BeOfType<EndowmentPolicyResourceRepresentation>()
            .Which.PolicyType.Should().Be(EndowmentPolicyType.NotSpecified);
    }
}
