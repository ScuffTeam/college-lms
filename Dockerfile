# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY college-lms/*.csproj ./college-lms/
RUN dotnet restore

# copy everything else and build app
COPY college-lms/. ./college-lms/
WORKDIR /source/college-lms
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "college-lms.dll"]
EXPOSE ${APP_PORT}

COPY ./efbundle ./
RUN if [ -f "efbundle" ]; then \
  echo "✅ Found efbundle file, copying..."; \
else \
  echo "⚠️ efbundle file not found, skipping copy"; \
fi
