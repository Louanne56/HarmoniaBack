public class SuitesFavoritesDTO
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string ProgressionAccordsId { get; set; }
    public ProgressionAccordDTO? ProgressionAccords { get; set; }

    public SuitesFavoritesDTO()
    {
        // Explicitement initialiser ces propriétés
        Id = string.Empty;
        UserId = string.Empty;
        ProgressionAccordsId = string.Empty;
    }

    public SuitesFavoritesDTO(SuitesFavorites suitesFavorites)
    {
        this.Id = suitesFavorites.Id;
        this.UserId = suitesFavorites.UserId;
        this.ProgressionAccordsId = suitesFavorites.ProgressionAccordsId;

        if (suitesFavorites.ProgressionAccords != null)
        {
            this.ProgressionAccords = new ProgressionAccordDTO(suitesFavorites.ProgressionAccords);
        }
    }
}
