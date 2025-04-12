/*public static class SeedData
{
    public static void seed()
    {
        HarmoniaContext context = new HarmoniaContext();

        // 1. Ajouter les accords
        Accord A = new Accord
        {
            Nom = "A",
            Position1 = "X02220",
            Position2 = "577655",
        };
        Accord B = new Accord
        {
            Nom = "B",
            Position1 = "X24442",
            Position2 = "799877",
        };
        Accord C = new Accord
        {
            Nom = "C",
            Position1 = "X32010",
            Position2 = "8 10 10 9 8 8",
        };
        Accord D = new Accord
        {
            Nom = "D",
            Position1 = "XX0232",
            Position2 = "X57775",
        };
        Accord E = new Accord
        {
            Nom = "E",
            Position1 = "022100",
            Position2 = "X79997",
        };

        context.Accords.AddRange(A, B, C, D, E);
        context.SaveChanges();

        ProgressionAccords p1 = new ProgressionAccords
        {
            Nom = "Blues en A",
            Mode = Mode.Majeur,
            Style = Style.Blues,
            Tonalite = Tonalite.A,
            Accords = new List<Accord> { A, D, E },
        };
        ProgressionAccords p2 = new ProgressionAccords
        {
            Nom = "Rock en C",
            Mode = Mode.Majeur,
            Style = Style.Rock,
            Tonalite = Tonalite.C,
            Accords = new List<Accord> { A, B, C },
        };

        context.ProgressionAccords.AddRange(p1, p2);
        context.SaveChanges();

        Utilisateur user1 = new Utilisateur { Pseudo = "Alice" };
        context.Utilisateurs.Add(user1);
        context.SaveChanges();

        SuitesFavorites fav1 = new SuitesFavorites { ProgressionAccords = p1, Id = user1.Id };

        context.SuitesFavorites.Add(fav1);
        context.SaveChanges(); 
    }
}*/
