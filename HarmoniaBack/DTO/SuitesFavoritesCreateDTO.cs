public class SuitesFavoritesCreateDTO
{
    public required string UserId { get; set; } // ID de l'utilisateur
    public required string ProgressionAccordsId { get; set; } // ID de la suite d'accords

    public SuitesFavoritesCreateDTO() { }
}
