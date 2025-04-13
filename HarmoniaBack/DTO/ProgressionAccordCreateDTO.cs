public class ProgressionAccordCreateDTO
{
    public string? Nom { get; set; } = string.Empty; // Nom de la progression d'accords
    public required Tonalite Tonalite { get; set; } // Tonalité de la progression d'accords
    public required Mode Mode { get; set; }
    public required Style Style { get; set; }
    public List<string> Accords { get; set; } = new List<string>();

    public ProgressionAccordCreateDTO() { }
}
