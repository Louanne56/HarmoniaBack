using System.ComponentModel.DataAnnotations;

// Enumération des modes musicaux
public enum Mode
{
    Majeur, // Mode majeur
    Mineur // Mode mineur
    ,
}

// Enumération des tonalités musicales avec affichage des dièses
public enum Tonalite
{
    C, // Do

    [Display(Name = "C#")]
    CSharp, // Do#
    D, // Ré

    [Display(Name = "D#")]
    DSharp, // Ré#
    E, // Mi
    F, // Fa

    [Display(Name = "F#")]
    FSharp, // Fa#
    G, // Sol

    [Display(Name = "G#")]
    GSharp, // Sol#
    A, // La

    [Display(Name = "A#")]
    ASharp, // La#
    B // Si
    ,
}

// Enumération des styles musicaux
public enum Style
{
    Jazz, // Style jazz
    Blues, // Style blues
    Rock, // Style rock
    Pop // Style pop
    ,
}

// Représente une progression d'accords (suite d'accords)
public class ProgressionAccords
{
    // Propriétés de la progression
    public string Id { get; set; } = Guid.NewGuid().ToString(); // ID unique
    public required string Nom { get; set; } // Nom de la progression
    public required Mode Mode { get; set; } // Mode (Majeur/Mineur)
    public required Style Style { get; set; } // Style musical
    public required Tonalite Tonalite { get; set; } // Tonalité

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
