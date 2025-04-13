using Microsoft.AspNetCore.Identity;

// Modèle Utilisateur étendant le framework Identity
// (Système d'authentification ASP.NET Core)

public class Utilisateur : IdentityUser
{
    public List<SuitesFavorites>? SuitesFavorites { get; set; }

    // Propriétés pour le refresh token
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    // Constructeur par défaut
    public Utilisateur()
    {
        SuitesFavorites = new List<SuitesFavorites>();
    }

    // Constructeur à partir d'un DTO utilisateur
    public Utilisateur(UtilisateurDTO utilisateurDTO)
    {
        this.UserName = utilisateurDTO.UserName;
    }

    // Constructeur à partir d'un DTO d'inscription
    public Utilisateur(InscriptionDTO inscriptionDTO)
    {
        this.Email = inscriptionDTO.Email;
        this.UserName = inscriptionDTO.Pseudo;
        this.SecurityStamp = Guid.NewGuid().ToString();
        SuitesFavorites = new List<SuitesFavorites>();
    }
}
