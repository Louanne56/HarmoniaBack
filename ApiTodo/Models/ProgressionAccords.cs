using System.ComponentModel.DataAnnotations;

public enum Mode
{
    Majeur,
    Mineur,
}

public enum Tonalite
{
    C,

    [Display(Name = "C#")]
    CSharp,
    D,

    [Display(Name = "D#")]
    DSharp,
    E,
    F,

    [Display(Name = "F#")]
    FSharp,
    G,

    [Display(Name = "G#")]
    GSharp,
    A,

    [Display(Name = "A#")]
    ASharp,
    B,
}

public enum Style
{
    Jazz,
    Blues,
    Rock,
    Pop,
}

public class ProgressionAccords
{
    public string Id { get; set; }
    public string? Nom { get; set; }
    public Mode? Mode { get; set; }
    public Style? Style { get; set; }
    public Tonalite? Tonalite { get; set; }
    public List<Accord> Accords { get; set; } = new List<Accord>();


    public ProgressionAccords()
    {
        Id = Guid.NewGuid().ToString(); // Générer un ID unique dans le constructeur par défaut
    }

    public ProgressionAccords(ProgressionAccordDTO progressionAccordDTO)
    {
        this.Id = !string.IsNullOrEmpty(progressionAccordDTO.Id)
            ? progressionAccordDTO.Id
            : Guid.NewGuid().ToString();
        this.Nom = progressionAccordDTO.Nom;
        this.Mode = progressionAccordDTO.Mode;
        this.Style = progressionAccordDTO.Style;
        this.Tonalite = progressionAccordDTO.Tonalite;
    }
}
