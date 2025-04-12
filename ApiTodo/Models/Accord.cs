public class Accord
{
    public string Id { get; set; }
    public string? Nom { get; set; }
    public string? Diagram1 { get; set; }
    public string? Diagram2 { get; set; }
    public string Audio { get; set; }
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
        this.Nom = accordDTO.Nom;
        this.Diagram1 = accordDTO.Diagram1;
        this.Diagram2 = accordDTO.Diagram2;
        this.Audio = accordDTO.Audio;
        this.Audio2 = accordDTO.Audio2;
    }
}
