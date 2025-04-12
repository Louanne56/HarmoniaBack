using Microsoft.AspNetCore.Identity;

public class Utilisateur : IdentityUser
{
    // L'Id et email sera hérité de IdentityUser, donc vous n'avez pas besoin de le redéclarer
    // Par contre, notez que IdentityUser utilise un Id de type string

    public string? Pseudo { get; set; }
    public List<SuitesFavorites>? SuitesFavorites { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }

    // Constructeur par défaut
    public Utilisateur()
    {
        SuitesFavorites = new List<SuitesFavorites>();
    }

    // Constructeur pour mapper un UtilisateurDTO vers Utilisateur
    public Utilisateur(UtilisateurDTO utilisateurDTO)
    {
        // Attention: si vous passiez l'Id depuis le DTO, vous devrez le convertir en string
        // this.Id = utilisateurDTO.Id.ToString();
        this.Pseudo = utilisateurDTO.Pseudo;
    }

    public Utilisateur(InscriptionDTO inscriptionDTO)
    {
        this.Pseudo = inscriptionDTO.Pseudo; // pour mon modèle métier
        this.Email = inscriptionDTO.Email;
        this.UserName = inscriptionDTO.Pseudo; // pour Identity
        this.SecurityStamp = Guid.NewGuid().ToString(); // pour Identity
        SuitesFavorites = new List<SuitesFavorites>();
    }
}
