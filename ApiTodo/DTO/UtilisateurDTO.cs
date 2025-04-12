public class UtilisateurDTO
{
    public string Id { get; set; } // Changé de int à string
    public string? Pseudo { get; set; }
    public string Email { get; set; } 

    public UtilisateurDTO() { }

    public UtilisateurDTO(Utilisateur utilisateur)
    {
        this.Id = utilisateur.Id; // Maintenant l'Id est un string
        this.Pseudo = utilisateur.Pseudo;
    }
}