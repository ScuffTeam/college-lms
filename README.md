# college-lms-api

## HOW TO START

### Docker

1. Copy .env example file and provide configurations
```sh
cp .env.example .env
```
2. Run with docker-compose
```sh
docker-compose up -d
```

### Manual

1. Copy development env in project dir and provide same variable values as .env
```sh
cp env.Development.example env.Development
```
2. Use `dotnet run` with variables provided 

## Migrations

### Docker

1. Run step 1 from Manual start and use
```sh
dotnet ef --project college-lms migrations bundle --self-contained -r linux-x64 --force
```
2. Docker pulls file automatically, run /app/efbundle on machine

### Manual

Use default dotnet ef core migrations
