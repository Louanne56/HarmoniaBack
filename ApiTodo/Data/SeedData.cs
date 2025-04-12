using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

public static class SeedData
{
    public static void seed()
    {
        HarmoniaContext context = new HarmoniaContext();

        // 1. Ajouter les accords
        if (context.Accords.Any())
        {
            Console.WriteLine("La base de données contient déjà des accords. Initialisation ignorée.");
            return; // La base de données a déjà été initialisée
        }

        // Créer et ajouter les accords
        var accords = new List<Accord>
        {
            new Accord
            {
                Id = "1d108377-f60a-4179-8e5e-a70b791eefe5",
                Nom = "A",
                Diagram1 = "A.png",
                Diagram2 = "A_v2.png",
                Audio = "A.mp3",
                Audio2 = "A_v2.mp3"
            },
            new Accord
            {
                Id = "1e8f5f31-915c-44d5-9285-7b36cbb2ab3b",
                Nom = "Am",
                Diagram1 = "Am.png",
                Diagram2 = "Am_v2.png",
                Audio = "Am.mp3",
                Audio2 = "Am_v2.mp3"
            },
            new Accord
            {
                Id = "c2d47654-578a-4cae-be67-f0e2fb682f94",
                Nom = "Am7",
                Diagram1 = "Am7.png",
                Diagram2 = "",
                Audio = "Am7.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "ed62a31c-787d-4c0c-b6c8-5efa6c2723a5",
                Nom = "Bdim",
                Diagram1 = "Bdim.png",
                Diagram2 = "",
                Audio = "Bdim.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "2b43d694-7c88-46c5-9410-9a906262fc40",
                Nom = "Bm",
                Diagram1 = "Bm.png",
                Diagram2 = "Bm_v2.png",
                Audio = "Bm.mp3",
                Audio2 = "Bm_v2.mp3"
            },
            new Accord
            {
                Id = "7d587d06-ada8-4f1c-8181-35331e2f063b",
                Nom = "Bm7",
                Diagram1 = "Bm7.png",
                Diagram2 = "",
                Audio = "Bm7.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "205dceb5-e4d7-4fbb-8689-f17c6c7e79a5",
                Nom = "C",
                Diagram1 = "C.png",
                Diagram2 = "C_v2.png",
                Audio = "C.mp3",
                Audio2 = "C_v2.mp3"
            },
            new Accord
            {
                Id = "9a6b39b6-6f66-44ca-bc77-041a3410880f",
                Nom = "C7",
                Diagram1 = "C7.png",
                Diagram2 = "",
                Audio = "C7.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "247577a0-7335-4b93-b1bf-a8b2fb4933be",
                Nom = "Cm",
                Diagram1 = "Cm.png",
                Diagram2 = "",
                Audio = "Cm.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "df7c4e46-a05a-433b-b3fe-2c362b22ef24",
                Nom = "D7",
                Diagram1 = "D7.png",
                Diagram2 = "",
                Audio = "D7.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "0fe1b5a8-2efe-4341-ba4c-a2615973970a",
                Nom = "Dm7",
                Diagram1 = "Dm7.png",
                Diagram2 = "",
                Audio = "Dm7.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "09d239aa-494d-4286-a891-8c787325a371",
                Nom = "E",
                Diagram1 = "E.png",
                Diagram2 = "E_v2.png",
                Audio = "E.mp3",
                Audio2 = "E_v2.mp3"
            },
            new Accord
            {
                Id = "d49db938-8705-40af-a5ab-79fe6179f333",
                Nom = "Em",
                Diagram1 = "Em.png",
                Diagram2 = "Em_v2.png",
                Audio = "Em.mp3",
                Audio2 = "Em_v2.mp3"
            },
            new Accord
            {
                Id = "43ca59ad-b4e6-4341-b81f-6926698f5137",
                Nom = "Em7",
                Diagram1 = "Em7.png",
                Diagram2 = "",
                Audio = "Em7.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "e4692b24-0d20-4cb7-afea-c83059d6c4b8",
                Nom = "F",
                Diagram1 = "F.png",
                Diagram2 = "F_v2.png",
                Audio = "F.mp3",
                Audio2 = "F_v2.mp3"
            },
            new Accord
            {
                Id = "52b69e2a-4e09-4656-9e81-7b84cec8dde2",
                Nom = "Fsharpdim",
                Diagram1 = "Fsharpdim.png",
                Diagram2 = "",
                Audio = "Fsharpdim.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "9d6630e3-3d57-4af7-a3cd-426fe1ad442d",
                Nom = "Fm",
                Diagram1 = "Fm.png",
                Diagram2 = "",
                Audio = "Fm.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "30211a77-28b1-47cd-bc15-2c0c60d54d29",
                Nom = "G",
                Diagram1 = "G.png",
                Diagram2 = "G_v2.png",
                Audio = "G.mp3",
                Audio2 = "G_v2.mp3"
            },
            new Accord
            {
                Id = "2da86f0c-fd7e-4189-a8b4-2ca8fbae4358",
                Nom = "G7",
                Diagram1 = "G7.png",
                Diagram2 = "",
                Audio = "G7.mp3",
                Audio2 = ""
            },
            new Accord
            {
                Id = "d986b211-3f28-4466-8aac-be0b5d7ee002",
                Nom = "Gm",
                Diagram1 = "Gm.png",
                Diagram2 = "",
                Audio = "Gm.mp3",
                Audio2 = ""
            }
        };

        context.Accords.AddRange(accords);
        context.SaveChanges();
        Console.WriteLine("Accords ajoutés avec succès");

        // 2. Créer un dictionnaire d'accords pour un accès facile
        var accordsDict = accords.ToDictionary(a => a.Nom, a => a);

        // 3. Ajouter les progressions d'accords
        var progressions = new List<ProgressionAccords>
        {
            new ProgressionAccords
            {
                Id = "b7886b8c-6f3c-4bb6-894f-99fbf8e38761",
                Nom = "Pop en mi mineur",
                Tonalite = Tonalite.E,
                Mode = Mode.Mineur,
                Style = Style.Pop,
                Accords = new List<Accord>
                {
                    accordsDict["A"],
                    accordsDict["C"],
                    accordsDict["Bm"],
                    accordsDict["Em"]
                }
            },
            new ProgressionAccords
            {
                Id = "77838781-fa74-473f-99ad-2bda4512dfdc",
                Nom = "Rock en do majeur",
                Tonalite = Tonalite.C,
                Mode = Mode.Majeur,
                Style = Style.Rock,
                Accords = new List<Accord>
                {
                    accordsDict["C"],
                    accordsDict["G"],
                    accordsDict["F"]
                }
            }
        };

        context.ProgressionAccords.AddRange(progressions);
        context.SaveChanges();
        Console.WriteLine("Progressions d'accords ajoutées avec succès");

        // 4. Ajouter les utilisateurs
        var hasher = new PasswordHasher<Utilisateur>();
        var users = new List<Utilisateur>
        {
            new Utilisateur
            {
                Id = "031046ec-54b6-4c00-a714-c3051037a0cd",
                Pseudo = "Louanne",
                Email = "monnierlouanne56@gmail.com",
                UserName = "Louanne",
                NormalizedUserName = "LOUANNE",
                NormalizedEmail = "MONNIERLOUANNE56@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new Utilisateur
            {
                Id = "1598aabf-875a-4239-87e7-f9bc1e79daf9",
                Pseudo = "loulou",
                Email = "lmonnier005@ensc.fr",
                UserName = "loulou",
                NormalizedUserName = "LOULOU",
                NormalizedEmail = "LMONNIER005@ENSC.FR",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new Utilisateur
            {
                Id = "9106e68e-4978-437e-98c5-98b18ab8ca9b",
                Pseudo = "patrice",
                Email = "monnierpatrice@orange.fr",
                UserName = "patrice",
                NormalizedUserName = "PATRICE",
                NormalizedEmail = "MONNIERPATRICE@ORANGE.FR",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            },
            new Utilisateur
            {
                Id = "5cb8b974-9062-4ea4-8fe0-4febf3da5c6d",
                Pseudo = "pipou",
                Email = "pipou@mail.com",
                UserName = "pipou",
                NormalizedUserName = "PIPOU",
                NormalizedEmail = "PIPOU@MAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            }
        };

        // Définir un mot de passe par défaut pour tous les utilisateurs
        string defaultPassword = "Password123!";
        foreach (var user in users)
        {
            user.PasswordHash = hasher.HashPassword(user, defaultPassword);
        }

        context.Users.AddRange(users);
        context.SaveChanges();
        Console.WriteLine("Utilisateurs ajoutés avec succès");

        // 5. Ajouter les suites favorites
        var suitesFavorites = new List<SuitesFavorites>
        {
            new SuitesFavorites
            {
                Id = "291a42f0-75e6-4c77-9ba1-2ba894f4b197",
                UserId = "031046ec-54b6-4c00-a714-c3051037a0cd",
                ProgressionAccordsId = "b7886b8c-6f3c-4bb6-894f-99fbf8e38761"
            },
            new SuitesFavorites
            {
                Id = "86d1a39f-db34-41a1-b770-991ad9a2f406",
                UserId = "031046ec-54b6-4c00-a714-c3051037a0cd",
                ProgressionAccordsId = "77838781-fa74-473f-99ad-2bda4512dfdc"
            }
        };

        context.SuitesFavorites.AddRange(suitesFavorites);
        context.SaveChanges();
        Console.WriteLine("Suites favorites ajoutées avec succès");
        
        Console.WriteLine("Initialisation de la base de données terminée avec succès");
    }
}