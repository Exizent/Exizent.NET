# Exizent.NET
A simple .NET client wrapper for Exizent API

[![install from nuget](http://img.shields.io/nuget/v/Exizent.CaseManagement.Client.svg?style=flat-square)](https://www.nuget.org/packages/Exizent.CaseManagement.Client)
[![downloads](http://img.shields.io/nuget/dt/Exizent.CaseManagement.Client.svg?style=flat-square)](https://www.nuget.org/packages/Exizent.CaseManagement.Client)
[![Continuous Integration Workflow](https://github.com/Exizent/Exizent.NET/actions/workflows/continuous-integration-workflow.yml/badge.svg)](https://github.com/Exizent/Exizent.NET/actions/workflows/continuous-integration-workflow.yml)

## Getting Started

### ASP.NET Core

Exizent.NET comes with some extensions for the .NET dependency injection, these can be installed via the package manager console by executing the following commandlet:

```powershell
PM> Install-Package Exizent.CaseManagement.Client.Extensions.Microsoft.DependencyInjection
```

alternative you can use the dotnet CLI.

```bash
dotnet add package Exizent.CaseManagement.Client.Extensions.Microsoft.DependencyInjection
```

Once we have the package installed, we can then configure the IoC container to use the Exizent.NET client by adding the following line to the program.cs or startup.cs:

```csharp
builder.Services.AddExizentCaseManagementClient(
    "my-client-id",
    "my-client-secret"
);
```

We can now take a dependancy on `ICaseManagementClient` and use it to interact with the Exizent API.

```csharp
[ApiController]
[Route("[controller]")]
public class CaseController : ControllerBase
{
    private readonly ICaseManagementApiClient _caseManagementApiClient;

    public CaseController(ICaseManagementApiClient caseManagementApiClient)
        => _caseManagementApiClient = caseManagementApiClient;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _caseManagementApiClient.GetCase(Guid.Parse("9FCE0394-05AC-4B89-8915-1706CEB7113F")));
    }
}
```

Under the hood this is using [typed clients](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients) for the `HttpClient` used by Exizent.NET and it's also possible to use this package with any dependency injection framework that implements `Microsoft.Extensions.DependencyInjection.Abstractions`.

## Usage

### Getting a case

To fetch for a case resource, we can pass the case id to the `GetCase` method:

```csharp
var caseId = Guid.Parse("9FCE0394-05AC-4B89-8915-1706CEB7113F");
var result = await _caseManagementApiClient.GetCase(caseId);

foreach (var person in result.People)
{
    Console.WriteLine(person.FirstName + " " + person.LastName);
}
```

## Contributing

1. Fork
1. Changes
1. Pull Request

