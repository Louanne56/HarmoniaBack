using Microsoft.AspNetCore.Identity;

public static class SeedData
{
    public static void seed()
    {
        HarmoniaContext context = new HarmoniaContext();

        // 1. Ajouter les accords
        Accord A = new Accord()
        {
            Id = "1d108377-f60a-4179-8e5e-a70b791eefe5",
            Nom = "A",
            Diagram1 = "A.png",
            Diagram2 = "A_v2.png",
            Audio = "A.mp3",
            Audio2 = "A_v2.mp3",
        };

        // 1. Ajouter les accords
        Accord E = new Accord()
        {
            Id = "09d239aa-494d-4286-a891-8c787325a371",
            Nom = "E",
            Diagram1 = "E.png",
            Diagram2 = "E_v2.png",
            Audio = "E.mp3",
            Audio2 = "E_v2.mp3",
        };

        Accord Dm7 = new Accord()
        {
            Id = "0fe1b5a8-2efe-4341-ba4c-a2615973970a",
            Nom = "Dm7",
            Diagram1 = "Dm7.png",
            Diagram2 = "",
            Audio = "Dm7.mp3",
            Audio2 = "",
        };


        Accord Am = new Accord()
        {
            Id = "1e8f5f31-915c-44d5-9285-7b36cbb2ab3b",
            Nom = "Am",
            Diagram1 = "Am.png",
            Diagram2 = "Am_v2.png",
            Audio = "Am.mp3",
            Audio2 = "Am_v2.mp3",
        };

        Accord C = new Accord()
        {
            Id = "205dceb5-e4d7-4fbb-8689-f17c6c7e79a5",
            Nom = "C",
            Diagram1 = "C.png",
            Diagram2 = "C_v2.png",
            Audio = "C.mp3",
            Audio2 = "C_v2.mp3",
        };
        Accord Cm = new Accord()
        {
            Id = "247577a0-7335-4b93-b1bf-a8b2fb4933be",
            Nom = "Cm",
            Diagram1 = "Cm.png",
            Diagram2 = "",
            Audio = "Cm.mp3",
            Audio2 = "",
        };

        Accord Bm = new Accord()
        {
            Id = "2b43d694-7c88-46c5-9410-9a906262fc40",
            Nom = "Bm",
            Diagram1 = "Bm.png",
            Diagram2 = "Bm_v2.png",
            Audio = "Bm.mp3",
            Audio2 = "Bm_v2.mp3",
        };

        Accord G7 = new Accord()
        {
            Id = "2da86f0c-fd7e-4189-a8b4-2ca8fbae4358",
            Nom = "G7",
            Diagram1 = "G7.png",
            Diagram2 = "",
            Audio = "G7.mp3",
            Audio2 = "",
        };

        Accord G = new Accord()
        {
            Id = "30211a77-28b1-47cd-bc15-2c0c60d54d29",
            Nom = "G",
            Diagram1 = "G.png",
            Diagram2 = "G_v2.png",
            Audio = "G.mp3",
            Audio2 = "G_v2.mp3",
        };

        Accord Em7 = new Accord()
        {
            Id = "43ca59ad-b4e6-4341-b81f-6926698f5137",
            Nom = "Em7",
            Diagram1 = "Em7.png",
            Diagram2 = "",
            Audio = "Em7.mp3",
            Audio2 = "",
        };

        Accord Fsharpdim = new Accord()
        {
            Id = "52b69e2a-4e09-4656-9e81-7b84cec8dde2",
            Nom = "Fsharpdim",
            Diagram1 = "Fsharpdim.png",
            Diagram2 = "",
            Audio = "Fsharpdim.mp3",
            Audio2 = "",
        };

        Accord Bm7 = new Accord()
        {
            Id = "7d587d06-ada8-4f1c-8181-35331e2f063b",
            Nom = "Bm7",
            Diagram1 = "Bm7.png",
            Diagram2 = "",
            Audio = "Bm7.mp3",
            Audio2 = "",
        };

        Accord C7 = new Accord()
        {
            Id = "9a6b39b6-6f66-44ca-bc77-041a3410880f",
            Nom = "C7",
            Diagram1 = "C7.png",
            Diagram2 = "",
            Audio = "C7.mp3",
            Audio2 = "",
        };

        Accord Fm = new Accord()
        {
            Id = "9d6630e3-3d57-4af7-a3cd-426fe1ad442d",
            Nom = "Fm",
            Diagram1 = "Fm.png",
            Diagram2 = "",
            Audio = "Fm.mp3",
            Audio2 = "",
        };

        Accord D = new Accord()
        {
            Id = "c0f3b1a2-4d5e-4b8c-8f1d-9a6e2f3b5c1g",
            Nom = "D",
            Diagram1 = "D.png",
            Diagram2 = "D_v2.png",
            Audio = "D.mp3",
            Audio2 = "D_v2.mp3",
        };

        Accord Am7 = new Accord()
        {
            Id = "c2d47654-578a-4cae-be67-f0e2fb682f94",
            Nom = "Am7",
            Diagram1 = "Am7.png",
            Diagram2 = "",
            Audio = "Am7.mp3",
            Audio2 = "",
        };

        Accord Em = new Accord()
        {
            Id = "d49db938-8705-40af-a5ab-79fe6179f333",
            Nom = "Em",
            Diagram1 = "Em.png",
            Diagram2 = "Em_v2.png",
            Audio = "Em.mp3",
            Audio2 = "Em_v2.mp3",
        };

        Accord Gm = new Accord()
        {
            Id = "d986b211-3f28-4466-8aac-be0b5d7ee002",
            Nom = "Gm",
            Diagram1 = "Gm.png",
            Diagram2 = "",
            Audio = "Gm.mp3",
            Audio2 = "",
        };

        Accord D7 = new Accord()
        {
            Id = "df7c4e46-a05a-433b-b3fe-2c362b22ef24",
            Nom = "D7",
            Diagram1 = "D7.png",
            Diagram2 = "",
            Audio = "D7.mp3",
            Audio2 = "",
        };

        Accord F = new Accord()
        {
            Id = "e4692b24-0d20-4cb7-afea-c83059d6c4b8",
            Nom = "F",
            Diagram1 = "F.png",
            Diagram2 = "F_v2.png",
            Audio = "F.mp3",
            Audio2 = "F_v2.mp3",
        };

        Accord Bdim = new Accord()
        {
            Id = "ed62a31c-787d-4c0c-b6c8-5efa6c2723a5",
            Nom = "Bdim",
            Diagram1 = "Bdim.png",
            Diagram2 = "",
            Audio = "Bdim.mp3",
            Audio2 = "",
        };

        Accord Dm = new Accord()
        {
            Id = "f3a2b5d4-0c7e-4b8c-8f1d-9a6e2f3b5c1f",
            Nom = "Dm",
            Diagram1 = "Dm.png",
            Diagram2 = "",
            Audio = "Dm.mp3",
            Audio2 = "",
        };

        context.Accords.AddRange(
            A,
            E,
            Dm7,
            Am,
            C,
            Cm,
            Bm,
            G7,
            G,
            Em7,
            Fsharpdim,
            Bm7,
            C7,
            Fm,
            D,
            Am7,
            Em,
            Gm,
            D7,
            F,
            Bdim,
            Dm
        );

        // 2. Ajouter les progressions d'accords
        {
            ProgressionAccords p1 = new ProgressionAccords
            {
                Id = "b7886b8c-6f3c-4bb6-894f-99fbf8e38761",
                Nom = "Pop en mi mineur",
                Tonalite = Tonalite.E,
                Mode = Mode.Mineur,
                Style = Style.Pop,
                Accords = new List<Accord> { Em, C, G, D },
            };
            ProgressionAccords p2 = new ProgressionAccords
            {
                Id = "b7886b8c-6f3c-4bb6-894f-99fbf8e38762",
                Nom = "Pop en mi mineur",
                Tonalite = Tonalite.E,
                Mode = Mode.Mineur,
                Style = Style.Pop,
                Accords = new List<Accord> { C, Am, Em, Bm },
            };

            ProgressionAccords p3 = new ProgressionAccords
            {
                Id = "1dce1028-b8ec-43e3-8b9d-d926021234da",
                Nom = "Rock en mi mineur",
                Tonalite = Tonalite.E,
                Mode = Mode.Majeur,
                Style = Style.Rock,
                Accords = new List<Accord> { Em, Am, Bm },
            };

            ProgressionAccords p4 = new ProgressionAccords
            {
                Id = "925fe80f-2ded-4701-bec8-0788203d3925",
                Nom = "Jazz en mi mineur",
                Tonalite = Tonalite.E,
                Mode = Mode.Mineur,
                Style = Style.Jazz,
                Accords = new List<Accord> { Fsharpdim, Bm, Em },
            };

            ProgressionAccords p5 = new ProgressionAccords
            {
                Id = "77838781-fa74-473f-99ad-2bda4512dfdc",
                Nom = "Rock en do majeur",
                Tonalite = Tonalite.C,
                Mode = Mode.Majeur,
                Style = Style.Rock,
                Accords = new List<Accord> { C, F, G },
            };

            ProgressionAccords p6 = new ProgressionAccords
            {
                Id = "e058ff02-faa0-4899-9a0d-95a296618abf",
                Nom = "Pop en do majeur",
                Tonalite = Tonalite.C,
                Mode = Mode.Majeur,
                Style = Style.Blues,
                Accords = new List<Accord> { C, G, Am, G },
            };

            ProgressionAccords p7 = new ProgressionAccords
            {
                Id = "f3a2b5d4-0c7e-4b8c-8f1d-9a6e2f3b5c1e",
                Nom = "Jazz en do majeur",
                Tonalite = Tonalite.C,
                Mode = Mode.Majeur,
                Style = Style.Jazz,
                Accords = new List<Accord> { Dm7, G7, C7 },
            };

            ProgressionAccords p8 = new ProgressionAccords
            {
                Id = "c3a2b5d4-0c7e-4b8c-8f1d-9a6e2f3b5c1e",
                Nom = "Pop en do majeur",
                Tonalite = Tonalite.C,
                Mode = Mode.Majeur,
                Style = Style.Jazz,
                Accords = new List<Accord> { Am, F, C, G },
            };

            ProgressionAccords p9 = new ProgressionAccords
            {
                Id = "32fd18db-4e8d-4c12-924b-79d674fb8e6b",
                Nom = "Pop en la mineur",
                Tonalite = Tonalite.A,
                Mode = Mode.Mineur,
                Style = Style.Pop,
                Accords = new List<Accord> { Am, Em, F, Dm },
            };

            ProgressionAccords p10 = new ProgressionAccords
            {
                Id = "9fd7f42f-bbaf-48dc-8a4a-2e1b798a9e55",
                Nom = "Pop en la mineur",
                Tonalite = Tonalite.A,
                Mode = Mode.Mineur,
                Style = Style.Pop,
                Accords = new List<Accord> { F, Dm, Am, Em },
            };

            ProgressionAccords p11 = new ProgressionAccords
            {
                Id = "6e8d1062-1d53-4077-8946-4b6b01a897a7",
                Nom = "Rock en la mineur",
                Tonalite = Tonalite.A,
                Mode = Mode.Mineur,
                Style = Style.Rock,
                Accords = new List<Accord> { Am, Dm, Em },
            };

            ProgressionAccords p12 = new ProgressionAccords
            {
                Id = "d68912fa-fd1f-4e1d-9fd1-8e1e62e8e799",
                Nom = "Jazz en la mineur",
                Tonalite = Tonalite.A,
                Mode = Mode.Mineur,
                Style = Style.Jazz,
                Accords = new List<Accord> { Bdim, Em, Am },
            };

            ProgressionAccords p13 = new ProgressionAccords
            {
                Id = "e2a13c7d-c4b3-4de7-b9ef-91de3c22dc13",
                Nom = "Pop en sol majeur",
                Tonalite = Tonalite.G,
                Mode = Mode.Majeur,
                Style = Style.Pop,
                Accords = new List<Accord> { G, D, Em, C },
            };

            ProgressionAccords p14 = new ProgressionAccords
            {
                Id = "457967e4-bfcb-4eec-90ae-192a2d08df48",
                Nom = "Pop en sol majeur",
                Tonalite = Tonalite.G,
                Mode = Mode.Majeur,
                Style = Style.Pop,
                Accords = new List<Accord> { Em, C, G, D },
            };

            ProgressionAccords p15 = new ProgressionAccords
            {
                Id = "8c0b229f-f60f-4e7f-a4c3-fca1ac8588c4",
                Nom = "Rock en sol majeur",
                Tonalite = Tonalite.G,
                Mode = Mode.Majeur,
                Style = Style.Rock,
                Accords = new List<Accord> { G, C, D },
            };
            context.ProgressionAccords.AddRange(
                p1,
                p2,
                p3,
                p4,
                p5,
                p6,
                p7,
                p8,
                p9,
                p10,
                p11,
                p12,
                p13,
                p14,
                p15
            );
        }
        ;

        // 4. Ajouter les utilisateurs
        var hasher = new PasswordHasher<Utilisateur>();

        var user1 = new Utilisateur
        {
            Id = "031046ec-54b6-4c00-a714-c3051037a0cd",
            UserName = "Louanne",
            Email = "monnierlouanne56@gmail.com",
            NormalizedUserName = "LOUANNE",
            NormalizedEmail = "MONNIERLOUANNE56@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        user1.PasswordHash = hasher.HashPassword(user1, "MotDePasseLouanne");

        var user2 = new Utilisateur
        {
            Id = "f0d3d6c3-9a3f-4ab-8c3d-0f1a2e4f5b6d",
            UserName = "Victor",
            Email = "victor@gmail.com",
            NormalizedUserName = "VICTOR",
            NormalizedEmail = "VICTOR@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        user2.PasswordHash = hasher.HashPassword(user2, "MotDePasseVictor");

        var user3 = new Utilisateur
        {
            Id = "5cb8b974-9062-4ea4-8fe0-4febf3da5c6d",
            UserName = "pipou",
            Email = "pipou@mail.com",
            NormalizedUserName = "PIPOU",
            NormalizedEmail = "PIPOU@MAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };
        user3.PasswordHash = hasher.HashPassword(user3, "MotDePassePipou");
        context.Users.AddRange(user1, user2, user3);

        // 5. Ajouter les suites favorites
        // 5. Ajouter les suites favorites
        var fav1 = new SuitesFavorites
        {
            Id = "291a42f0-75e6-4c77-9ba1-2ba894f4b197",
            UserId = "031046ec-54b6-4c00-a714-c3051037a0cd",
            ProgressionAccordsId = "b7886b8c-6f3c-4bb6-894f-99fbf8e38761",
        };

        var fav2 = new SuitesFavorites
        {
            Id = "86d1a39f-db34-41a1-b770-991ad9a2f406",
            UserId = "031046ec-54b6-4c00-a714-c3051037a0cd",
            ProgressionAccordsId = "77838781-fa74-473f-99ad-2bda4512dfdc",
        };

        context.SuitesFavorites.AddRange(fav1, fav2);

        context.SaveChanges();
    }
}
