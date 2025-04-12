public class UtilisateurCreateDTO
{
    public string? Pseudo { get; set; }

    public UtilisateurCreateDTO() { }

    public UtilisateurCreateDTO(Utilisateur utilisateur)
    {
        Pseudo = utilisateur.Pseudo;
    }
}