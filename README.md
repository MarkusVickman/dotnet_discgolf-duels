# Dotnet - Projekt - Discgolf Duels
Repot innehåller en discgolf-applikation. Applikationen är skapad med dotnet core MVC samt entity framework core och Identity framework. För att lagra data ansluter lösningen till en Sqlite-databas.
I applikationen kan användare skapa konton. Om användaren inte är inloggad syns endast tävlingar samt alla spelade rundor. När användaren loggar in för första gången kan den skapa en publikprofil för att komma åt följande funktioner:
* Spela och hantera rundor
* Lägga till och hantera banor
* Skapa och adminitrera tävling
* Registrera på andras tävlingar
* Spela tävling

## Uppgiftens grundkrav
* Skapa ett databasdrivet system baserat Entity Framework Core, där det via ett webbgränssnitt går att hämta, ändra och addera data i underliggande databas. Detta kallas vanligtvis för CRUD-gränssnitt (utifrån Create, Read, Update och Delete).
* Applikationer ska innehålla en funktion för inloggning (säkerhetsnivån på din lösning är inte viktig).
* Webbgränssnittet ni utvecklar ska på ett eller annat sätt inkludera att presentera data från databasen för publika (dvs ej inloggade) besökare.
* Resulterande HTML-kod (den som automatgenererats och skickas till webbläsare) analyseras enligt W3C:s rekommendationer och validera korrekt (både HTML- och CSS-kod).
* Webbplatsen ska följa generella riktlinjer vad det gäller tillgänglighet och användbarhet.

## Uppgiftkrav för överbetyg
* Extra funktionallitet som är något nytt som inte behandlas i kursens tre ordinarie laborationer och att det tillför värde till den färdiga lösningen.

** För överbetyg har jag flera databas-tabeller som har relationer till varandra. Att hämta data i applikationens-controllers för aktuell tabell samt relaterade tabeller samtidigt är något vi inte gått igenom i kursen. **
** Min lösning är också publicerad. **
** Ska implementera MariaDB!!!!! **
** Ska ordna mailserver!!!!! **

## Min Lösning
För att hantera mycket relaterad data används en SQL-databas. Modeller och tabeller skapades för Competition, Playing, Play, Registration, PublicUser.

### Tabeller/Modeller

```bash
public class Competition
    {
        public int CompetitionId { get; set; }
        public required String CompetitionName { get; set; }
        public required int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course? Course { get; set; }

        public required DateTime CompetitionDate { get; set; }

        public required int MaxPlayerCount { get; set; }

        public required int PublicUserId { get; set; }

        [ForeignKey("PublicUserId")]
        public PublicUser? PublicUser { get; set; }

        public ICollection<Registration>? Registrations { get; set; }
```

```bash
CREATE TABLE "Songs" (
	"SongId"	INTEGER NOT NULL,
	"Title"	TEXT NOT NULL,
	"Length"	INTEGER NOT NULL,
	"Category"	TEXT,
	"ArtistId"	INTEGER NOT NULL,
	"AlbumId"	INTEGER,
	CONSTRAINT "PK_Songs" PRIMARY KEY("SongId" AUTOINCREMENT),
	CONSTRAINT "FK_Songs_Albums_AlbumId" FOREIGN KEY("AlbumId") REFERENCES "Albums"("AlbumId"),
	CONSTRAINT "FK_Songs_Artists_ArtistId" FOREIGN KEY("ArtistId") REFERENCES "Artists"("ArtistId") ON DELETE CASCADE
)
```

```bash
CREATE TABLE "Albums" (
    "AlbumId" INTEGER NOT NULL CONSTRAINT "PK_Albums" PRIMARY KEY AUTOINCREMENT,
    "AlbumName" TEXT NOT NULL,
    "ReleaseYear" TEXT NOT NULL,
    "ArtistId" INTEGER NOT NULL,
    CONSTRAINT "FK_Albums_Artists_ArtistId" FOREIGN KEY ("ArtistId") REFERENCES "Artists" ("ArtistId") ON DELETE CASCADE
)
```

### Routes

 


### Skapad av MARKUS VICKMAN (MAVI2302) 
