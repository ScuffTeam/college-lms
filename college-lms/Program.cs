using System.Text;
using college_lms;
using college_lms.Middlewares;
using college_lms.Services;
using college_lms.Services.Interfaces;
using college_lms.Services.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    var envFile = Path.Combine(AppContext.BaseDirectory, "./.env.Development");
    if (File.Exists(envFile))
    {
        foreach (var line in File.ReadAllLines(envFile))
        {
            var parts = line.Split('=', 2);
            if (parts.Length == 2)
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
        }
    }
}

builder.Configuration.AddEnvironmentVariables();

// Configure AppSettings from environment variables
builder
    .Services.AddOptions<AppOptions>()
    .Bind(builder.Configuration.GetSection("AppOptions"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

// Configure DbContext with Npgsql
builder.Services.AddDbContext<AppDbContext>(
    (provider, options) =>
    {
        var appOptions = provider.GetRequiredService<IOptions<AppOptions>>().Value;
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = appOptions.DatabaseOptions.Host,
            Port = int.Parse(appOptions.DatabaseOptions.Port),
            Database = appOptions.DatabaseOptions.Database,
            Username = appOptions.DatabaseOptions.Username,
            Password = appOptions.DatabaseOptions.Password,
        };
        options.UseNpgsql(connectionStringBuilder.ConnectionString);
    }
);

// Setup Identity
builder
    .Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Setup Cache
builder.Services.AddStackExchangeRedisCache(options =>
    options.Configuration = builder.Configuration["AppOptions:RedisConnection"]
);

builder.Services.AddSingleton<IRefreshTokenStore, CacheService>();
builder.Services.AddSingleton<IJwtBlacklistStore, CacheService>();

// Add JWT Authentication
builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var secretKey =
            builder.Configuration.GetSection(AppOptions.Name)[nameof(AppOptions.SecretKey)]
            ?? throw new ArgumentException("No SecretKey found in env");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero,
        };
    });

// Add Authorization
builder.Services.AddAuthorization();

// Add Services
builder.Services.AddScoped<IAuthService, AuthService>();

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Use Middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
