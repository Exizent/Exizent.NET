ARG CONFIGURATION="Release"
ARG NUGET_PACKAGE_VERSION="1.0.0"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore

ARG CONFIGURATION
ENV PATH="/root/.dotnet/tools:${PATH}"

COPY ./*.props .
COPY ./*.targets .
COPY ./*.sln .
COPY ./*.png .

COPY ./src/Exizent.CaseManagement.Client/*.csproj ./src/Exizent.CaseManagement.Client/
COPY ./src/Exizent.CaseManagement.Client.Extensions.Microsoft.DependencyInjection/*.csproj ./src/Exizent.CaseManagement.Client.Extensions.Microsoft.DependencyInjection/
COPY ./tests/Exizent.CaseManagement.Client.IntegrationTests/*.csproj ./tests/Exizent.CaseManagement.Client.IntegrationTests/
COPY ./tests/Exizent.CaseManagement.Client.Tests/*.csproj ./tests/Exizent.CaseManagement.Client.Tests/
RUN dotnet restore

FROM restore as build
ARG CONFIGURATION
ARG NUGET_PACKAGE_VERSION

COPY ./src/ ./src/
COPY ./tests/ ./tests/
RUN dotnet build --configuration $CONFIGURATION --no-restore

FROM build as test
RUN dotnet test --logger "junit;LogFilePath=/TestResults/TestResults.xml" --configuration $CONFIGURATION --no-build

FROM build as pack
RUN mkdir -p artifacts
RUN dotnet pack --configuration Release -p:Version=${NUGET_PACKAGE_VERSION} --no-build --output ./artifacts

FROM scratch
COPY --from=pack /artifacts/*.nupkg /artifacts/
COPY --from=pack /artifacts/*.snupkg /artifacts/
COPY --from=test /TestResults/*.xml /TestResults/

