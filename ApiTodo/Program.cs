using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

// Initialisation de l'application web
var builder = WebApplication.CreateBuilder(args);

/******************************************
 * CONFIGURATION DES SERVICES (DI)
 ******************************************/

// Configuration CORS (Cross-Origin Resource Sharing)
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin() // Autorise toutes les origines (à modifier en production)
                .AllowAnyHeader() // Autorise tous les en-têtes HTTP
                .AllowAnyMethod(); // Autorise toutes les méthodes HTTP (GET, POST, etc.)
        }
    );
});

// Configuration de l'authentification JWT (JSON Web Tokens)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, // Vérifie la signature du token
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"] ?? "") // Clé secrète
            ),
            ValidateIssuer = false, // Désactive la validation de l'émetteur
            ValidateAudience = false, // Désactive la validation de l'audience
            ValidateLifetime = true, // Vérifie la date d'expiration
        };
    });

// Configuration d'Identity (gestion des utilisateurs)
builder.Services.AddIdentity<Utilisateur, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true; // Email unique obligatoire
        options.SignIn.RequireConfirmedEmail = false; // Pas de confirmation par email requise
    })
    .AddEntityFrameworkStores<HarmoniaContext>() // Utilise EF Core pour le stockage
    .AddDefaultTokenProviders(); // Ajoute les fournisseurs de tokens par défaut

// Configuration de l'écoute sur toutes les interfaces réseau
builder.WebHost.UseUrls("http://0.0.0.0:5007");

// Ajout des services essentiels
builder.Services.AddDbContext<HarmoniaContext>(); // Ajout du contexte EF Core
builder.Services.AddControllers(); // Support des controllers API
builder.Services.AddEndpointsApiExplorer(); // Nécessaire pour Swagger
builder.Services.AddSwaggerGen(); // Génération de la documentation API

/******************************************
 * CONSTRUCTION DE L'APPLICATION
 ******************************************/
var app = builder.Build();

// Application des migrations et seeding initial
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<HarmoniaContext>();
    context.Database.Migrate(); // Applique automatiquement les migrations en attente

    if (context.Database.GetPendingMigrations().Any()) 
    {
        // Exécute le seeding seulement si des migrations viennent d'être appliquées
        SeedData.seed();
    }
}

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Active Swagger
    app.UseSwaggerUI(); // Active l'interface Swagger UI
}

app.UseHttpsRedirection(); // Redirection HTTP vers HTTPS
app.UseCors("AllowAll"); // Active la politique CORS configurée
app.UseAuthentication(); // Active l'authentification
app.UseAuthorization(); // Active l'autorisation
app.UseStaticFiles(); // Permet de servir des fichiers statiques
app.MapControllers(); // Mappe les routes des controllers
app.Run(); // Lance l'application