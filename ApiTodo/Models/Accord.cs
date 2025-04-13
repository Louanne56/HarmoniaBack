public class Accord
{
    // Propriétés de base d'un accord musical
    public string Id { get; set; } // Identifiant unique de l'accord
    public required string Nom { get; set; } // Nom de l'accord (ex: "C Majeur")
    public required string Diagram1 { get; set; } // Premier diagramme (obligatoire)
    public string? Diagram2 { get; set; } // Deuxième diagramme optionnel
    public required string Audio { get; set; } // Fichier audio principal
    public string? Audio2 { get; set; } // Fichier audio secondaire optionnel

    // Liste des progressions qui utilisent cet accord
    public List<ProgressionAccords> Progressions { get; set; } = new();

    // Constructeur par défaut avec génération d'ID
    public Accord()
    {
        Id = Guid.NewGuid().ToString();
    }

    // Constructeur à partir d'un DTO (Data Transfer Object)
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
