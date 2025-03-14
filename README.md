# Dotnet - Projekt - Discgolf Duels
Repot innehåller en discgolf-applikation. Applikationen är skapad med dotnet core MVC samt entity framework core och Identity framework. För att lagra data ansluter lösningen till en MariaDb-databas. Applikationen är kopplad till en extern email-server och stöder funktioner som bekräftelse-mail och återställa lösenord.
I applikationen kan användare skapa konton. Om användaren inte är inloggad syns endast tävlingar samt alla spelade rundor. När användaren loggar in för första gången kan den skapa en publikprofil för att komma åt följande funktioner:
* Spela och hantera rundor
* Lägga till och hantera banor
* Skapa och adminitrera tävling
* Registrera på andras tävlingar
* Spela tävling

Besök webbplatsen [Disc golf duels](https://discgolf-duels-b3ake0adgjf3btd2.swedencentral-01.azurewebsites.net/).

## Uppgiftens grundkrav
* Skapa ett databasdrivet system baserat Entity Framework Core, där det via ett webbgränssnitt går att hämta, ändra och addera data i underliggande databas. Detta kallas vanligtvis för CRUD-gränssnitt (utifrån Create, Read, Update och Delete).
* Applikationer ska innehålla en funktion för inloggning (säkerhetsnivån på din lösning är inte viktig).
* Webbgränssnittet ni utvecklar ska på ett eller annat sätt inkludera att presentera data från databasen för publika (dvs ej inloggade) besökare.
* Resulterande HTML-kod (den som automatgenererats och skickas till webbläsare) analyseras enligt W3C:s rekommendationer och validera korrekt (både HTML- och CSS-kod).
* Webbplatsen ska följa generella riktlinjer vad det gäller tillgänglighet och användbarhet.

## Uppgiftkrav för överbetyg
* Extra funktionallitet som är något nytt som inte behandlas i kursens tre ordinarie laborationer och att det tillför värde till den färdiga lösningen.

## Min Lösning
För att hantera mycket relaterad data används en SQL-databas. Modeller och tabeller skapades för Competition, Playing, Play, Registration, PublicUser.

### För överbetyg 

* Har flera databas-tabeller som har relationer till varandra. Att hämta data i applikationens-controllers för aktuell tabell samt relaterade tabeller samtidigt är något vi inte gått igenom i kursen.
* Min lösning är också publicerad.
* Ansluter och lagrar data i en extern mariaDb-databas
* Email-funktionalitet via ansluten email-server med lämpligt namn på email-adressen.

### Tabeller/Modeller


|                         | Competition                                |              |
|-------------------------|--------------------------------------------|--------------|
|CompetitionId            |int autoincrement                           | PK           |
|CompetitionName          |varchar (required)                          |              |
|CourseId                 |int (required, FK från `Course`)            | FK           |
|CompetitionDate          |datetime (required)                         |              |
|MaxPlayerCount           |int (required)                              |              |
|PublicUserId             |int (required, FK från `PublicUser`)        | FK           |
|Registrations            |ICollection (optional, länkad tabell)       |              |


|                         | Course                                     |              |
|-------------------------|--------------------------------------------|--------------|
|CourseId                 |int autoincrement                           | PK           |
|Par                      |varchar (required)                          |              |
|CourseName               |varchar(150) (required)                     |              |



|                         | Playing                                    |              |
|-------------------------|--------------------------------------------|--------------|
|PlayingId                |int autoincrement                           | PK           |
|PlayId                   |int (required, FK från `Play`)              | FK           |
|Par                      |varchar (optional)                          |              |
|GroupNr                  |int (optional)                              |              |
|PublicUserId             |int (required, FK från `PublicUser`)        | FK           |
|RegisterDate             |datetime (default: DateTime.Now)            |              |


|                         | Play                                       |              |
|-------------------------|--------------------------------------------|--------------|
|PlayId                   |int autoincrement                           | PK           |
|CompetitionId            |int (optional, FK från `Competition`)       | FK           |
|CourseId                 |int (required, FK från `Course`)            | FK           |
|Active                   |bool (default: false)                       |              |


|                         | Registration                               |              |
|-------------------------|--------------------------------------------|--------------|
|RegistrationId           |int autoincrement                           | PK           |
|CompetitionId            |int (optional, FK från `Competition`)       | FK           |
|PublicUserId             |int (required, FK från `PublicUser`)        | FK           |
|RegisterDate             |datetime (default: DateTime.Now)            |              |


|                         | PublicUser                                 |              |
|-------------------------|--------------------------------------------|--------------|
|PublicUserId             |int autoincrement                           | PK           |
|DisplayName              |varchar(100) (required)                     |              |
|PDGA_Nr                  |int (optional)                              |              |
|PictureURL               |varchar(300) (optional)                     |              |
|Id                       |varchar (required, FK från `IdentityUser`)  | FK           |

**IdentityUser är id från .NET Identity** 


### Skapad av MARKUS VICKMAN (MAVI2302) 
