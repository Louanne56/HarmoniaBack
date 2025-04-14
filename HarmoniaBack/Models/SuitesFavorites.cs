// Représente une progression favorite d'un utilisateur
public class SuitesFavorites
{
    public string Id { get; set; } 
    public required string UserId { get; set; } 
    public ProgressionAccords? ProgressionAccords { get; set; }
    public required string ProgressionAccordsId { get; set; } // ID de la progression

    // Constructeur par défaut
    public SuitesFavorites()
    {
        Id = Guid.NewGuid().ToString();
    }

    // Constructeur avec paramètres
    public SuitesFavorites(string userId, string progressionAccordsId)
    {
        Id = Guid.NewGuid().ToString();
        UserId = userId;
        ProgressionAccordsId = progressionAccordsId;
    }
}
