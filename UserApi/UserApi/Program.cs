using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OpenIddict.Abstractions;
using OpenIddict.Server;
using System.Security.Cryptography;
using UserApi.Data;
using UserApi.Data.Models;
using UserApi.Repositories;
using UserApi.Services;
using UserApi.Infrastructure.Seeders;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injection des services
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Repository pour les utilisateurs
builder.Services.AddScoped<UserService>(); // Service pour la logique métier des utilisateurs


// Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// ► OpenIddict
var encryptionCertStream = File.OpenRead("keys/encryption.pfx");
builder.Services.AddOpenIddict()
    // ── Persistance EF Core (clients, scopes…)
    .AddCore(opt => opt
        .UseEntityFrameworkCore()
        .UseDbContext<AppDbContext>())

    // ── Serveur OAuth2 / OIDC
    .AddServer(opt =>
    {
        opt.SetTokenEndpointUris("/login");      // flow password/refresh
        opt.AllowPasswordFlow()
           .AllowRefreshTokenFlow();

        // Scopes si besoin
        opt.RegisterScopes(OpenIddictConstants.Scopes.Email,
                           OpenIddictConstants.Scopes.Profile,
                           OpenIddictConstants.Scopes.Roles);

        // ── Clé RSA (signature RS256)
        var rsa = RSA.Create();
        rsa.ImportFromPem(File.ReadAllText(builder.Configuration["Jwt:PrivateKeyPath"]));
        opt.AddSigningKey(new RsaSecurityKey(rsa));
        opt.AddEncryptionCertificate(encryptionCertStream, "motdepasse");

        if (builder.Environment.IsDevelopment())
        {
            opt.UseAspNetCore().DisableTransportSecurityRequirement();
        }
        
        // Pipeline ASP.NET
        opt.UseAspNetCore()
           .EnableTokenEndpointPassthrough(); // la réponse JSON sort directement
    })

    // ── Validation des JWT pour tes autres APIs (ou la même)
    .AddValidation(opt =>
    {
        opt.UseLocalServer();   // même serveur que l’émetteur
        opt.UseAspNetCore();
    });

// Auth/Autorisation
builder.Services.AddAuthentication()
                .AddJwtBearer(); // déjà géré par OpenIddict.Validation

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();
await OpenIddictSeeder.SeedAsync(app.Services);
app.Lifetime.ApplicationStopped.Register(() => encryptionCertStream.Dispose());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
        {
            var prefix = httpReq.Headers["X-Forwarded-Prefix"].FirstOrDefault();
            if (!string.IsNullOrEmpty(prefix))
            {
                swaggerDoc.Servers = new List<OpenApiServer> {
                new OpenApiServer { Url = prefix }
            };
            }
        });
    });
    app.UseSwaggerUI();
}

if (!builder.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
