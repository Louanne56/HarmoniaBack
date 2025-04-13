public class Accord
{
    public string Id { get; set; }
    public required string Nom { get; set; }
    public required string Diagram1 { get; set; } // attribut qui stocke le chemin de l'image du diagram de l'accord
    public string? Diagram2 { get; set; }
    public required string Audio { get; set; } // attribut qui stocke le chemin de l'audio de l'accord
    public string? Audio2 { get; set; }

    // Relations avec d'autres entités
    public List<ProgressionAccords> Progressions { get; set; } = new();

    // Constructeur par défaut avec génération d'ID
    public Accord()
    {
        Id = Guid.NewGuid().ToString();
    }

    // Constructeur à partir d'un DTO
    public Accord(AccordDTO accordDTO)
    {
        Id = !string.IsNullOrEmpty(accordDTO.Id) ? accordDTO.Id : Guid.NewGuid().ToString();
        Nom = accordDTO.Nom ?? throw new ArgumentException("Nom cannot be null");
        Diagram1 = accordDTO.Diagram1 ?? throw new ArgumentException("Diagram1 cannot be null");
        Diagram2 = accordDTO.Diagram2;
        Audio = accordDTO.Audio;
        Audio2 = accordDTO.Audio2;
    }
}