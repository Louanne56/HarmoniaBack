public class SuitesFavorites
{
    public string Id { get; set; }
    public string UserId { get; set; }

    public ProgressionAccords? ProgressionAccords { get; set; }
    public string ProgressionAccordsId { get; set; }

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
    public SuitesFavorites(SuitesFavoritesDTO suitesFavoritesDTO)
    {
        // Si l'ID du DTO est fourni, utilisez-le, sinon générez-en un nouveau
        this.Id = !string.IsNullOrEmpty(suitesFavoritesDTO.Id)
            ? suitesFavoritesDTO.Id
            : Guid.NewGuid().ToString();
        this.UserId = suitesFavoritesDTO.UserId;
        this.ProgressionAccordsId = suitesFavoritesDTO.ProgressionAccordsId;

        if (suitesFavoritesDTO.ProgressionAccords != null)
        {
            this.ProgressionAccords = new ProgressionAccords(suitesFavoritesDTO.ProgressionAccords);
        }
    }
}
