using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class HarmoniaContext : IdentityDbContext<Utilisateur>
{
    public DbSet<Accord> Accords { get; set; } = null!;
    public DbSet<ProgressionAccords> ProgressionAccords { get; set; } = null!;

    // Utilisateurs est déjà inclus via IdentityDbContext<Utilisateur>
    public DbSet<SuitesFavorites> SuitesFavorites { get; set; } = null!;


    public string DbPath { get; private set; }

    public HarmoniaContext()
    {
        // Path to SQLite database file
        DbPath = "Harmonia.db";
    }

    // The following configures EF to create a SQLite database file locally
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // Use SQLite as database
        options.UseSqlite($"Data Source={DbPath}");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Ne pas oublier cette ligne !

       // Désactive la création des tables inutiles générées automatiquement par ASP.NET Core Identity
        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserToken<string>>();
        builder.Ignore<IdentityRole>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityUserRole<string>>();

        // Tes personnalisations
        builder.Entity<Utilisateur>().ToTable("Utilisateurs");

        _ = builder
            .Entity<ProgressionAccords>()
            .HasMany(p => p.Accords)
            .WithMany(a => a.Progressions)
            .UsingEntity(j => j.ToTable("ProgressionAccord")); // Table de jonction
    }
}
