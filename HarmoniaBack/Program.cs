/*
 * Program.cs - Point d'entrée de l'application web API Harmonia
 * Configure les services, le middleware et initialise l'application et la base de données.
 */

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

// Initialisation de l'application web
var builder = WebApplication.CreateBuilder(args);

// CONFIGURATION DES SERVICES (DI)
// Configuration CORS - Permet les requêtes cross-origin de n'importe quelle source
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

// Configuration JWT - Système d'authentification par tokens
builder
    .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"] ?? "")
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
        };
    });

// Configuration Identity - Système de gestion des utilisateurs
builder
    .Services.AddIdentity<Utilisateur, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<HarmoniaContext>()
    .AddDefaultTokenProviders();

// Configuration réseau et services essentiels
builder.WebHost.UseUrls("http://0.0.0.0:5007");
builder.Services.AddDbContext<HarmoniaContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CONSTRUCTION ET CONFIGURATION DE L'APPLICATION
var app = builder.Build();

// Initialisation de la base de données - Migrations et remplissage initial
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HarmoniaContext>();
    context.Database.Migrate();
    
    // Remplissage initial uniquement si la base est vide
    if (!context.Accords.Any())
    {
        SeedData.seed();
    }
}

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configuration du middleware de l'application
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();

// Démarre l'application
app.Run();