public class SuitesFavorites
{
    public string Id { get; set; }
    public required string UserId { get; set; }

    public ProgressionAccords? ProgressionAccords { get; set; }
    public required string ProgressionAccordsId { get; set; }

    public SuitesFavorites()
    {
        Id = Guid.NewGuid().ToString(); // Générer un ID unique dans le constructeur par défaut
    }

    public SuitesFavorites(string userId, string progressionAccordsId)
    {
        Id = Guid.NewGuid().ToString(); // Générer un ID unique
        this.UserId = userId;
        this.ProgressionAccordsId = progressionAccordsId;
    }

    // Constructeur pour mapper un SuitesFavoritesDTO vers SuitesFavorites
}
