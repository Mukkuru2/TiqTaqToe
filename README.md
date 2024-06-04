## GD Blok 3

[comment]: <> (Edit to test VCS change of remote)

### 🛠️ Installatie 
- Installeer Node versie 18 voor jouw OS vanaf deze webpagina: https://nodejs.org/download/release/v18.19.0/ - Vind de versie voor jouw OS. Als je een Mircosoft systeem gebruikt, is het handig om de MSI te downloaden.

![downlooad MSI](../docs/images/readme/nodeJS_msi.png)

- Installeer .NET 6 voor jouw OS vanaf deze webpagina: https://dotnet.microsoft.com/en-us/download/dotnet/6.0 - Installeer de SDK aan de linkerkant, niet de runtime!
- Clone de repository
- Open de repository als map in VSCode, zorg ervoor dat je zowel de `game` als `server` map ziet, anders werkt de aangeleverde configuratie niet correct.
- Installeer de aangerade VSCode extension "C# Dev Kit".
- In VSCode, druk op cmd/ctrl + ` om je terminal te openen. Je kan ook naar Terminal > New Terminal gaan.
- In het terminal, change directory (cd) naar server (zie afbeelding hieronder): <br>
    cd server <br>

- Typ `npm install` om alle code te installeren waar de server van afhankelijk is.

![Run npm install](../docs/images/readme/run npm install.png) <br>

- Als dit gedaan is, typ `npm start` in het terminal om de server op te starten.

![alt text](../docs/images/readme/run npm start.png) <br>

- Druk op F5 (of in het menu Run > Start Debugging) om te controleren of de game compiled en opstart.
- Als het goed is, werkt de game nu en connect deze naar de server.
- Je kan dit controleren door in de server naar de console log te gaan. Hier zou nu iets moeten staan als <br>
	Server listening on port 3000 for environment LOCAL! <br>
	bc3d08a00e69b286b6b1232470acdd56 connected <br>
<br>

Extra:
- Via Terminal > Run Task... kun je de "MonoGame Content Builder" opstarten. Voor meer informatie over deze tool kan je de volgende [link](https://monogame.net/articles/content_pipeline/using_mgcb_editor/) volgen.

### 📚 Opdrachtomschrijving
De opdrachtomschrijving vind je op de [Wiki](../../wikis/home) of op DLO.

### 🛹 Boards
  - Onder de pagina `Issues > Boards `(te vinden via de balk links 👈🏽) vind je verschillende boards:
    - Product backlog met user stories en learning stories;
    - Sprint 1 backlog;
    - Sprint 2 backlog;
    - Sprint 3 backlog.
