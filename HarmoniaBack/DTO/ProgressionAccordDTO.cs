using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

public class ProgressionAccordDTO
{
    public string Id { get; set; }
    public string Nom { get; set; }

    // Valeurs enum originales
    public Tonalite Tonalite { get; set; }
    public Mode Mode { get; set; }
    public Style Style { get; set; }

    // Représentations textuelles
    public string TonaliteNom { get; set; }
    public string ModeNom { get; set; }
    public string StyleNom { get; set; }

    public List<string> Accords { get; set; }

    public ProgressionAccordDTO(ProgressionAccords progression)
    {
        Id = progression.Id;
        Nom = progression.Nom;

        // Valeurs enum
        Tonalite = progression.Tonalite;
        Mode = progression.Mode;
        Style = progression.Style;

        // Représentations textuelles
        TonaliteNom = GetDisplayName(progression.Tonalite); // Aucune vérification de null nécessaire
        ModeNom = progression.Mode.ToString(); // Mode est non-nullable, pas besoin de ?.
        StyleNom = progression.Style.ToString(); // Style est non-nullable, pas besoin de ?.

        Accords = progression.Accords?.Select(a => a.Nom).ToList() ?? new List<string>();
    }

    private string GetDisplayName(Enum enumValue)
    {
        var fieldInfo = enumValue.GetType()?.GetField(enumValue.ToString());
        if (fieldInfo == null)
            return enumValue.ToString();

        var displayAttribute =
            fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault()
            as DisplayAttribute;

        return displayAttribute?.Name ?? enumValue.ToString();
    }
}
