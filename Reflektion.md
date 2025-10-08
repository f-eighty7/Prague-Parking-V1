# Reflektion – Prague Parking V1.1
**Av: Ahin**

---

### 1. Sammanfattning
Detta projekt gick ut på att utveckla ett textbaserat konsolprogram för att hantera en parkeringsplats i Prag. Systemet, "Prague Parking V1.1", är designat för att vara enkelt och robust för personalen att använda och hanterar parkering av bilar och motorcyklar enligt en uppsättning specifika tekniska regler. Den färdiga applikationen uppfyller samtliga krav för betyget Godkänt, inklusive grundläggande funktioner för att lägga till, ta bort, flytta och söka efter fordon.

För att uppnå betyget Väl Godkänt har jag utökat programmet med två betydande funktioner. För det första implementerade jag en avancerad visuell översikt av parkeringshuset i form av ett färgkodat rutnät, vilket ger personalen en omedelbar överblick av lediga, halvfulla och fulla platser. För det andra har jag genomgående säkrat upp all användarinput, genom att skapa centraliserade och återanvändbara metoder för att validera både parkeringsplatsnummer och registreringsnummers format. Denna reflektion kommer att redogöra för resan från de initiala kraven till den färdiga, robusta applikationen.

### 2. Hur jag löste uppgiften
Min arbetsprocess var iterativ och byggde på att först skapa en fungerande grund och därefter successivt förbättra och refaktorera koden.

#### Grundstruktur och G-nivå
För att lösa G-kraven var mitt fokus att bygga ett fullt fungerande och robust parkeringssystem som uppfyllde alla tekniska specifikationer. Arbetet började med att etablera programmets kärna: en `string[] parkingGarage = new string[100];`, vilket direkt adresserade kravet på 100 parkeringsrutor i en endimensionell array. Den stora utmaningen var att lagra komplex information, om en plats var tom, hade en bil, en MC, eller två MC, i en enkel sträng. Jag löste detta genom att adoptera den rekommenderade datastrukturen och konsekvent använda formatet `BIL#REGNR`, `MC#REGNR` och `MC#REGNR1|MC#REGNR2`. Detta blev grunden för all datamanipulering i programmet.

För att hantera kravet på identifiering och validering av fordon via registreringsnummer (max 10 tecken, inga mellanslag), skapade jag tidigt den återanvändbara hjälpmetoden `isRegNumValid()`. Genom att centralisera denna logik och anropa den från menyn innan någon åtgärd utfördes, säkerställde jag att endast korrekt formaterad data skickades vidare i systemet.

Slutligen, för att bygga själva systemet med dess textbaserade meny, tillämpade jag designprincipen "Separation of Concerns". Jag lät `PragueParking()`-metoden ensam ansvara för all användardialog, medan mindre, specialiserade metoder (`AddVehicle`, `RemoveVehicle` etc.) fick sköta den bakomliggande logiken. En avgörande insikt för att skapa en robust användarupplevelse var att omsluta varje menyval i en egen `while(true)`-loop, vilket förhindrade att användaren kastades ut vid felinmatning. Detta ledde till en viktig refaktorering där jag ändrade returtypen på logik-metoderna från `void` till `bool`, vilket gav menyn den nödvändiga feedbacken för att veta om en "försök igen"-loop skulle fortsätta eller avbrytas.

#### VG-funktioner
När G-kraven var uppfyllda och koden var stabil, påbörjade jag arbetet med VG-funktionerna. Mitt mål var att bygga vidare på den befintliga, robusta grunden. För att ge personalen en bättre överblick skapade jag en ny metod, `VisualizeParkingLot()`, som presenterar hela parkeringshuset som ett visuellt rutnät. Utmaningen var att simulera ett tvådimensionellt rutnät från en endimensionell array. Genom att iterera igenom arrayen och använda `Console.Write` kunde jag bygga upp rader av platser. Nyckeln till att skapa själva rutnätet var att använda modulo-operatorn `((i + 1) % 5 == 0)` för att räkna ut exakt när en radbrytning skulle ske. Jag lade även till färgkodning (grön, gul, röd) för att ge omedelbar visuell feedback på varje plats status, samt en förklarande text i slutet.

Parallellt med detta arbetade jag med att fullständigt säkra upp all användarinput, vilket var det andra VG-kravet. För platsnummer identifierade jag att logiken för att hämta och validera ett tal (är det en siffra? är den inom 1-100?) upprepades på flera ställen. Jag skapade då en återanvändbar hjälpmetod, `GetValidSpotNumber()`, som centraliserade all denna logik. För registreringsnummer utökade jag min `isRegNumValid`-metod avsevärt. Utöver de grundläggande kontrollerna för längd och mellanslag, implementerade jag en `foreach`-loop som inspekterar varje enskilt tecken i strängen. Genom att använda `char.IsLetterOrDigit()` säkerställde jag att endast bokstäver och siffror, inklusive västeuropeiska tecken som `Å`, `Ä` och `Ö`, är tillåtna. Detta gjorde valideringen komplett och uppfyllde VG-kravet till fullo.

### 3. Utmaningar i uppgiften och hur de löstes
**Datarepresentation och exakthet:** Den största tekniska utmaningen var den begränsningen att använda en `string`-array. Detta ledde till ett svårfunnet fel när jag skulle ta bort en av två motorcyklar. Om deras registreringsnummer var delsträngar av varandra (t.ex. "ABC" och "ABCDEF"), och jag försökte ta bort "ABC", så misslyckades min logik. Jag insåg att en enkel `.Contains()`-kontroll inte var tillräckligt exakt. Lösningen var att, i det specifika fallet med delade platser, splitta varje fordonsdata vid `#`-tecknet för att isolera det rena registreringsnumret. Därefter kunde jag utföra en exakt jämförelse (`==` eller `!=`), vilket löste buggen och gjorde funktionen robust.

**Kontrollflöde och "blinda" metoder:** I början avbröts mina meny-loopar även när en operation misslyckades, till exempel om man försökte ta bort ett fordon som inte fanns. Jag insåg att menyn var "blind" för resultatet av anropen. Lösningen var en större refaktorering där jag ändrade mina "gör"-metoder (`AddVehicle`, `RemoveVehicle`, `MoveVehicle`) från `void` till att returnera en `bool`. Detta skapade en kommunikationskanal som lät menyn veta om den skulle avbryta loopen (`break`) eller fortsätta. Det var en avgörande insikt i hur man bygger interaktiva och responsiva program.

**Användarupplevelse:** En tidig utmaning var att ett enda fel från användaren skickade dem tillbaka till huvudmenyn. Lösningen var att implementera lokala `while(true)`-loopar för varje menyval. Detta, tillsammans med en "(0) för att avbryta"-funktion, skapade ett mycket mer förlåtande och professionellt flöde. Jag lade även till en optimering som kontrollerar om garaget är tomt (`IsGarageEmpty()`) innan funktioner som "Ta bort" eller "Flytta" ens påbörjas, för att undvika onödiga steg för användaren.

### 4. Metoder och modeller som använts
**Separation of Concerns:** Detta var den viktigaste designprincipen i hela projektet. Jag har konsekvent separerat användargränssnittet (allt i `PragueParking`-metoden) från affärslogiken (de andra metoderna). Menyn pratar med användaren, medan de andra metoderna utför tysta, logiska operationer och returnerar ett resultat.

**Don't Repeat Yourself:** När jag märkte att jag skrev samma kod på flera ställen, bröt jag ut den till återanvändbara hjälpmetoder. `FindVehicleIndexByRegNum`, `isRegNumValid`, `GetValidSpotNumber` och `IsGarageEmpty` är centrala exempel som gjorde huvudkoden mycket renare, kortare och enklare att underhålla.

**"Guard Clauses" / Tidig Retur:** I metoder som `MoveVehicle` använde jag medvetet tidiga `return false`-satser för att validera indata. Istället för att ha en stor `if`-sats för det lyckade fallet, kontrollerar jag för alla möjliga fel i början av metoden. Detta gör den huvudsakliga, "lyckade" kodvägen plattare, mer lättläst och förhindrar djupt nästlade `if-else`-strukturer.

### 5. Hur jag skulle lösa uppgiften nästa gång
Jag anser att jag har optimerat koden så gott det går med den kunskap jag har fått från kursen hittills.

Min största lärdom är värdet av att planera för återanvändning tidigt. I början av projektet skrev jag logik direkt där jag behövde den. När jag sedan skulle implementera "Flytta fordon" insåg jag att jag redan hade skrivit nästan samma söklogik för "Sök fordon". Det ledde till en refaktorering där jag skapade hjälpmetoden `FindVehicleIndexByRegNum`. Nästa gång skulle jag, redan från början, försöka identifiera sådana gemensamma uppgifter. Istället för att skriva söklogiken i "Sök"-funktionen först, skulle jag direkt skapa en generell hjälpmetod, medveten om att jag kommer att behöva den igen.

En annan sak jag skulle göra annorlunda är hanteringen av textsträngar som `"BIL#"` och `"|"`. Jag har skrivit dessa direkt i koden på flera ställen. Jag har lärt mig att detta kan vara riskabelt; om jag skulle behöva ändra hur en bil identifieras, måste jag hitta och ändra det på alla ställen. Även med bara grundläggande variabler skulle jag nästa gång skapa några variabler högst upp i programmet, till exempel `string carIdentifier = "BIL#";`. Genom att sedan använda den variabeln överallt i koden, skulle jag kunna göra framtida ändringar på ett enda, säkert ställe.

Slutligen har jag insett kraften i den `while(true)`-struktur vi byggde för menyvalen. I början var min felhantering enklare och mindre förlåtande. Att landa i designen där varje menyval har sin egen loop som "fångar" användaren tills uppgiften är helt klar (eller avbruten) var en process. Nästa gång skulle jag bygga menyflödet med den robusta loop-strukturen från första böran, eftersom det visade sig vara nyckeln till en bra användarupplevelse.

### 6. Slutsats hemuppgift
Projektet var en utmärkt och praktisk övning i grundläggande C#-programmering och, framför allt, i problemlösning. Den största lärdomen för mig personligen har varit resan från att skriva en enkel, funktionell kod till att aktivt refaktorera den till en mer robust och välstrukturerad applikation. Att se hur principer som DRY och Separation of Concerns inte bara är teoretiska koncept, utan direkt leder till bättre, mer lättläst och mindre buggbenägen kod, har varit en ögonöppnare. Det mest tillfredsställande ögonblicket var när `bool`-returvärdena implementerades och meny-looparna plötsligt betedde sig exakt som avsett, vilket transformerade användarupplevelsen.

### 7. Slutsats kurs
Kursen har hittills gett en väldigt solid grund i C# och det allmänna "programmeringstänket". Detta projekt har varit det perfekta tillfället att praktiskt tillämpa och verkligen förstå de teoretiska koncepten vi gått igenom, såsom metoder, parametrar, returtyper, arrayer, loopar och datatyper. Framförallt har projektet belyst den fundamentala skillnaden mellan kod som bara "fungerar" och kod som är robust, underhållbar och väl-designad.

Att arbeta mot en specifik kravlista med ibland strikta tekniska begränsningar har också varit en nyttig inblick i hur professionell mjukvaruutveckling kan se ut. Jag känner mig nu betydligt mer självsäker i min förmåga.
