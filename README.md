# HarmoniaBack
 # HarmoniaBack
 
 ## Installation des prérequis
 
 ### 1. Installation de .NET 8.0
 
 Téléchargez et installez le SDK .NET 8.0 depuis le site officiel de Microsoft :  
 👉 [https://dotnet.microsoft.com/download/dotnet/8.0](https://dotnet.microsoft.com/download/dotnet/8.0)  
 *Note : L'installation du SDK .NET 8.0 inclut ASP.NET Core 8.0*
 
 Vérifiez l'installation avec la commande :
 ```bash
 dotnet --version
 ```
 
 ### 2. Installer les outils et packages nécessaires
 
 ```bash
 # Installer Entity Framework CLI
 dotnet tool install --global dotnet-ef --version 9.0.4
 
 # Ajouter les packages nécessaires
 dotnet add package Microsoft.EntityFrameworkCore.Design
 dotnet add package Microsoft.EntityFrameworkCore.Sqlite
 
 # Lancer le backend
 dotnet run
 ```
 
 ---
 
 ## Installation et lancement du Backend
 
 ```bash
 # Clonez le repository backend
 git clone [https://github.com/Louanne56/HarmoniaBack.git]
 cd HarmoniaBack
 ```
 
 Installez les packages Entity Framework nécessaires (si ce n'est pas déjà fait) :
 ```bash
 dotnet add package Microsoft.EntityFrameworkCore.Design
 dotnet add package Microsoft.EntityFrameworkCore.Sqlite
 ```
 
 Lancez le backend :
 ```bash
 dotnet run
 ```
 
 Le serveur démarrera et initialisera automatiquement la base de données avec les données de départ *(seed data)*.
 
 ---
 

 
 
 
 

    ```
    Cela ouvrira une page dans votre navigateur avec un **QR code**.
 4. Scannez ce QR code avec l'application **Expo Go** depuis votre smartphone.
 5. L'application se chargera automatiquement sur votre appareil.
 
 > 💡 Si le QR code ne fonctionne pas ou si vous êtes en environnement restreint, vous pouvez aussi utiliser l'option "Tunnel" dans la page Expo pour une meilleure compatibilité.
