using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

SeedData.seed(); // Appel de la méthode de seed pour initialiser la base de données

var builder = WebApplication.CreateBuilder(args);

// Configuration CORS pour permettre l'accès depuis n'importe quelle origine
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy
                .AllowAnyOrigin() // Permet à toute origine d'accéder à l'API (à restreindre en production)
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

// Configuration JWT
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

builder
    .Services.AddIdentity<Utilisateur, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = false;
    })
    .AddEntityFrameworkStores<HarmoniaContext>()
    .AddDefaultTokenProviders();

// Permet à l'API d'écouter sur toutes les interfaces réseau
builder.WebHost.UseUrls("http://0.0.0.0:5007");

// Ajout des services nécessaires
builder.Services.AddDbContext<HarmoniaContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll"); // Appliquer la politique CORS
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapControllers();
app.Run();
