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
 
         // Personnaliser les noms des tables Identity si nécessaire
         builder.Entity<Utilisateur>().ToTable("Utilisateurs");
         builder.Entity<IdentityRole>().ToTable("Roles");
         builder.Entity<IdentityUserRole<string>>().ToTable("UtilisateursRoles");
         builder.Entity<IdentityUserClaim<string>>().ToTable("UtilisateursClaims");
         builder.Entity<IdentityUserLogin<string>>().ToTable("UtilisateursLogins");
         builder.Entity<IdentityUserToken<string>>().ToTable("UtilisateursTokens");
         builder.Entity<IdentityRoleClaim<string>>().ToTable("RolesClaims");
         builder.Entity<ProgressionAccords>()
         .HasMany(p => p.Accords)
         .WithMany(a => a.Progressions)
         .UsingEntity(j => j.ToTable("ProgressionAccord")); // Table de jonction
     }
 }