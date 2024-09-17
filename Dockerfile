# Etapa 1: Imagem base para o ambiente de runtime (AspNet Core)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8181

# Etapa 2: Imagem para a compilação do projeto (SDK)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo .sln e os arquivos .csproj das diversas camadas do projeto
COPY FinanceiroDashboardMVC.sln ./
COPY FinanceiroDashboardMVC.Application/FinanceiroDashboardMVC.Application.csproj ./FinanceiroDashboardMVC.Application/
COPY FinanceiroDashboardMVC.Domain/FinanceiroDashboardMVC.Domain.csproj ./FinanceiroDashboardMVC.Domain/
COPY FinanceiroDashboardMVC.Infrastructure/FinanceiroDashboardMVC.Infrastructure.csproj ./FinanceiroDashboardMVC.Infrastructure/
COPY FinanceiroDashboardMVC.Presentation/FinanceiroDashboardMVC.Presentation.csproj ./FinanceiroDashboardMVC.Presentation/

RUN dotnet restore

COPY . .

WORKDIR /src/FinanceiroDashboardMVC.Presentation
RUN dotnet build -c Release -o /app/build

# Etapa 3: Publicação do projeto
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Etapa 4: Imagem final para o ambiente de runtime
FROM base AS final
WORKDIR /app
# Copia os arquivos publicados da etapa anterior
COPY --from=publish /app/publish .

# Define o ponto de entrada para o aplicativo
ENTRYPOINT ["dotnet", "FinanceiroDashboardMVC.Presentation.dll"]
