using AutoFixture;
using AutoFixture.Kernel;
using Exizent.CaseManagement.Client.Models.EstateItems;

namespace Exizent.CaseManagement.Client.Tests;

public sealed class Harness : IDisposable
{
    public TestHttpClientHandler ClientHandler { get; }
    public ICaseManagementApiClient Client { get; }

    public Fixture Fixture { get; } = new();

    public Harness()
    {
        ClientHandler = new TestHttpClientHandler();
        Client = new CaseManagementApiClient(new HttpClient(ClientHandler)
        {
            BaseAddress = new Uri("https://testing.com")
        });
    }
    
    public void Dispose()
    {
        ClientHandler.Dispose();
    }

    public EstateItemResourceRepresentation CreateEstateItem(Type type)
    {
        return (EstateItemResourceRepresentation)new SpecimenContext(Fixture).Resolve(type);
    }
}