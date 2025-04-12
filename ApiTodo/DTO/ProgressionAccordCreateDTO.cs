public class ProgressionAccordCreateDTO
{
    public string? Nom { get; set; }
    public Tonalite? Tonalite { get; set; }
    public Mode? Mode { get; set; }
    public Style? Style { get; set; }
    public List<string> Accords { get; set; } = new List<string>();

    public ProgressionAccordCreateDTO() { }
}
