public class AccordCreateDTO
{
    public string Nom { get; set; } = string.Empty; // Nom de l'accord
    public string Diagram1 { get; set; } = string.Empty; // Diagramme de l'accord (sous forme de texte ou d'URL vers une image)
    public string? Diagram2 { get; set; }

    public string Audio { get; set; } = string.Empty; // pour la version 1 de l'accord (sous forme de texte ou d'URL vers un fichier audio)
    public string? Audio2 { get; set; } // pour la version 2 de l'accord

    public AccordCreateDTO() { } //contructeur par défaut sert pour la d"sérialisation JSON (quand le client envoie des données à l'API, quand il fait un POST ça doit créer une instance de AccordCreateDTO)
}
