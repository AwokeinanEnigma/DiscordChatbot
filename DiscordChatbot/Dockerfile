FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DiscordChatbot/DiscordChatbot.csproj", "DiscordChatbot/"]
RUN dotnet restore "DiscordChatbot/DiscordChatbot.csproj"
COPY . .
WORKDIR "/src/DiscordChatbot"
RUN dotnet build "DiscordChatbot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DiscordChatbot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DiscordChatbot.dll"]
