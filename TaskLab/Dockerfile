FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskLab/TaskLab.csproj", "TaskLab/"]
RUN dotnet restore "TaskLab/TaskLab.csproj"
COPY . .
WORKDIR "/src/TaskLab"
RUN dotnet build "TaskLab.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskLab.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskLab.dll"]
