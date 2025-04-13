public class UtilisateurCreateDTO
{
    public string? UserName { get; set; }

    public UtilisateurCreateDTO() { }

    public UtilisateurCreateDTO(Utilisateur utilisateur)
    {
        UserName = utilisateur.UserName;
    }
}
