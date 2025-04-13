public class AccordDTO // pour les réponses de l'API
{
    public string Id { get; set; } = string.Empty; // ID de l'accord
    public string? Nom { get; set; }
    public string? Diagram1 { get; set; }
    public string? Diagram2 { get; set; }
    public string Audio { get; set; } = string.Empty; // pour la version 1 de l'accord
    public string? Audio2 { get; set; } // pour la version 2 de l'accord

    public AccordDTO() { }

    public AccordDTO(Accord accord)
    {
        this.Id = accord.Id;
        this.Nom = accord.Nom;
        this.Diagram1 = accord.Diagram1;
        this.Diagram2 = accord.Diagram2;
        this.Audio = accord.Audio;
        this.Audio2 = accord.Audio2;
    }
}
