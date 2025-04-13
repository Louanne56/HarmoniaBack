public class Accord
{
    public string Id { get; set; }
    public required string Nom { get; set; }
    public required string Diagram1 { get; set; }
    public string? Diagram2 { get; set; }
    public required string Audio { get; set; }
    public string? Audio2 { get; set; }
    public List<ProgressionAccords> Progressions { get; set; } = new();

    public Accord()
    {
        Id = Guid.NewGuid().ToString(); // Générer un ID unique dans le constructeur par défaut
    }

    public Accord(AccordDTO accordDTO)
    {
        // Si l'ID du DTO est fourni, utilisez-le, sinon générez-en un nouveau
        this.Id = !string.IsNullOrEmpty(accordDTO.Id) ? accordDTO.Id : Guid.NewGuid().ToString();
        this.Nom = accordDTO.Nom ?? throw new ArgumentException("Nom cannot be null");
        this.Diagram1 =
            accordDTO.Diagram1 ?? throw new ArgumentException("Diagram1 cannot be null");
        this.Diagram2 = accordDTO.Diagram2;
        this.Audio = accordDTO.Audio;
        this.Audio2 = accordDTO.Audio2;
    }
}
