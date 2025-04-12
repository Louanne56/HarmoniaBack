public class AccordCreateDTO
{
    public string? Nom { get; set; }
    public string? Diagram1 { get; set; }
    public string? Diagram2 { get; set; }

    public string Audio { get; set; }
    public string? Audio2 { get; set; } // pour la version 2 de l'accord

    public AccordCreateDTO() { } //contructeur par défaut sert pour la d"sérialisation JSON (quand le client envoie des données à l'API, quand il fait un POST ça doit créer une instance de AccordCreateDTO)
}
