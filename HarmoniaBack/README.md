# HarmoniaBack

installer dotnet version 8.0 : 8.0.301 [C:\Program Files\dotnet\sdk]
ASP dotnet core 8.0
dotnet tool install --global dotnet-ef --version 9.0.4
dotnet add package Microsoft.EntityFrameworkCore.Design 
dotnet add package Microsoft.EntityFrameworkCore.Sqlite 
dotnet ef database update
dotnet run

# HarmoniaBack - Backend de l'application Harmonia

## Prérequis

Avant de commencer, assurez-vous que vous avez installé les outils suivants via le terminal :

1. **Installer le .NET SDK 8.0** :
    ```bash
    dotnet --version
    # Si la version est inférieure à 8.0, installez-la avec la commande suivante :
    # Suivez les instructions pour installer le SDK .NET 8.0 sur https://dotnet.microsoft.com/download/dotnet/8.0
    ```

2. **Installer dotnet-ef (outil Entity Framework)** :
    ```bash
    dotnet tool install --global dotnet-ef --version 9.0.4
    ```

3. **Installer les packages nécessaires pour Entity Framework** :
    ```bash
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.EntityFrameworkCore.Sqlite
    ```

4. **Vérifier que SQLite est installé** :
    ```bash
    sqlite3 --version
    # Si SQLite n'est pas installé, suivez les instructions pour l'installer sur https://www.sqlite.org/download.html
    ```

## Installation

1. **Cloner le repository** :
    ```bash
    git clone https://github.com/Louanne56/HarmoniaBack.git
    cd harmonia-back
    ```

2. **Restaurer les dépendances** :
    ```bash
    dotnet restore
    ```
    ```

3. **Démarrer l'application** :
    ```bash
    dotnet run
    ```

    L'API sera accessible à l'adresse suivante : `http://localhost:5007`