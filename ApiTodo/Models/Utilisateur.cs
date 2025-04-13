using Microsoft.AspNetCore.Identity;

public class Utilisateur : IdentityUser
{
    // Supprimez la propriété Pseudo
    public List<SuitesFavorites>? SuitesFavorites { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    // Constructeurs mis à jour
    public Utilisateur()
    {
        SuitesFavorites = new List<SuitesFavorites>();
    }

    public Utilisateur(UtilisateurDTO utilisateurDTO)
    {
        this.UserName = utilisateurDTO.UserName; // Utilisez UserName au lieu de Pseudo
    }

    public Utilisateur(InscriptionDTO inscriptionDTO)
    {
        this.Email = inscriptionDTO.Email;
        this.UserName = inscriptionDTO.Pseudo; // Utilisez uniquement UserName
        this.SecurityStamp = Guid.NewGuid().ToString();
        SuitesFavorites = new List<SuitesFavorites>();
    }
}
