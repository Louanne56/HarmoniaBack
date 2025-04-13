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
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string Nom { get; set; }
    public required Mode Mode { get; set; }
    public required Style Style { get; set; }
    public required Tonalite Tonalite { get; set; }
    public List<Accord> Accords { get; set; } = new List<Accord>();

    // Constructeur par défaut privé (à utiliser seulement par EF Core)
    public ProgressionAccords() { }

    public ProgressionAccords(string nom, Mode mode, Style style, Tonalite tonalite)
    {
        Id = Guid.NewGuid().ToString();
        Nom = nom ?? throw new ArgumentException("Nom is required");
        Mode = mode;
        Style = style;
        Tonalite = tonalite;
    }

    public ProgressionAccords(ProgressionAccordDTO progressionAccordDTO)
    {
        // Si l'ID du DTO est fourni, utilisez-le, sinon générez-en un nouveau
        this.Id = !string.IsNullOrEmpty(progressionAccordDTO.Id)
            ? progressionAccordDTO.Id
            : Guid.NewGuid().ToString();

        // Affectation des valeurs
        this.Nom = progressionAccordDTO.Nom ?? throw new ArgumentException("Nom is required");
        this.Mode = progressionAccordDTO.Mode;
        this.Style = progressionAccordDTO.Style;
        this.Tonalite = progressionAccordDTO.Tonalite;
    }
}
