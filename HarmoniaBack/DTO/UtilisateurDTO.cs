public class UtilisateurDTO
{
    public string Id { get; set; } // Changé de int à string
    public string? UserName { get; set; }
    public string? Email { get; set; }

    public UtilisateurDTO()
    {
        // Explicitement initialiser ces propriétés
        Id = string.Empty;
        UserName = string.Empty;
        Email = string.Empty; // Omets cette ligne si Email est nullable
    }

    public UtilisateurDTO(Utilisateur utilisateur)
    {
        this.Id = utilisateur.Id; // Maintenant l'Id est un string
        this.UserName = utilisateur.UserName;
    }
}
