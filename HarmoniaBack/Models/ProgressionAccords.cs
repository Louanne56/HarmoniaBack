using System.ComponentModel.DataAnnotations;

// Enumération des modes musicaux
public enum Mode
{
    Majeur,
    Mineur,
}

// Enumération des tonalités musicales avec affichage des dièses
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

// Enumération des styles musicaux
public enum Style
{
    Jazz,
    Blues,
    Rock,
    Pop,
}

// Représente  une progression d'accords (suite d'accords)
public class ProgressionAccords
{
    // Propriétés de la progression
    public string Id { get; set; } = Guid.NewGuid().ToString(); 
    public required string Nom { get; set; }
    public required Mode Mode { get; set; } 
    public required Style Style { get; set; } 
    public required Tonalite Tonalite { get; set; } 

    // Liste des accords composant la progression
    public List<Accord> Accords { get; set; } = new List<Accord>();

    // Constructeur pour EF Core
    public ProgressionAccords() { }

    // Constructeur principal
    public ProgressionAccords(string nom, Mode mode, Style style, Tonalite tonalite)
    {
        Id = Guid.NewGuid().ToString();
        Nom = nom ?? throw new ArgumentException("Nom is required");
        Mode = mode;
        Style = style;
        Tonalite = tonalite;
    }

    // Constructeur à partir d'un DTO
    public ProgressionAccords(ProgressionAccordDTO progressionAccordDTO)
    {
        Id = !string.IsNullOrEmpty(progressionAccordDTO.Id)
            ? progressionAccordDTO.Id
            : Guid.NewGuid().ToString();
        Nom = progressionAccordDTO.Nom ?? throw new ArgumentException("Nom is required");
        Mode = progressionAccordDTO.Mode;
        Style = progressionAccordDTO.Style;
        Tonalite = progressionAccordDTO.Tonalite;
    }
}
