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
    public Tonalite? Tonalite { get; set; }
    public Mode? Mode { get; set; }
    public Style? Style { get; set; }

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
        TonaliteNom = progression.Tonalite.HasValue
            ? GetDisplayName(progression.Tonalite.Value)
            : null;

        ModeNom = progression.Mode?.ToString();
        StyleNom = progression.Style?.ToString();

        Accords = progression.Accords?.Select(a => a.Nom).ToList() ?? new List<string>();
    }

    private string GetDisplayName(Enum enumValue)
    {
        var displayAttribute =
            enumValue
                .GetType()
                .GetField(enumValue.ToString())
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault() as DisplayAttribute;

        return displayAttribute?.Name ?? enumValue.ToString();
    }
}
