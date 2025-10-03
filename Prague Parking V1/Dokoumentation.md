### 2025-09-27: Projektstart och gränssnitt

* **Aktivitet:** Undersökte möjligheten att anpassa användargränssnittets utseende i terminalen.
* **Problem/Fundering:** Hur kan man programmatiskt ändra bakgrundsfärgen för hela konsolfönstret? Varför återställdes den röda färgen till svart efter en viss utskrift?
* **Lösning/Slutsats:** Lösningen var att kombinera `Console.BackgroundColor` med `Console.Clear()`. Felsökningen visade att en ANSI escape-sekvens (`\u001b[0m`) i en utskriftssträng återställde alla färger. Detta löstes genom att antingen ta bort koden eller hantera färgbyten med C#-metoder (`Console.ForegroundColor`).

---

### 2025-09-27 – 2025-09-30: Grundläggande logik och array-hantering

* **Aktivitet:** Utforskade datatyper för lagring av registreringsnummer.
* **Problem/Fundering:** Hur representerar man ett registreringsnummer som innehåller både bokstäver och siffror?
* **Lösning/Slutsats:** Datatypen `string` är den korrekta att använda, vilket också specificeras i kraven.

* **Aktivitet:** Iterativ utveckling och felsökning av `AddVehicle`-metodens grundlogik.
* **Problem/Fundering:** En serie logiska fel uppstod:
    1.  Data sparades inte i arrayen på grund av felvänd tilldelning (`variabel = array[i]` istället för `array[i] = variabel`).
    2.  En sök-loop avslutade hela metoden i förtid med `return`.
    3.  En felplacerad `break` avslutade loopen efter bara ett varv.
    4.  Programmet kraschade vid full parkering eftersom det saknades kontroll för ett misslyckat sökresultat.
* **Lösning/Slutsats:** En robust trestegsstruktur utvecklades: 1. **Sök** (en loop letar efter ett index och sparar det), 2. **Kontrollera** (en `if`-sats efter loopen kollar om ett giltigt index hittades), 3. **Agera** (logiken för att parkera fordonet körs bara om kontrollen lyckades).

* **Aktivitet:** Fördjupning i konceptet signalvärden.
* **Problem/Fundering:** Vad är syftet med att initiera en index-variabel till värdet `-1`?
* **Lösning/Slutsats:** Värdet `-1` används som en standardiserad signalflagga för "ännu ej hittad" eller "finns inte", eftersom ett giltigt array-index aldrig kan vara negativt.

---

### 2025-09-30 – 2025-10-01: Refaktorering och kodåteranvändning

* **Aktivitet:** Funderingar om kodstruktur, variabel-scope och användning av parametrar.
* **Problem/Fundering:** Är `parkingGarage`-arrayen global? Borde metoderna använda parametrar istället för att vara beroende av en global variabel?
* **Lösning/Slutsats:** Bekräftade att `parkingGarage` fungerar som en delad resurs för alla metoder. Koden refaktorerades för att använda parametrar, vilket gör metoderna mer fristående och testbara.

* **Aktivitet:** Diskussion om metoders returtyper (`void` vs. `bool`).
* **Problem/Fundering:** När är det lämpligt att en metod är `void` och när bör den returnera ett värde?
* **Lösning/Slutsats:** Konstaterade att metoder som ändrar data och kan misslyckas (`Add`, `Remove`) bör returnera en `bool` för att kunna kommunicera resultatet till meny-loopen. Metoder som bara visar data (`Search`, `ShowAll`) kan vara `void`.

* **Aktivitet:** Insikt om kodduplicering vid sökning.
* **Problem/Fundering:** Söklogiken för att hitta ett fordon behövs i flera metoder (`SearchVehicle`, `MoveVehicle`, `RemoveVehicle`). Måste den kopieras?
* **Lösning/Slutsats:** Den gemensamma söklogiken bröts ut till en ny, återanvändbar hjälpmetod: `FindVehicleIndexByRegNum(string regNum)`, som returnerar fordonets index eller -1. `SearchVehicle` refaktorerades för att använda denna hjälpmetod, vilket minskade kodduplicering.

---

### 2025-10-01 – 2025-10-03: Implementation av huvudfunktioner och felsökning

* **Aktivitet:** Planering och implementation av `MoveVehicle`.
* **Problem/Fundering:** Hur interagerar `MoveVehicle` med `AddVehicle`?
* **Lösning/Slutsats:** Fastställde att `AddVehicle` korrekt återanvänder platser som `MoveVehicle` frigör.

* **Aktivitet:** Felsökning av ett oväntat beteende i `RemoveVehicle`.
* **Problem/Fundering:** När man tryckte Enter utan att skriva in ett reg-nr, raderades det första fordonet i garaget. Varför?
* **Lösning/Slutsats:** Buggen orsakades av att `Console.ReadLine()` returnerar en tom sträng (`""`) och att `anyString.Contains("")` alltid är sant. Lösningen var att lägga till indatavalidering i menyn för att säkerställa att `RemoveVehicle` aldrig anropas med en tom sträng.

* **Aktivitet:** Felsökning av ett specialfall i `RemoveVehicle` och `MoveVehicle`.
* **Problem/Fundering:** När man försökte ta bort en MC från en delad plats där ett reg-nr var en del av det andra (t.ex. "ABC" och "ABCDEF"), togs båda bort.
* **Lösning/Slutsats:** Problemet berodde på att den generella `.Contains()`-metoden inte var tillräckligt specifik. Lösningen var att byta ut den mot en **exakt jämförelse** (`==`) efter att först ha extraherat det rena registreringsnumret från datasträngen.

* **Aktivitet:** Iterativ felsökning av valideringslogiken i `MoveVehicle`.
* **Problem/Fundering:** Under implementationen av `MoveVehicle` uppstod logiska funderingar:
    * **Fel ordning:** Koden validerade destinationen *innan* den kontrollerade om källfordonet existerade.
* **Lösning/Slutsats:** Flödet korrigerades till att först hitta källfordonet och sedan validera destinationen. Villkoren för gränskontroll (`toSpot < 1 || toSpot > 100`) och kontroll av upptagen plats (`!String.IsNullOrEmpty`) fastställdes, vilket ledde till en korrekt och logisk struktur för metoden.

* **Aktivitet:** Slutförande av funktionerna.
* **Lösning/Slutsats:** All grundläggande funktionalitet (`Meny`, `Add`, `Remove`, `Move`, `Search`) är nu implementerad med robusta `while`-loopar för användardialog, felhantering, indatavalidering och konsekvent design.