# Loggbok - Prague Parking V1
**Namn:** Ahin

---

## 28 september 2025: Projektstart och strukturering

Idag startade jag projektet. Efter att ha läst igenom kraven insåg jag att hela parkeringshuset ska representeras av en enda string-array. Det kändes direkt som en utmaning att lagra komplex data som `BIL#REGNR` i en enkel sträng.

**Fundering:** Hur håller jag ordning på alla krav?

**Tanke:** Det bästa är nog att lägga in kraven som kommentarer direkt i koden. Jag skapar en `/* */`-block-kommentar högst upp med de tekniska reglerna (100 platser, max 10 tecken etc.) och en `//TODO`-lista för varje G-krav. Då har jag en checklista direkt i Visual Studio som jag kan bocka av.

**Lösning:** Skapade en grundstruktur för projektet med en `string[] parkingGarage = new string[100];` och en tom `PragueParking()`-metod. Lade till kommentarer och en TODO-lista för att guida arbetet.

---

## 29 september 2025: Gränssnitt, menyloop och första `AddVehicle`-logik

Fokus idag var att få till ett grundläggande menysystem och börja med den allra första versionen av `AddVehicle`-funktionen.

**Fundering:** Ett enkelt svartvitt gränssnitt känns tråkigt. Jag vill att det ska matcha temat. Hur kan jag ändra bakgrundsfärgen för hela terminalen till röd, som Prags flagga?

**Tanke:** Jag testade `Console.BackgroundColor`, men det ändrade bara färgen precis bakom texten. Jag sökte online och insåg att jag kanske måste "tvinga" terminalen att rita om sig själv. Tänk om jag sätter färgen och sedan rensar hela skärmen?

**Lösning:** Kombinationen `Console.BackgroundColor = ConsoleColor.Red;` följt av `Console.Clear();` fungerade perfekt. Jag kommenterade ut den färdiga `PragueParking()`-metoden för tillfället för att helt kunna fokusera på `AddVehicle`.

**Fundering:** Hur börjar jag med `AddVehicle`? Jag behöver först hämta information från användaren.

**Tanke:** Jag behöver två `Console.ReadLine()`: en för att fråga om fordonstyp (Bil/MC) och en för registreringsnumret. Jag skapar variablerna `vehicle` och `plateNumber` för att spara dessa.

**Lösning:** Skapade en `AddVehicle()`-metod som ställer två frågor och läser in svaren.

**Fundering:** Jag försöker jämföra användarens val för fordonstyp, men det fungerar inte. Min kod är `if (vehicle == 1)`.

**Tanke:** Efter lite felsökning insåg jag mitt misstag. `Console.ReadLine()` returnerar alltid en `string`. Jag kan inte jämföra en `string` direkt med ett heltal (`int`) som `1`. Jag måste jämföra det med textsträngen `"1"`.

**Lösning:** Ändrade villkoret till `if (vehicle == "1")`. Just nu är `if`-blocket tomt, men strukturen är på plats. Jag lade till en `Console.WriteLine` i slutet för att försöka se om något sparas, men jag insåg snabbt att jag inte har lagt till någon logik för att faktiskt spara `plateNumber` i `vehicles`-arrayen än. Dagens mål var att få strukturen på plats, imorgon fortsätter jag med själva parkeringslogiken.

---

## 30 september 2025: Söklogik, felhantering och specialfall för MC

Idag var målet att bygga ut den riktiga logiken för `AddVehicle`-metoden. Jag insåg snabbt att jag inte bara kan parkera på plats 0, utan måste hitta den första lediga platsen.

**Fundering:** Hur hittar jag den första tomma platsen i en loop, och hur vet jag när jag ska sluta leta?

**Tanke:** Jag behöver en `for`-loop som går från 0 till 99. Inuti loopen måste jag kolla om `parkingGarage[index]` är tom med `String.IsNullOrEmpty`. Om platsen är ledig, vill jag spara det indexet och sedan omedelbart avbryta loopen med `break`. Men vad händer om det är fullt? Då kommer loopen köras klart. Jag behöver ett sätt att signalera "hittades inte".

**Lösning:** Jag skapade en `int parkingSpot = -1;`-variabel innan loopen. Värdet -1 är min signal för "hittades inte" eftersom ett giltigt index aldrig är negativt. Inuti loopen, om en tom plats hittas, sätter jag `parkingSpot = index;` och anropar `break;`. Efter loopen kan jag kolla `if (parkingSpot != -1)`.

**Fundering:** Min `AddVehicle`-metod kraschade när jag försökte parkera när det var fullt.

**Tanke:** Jag insåg att jag saknade `if (parkingSpot != -1)`-kontrollen. Min kod försökte använda `parkingSpot` (som fortfarande var -1) för att komma åt `parkingGarage[-1]`, vilket är en omöjlig position.

**Lösning:** Lade till en övergripande `if/else`-struktur efter sök-loopen. All logik för att parkera fordonet ligger nu inuti `if`-blocket, och ett felmeddelande om att det är fullt ligger i `else`-blocket.

**Fundering:** Hur hanterar jag specialfallet med två motorcyklar? Logiken måste vara smartare än att bara leta efter en helt tom plats.

**Tanke:** För en MC bör jag först leta efter en plats där det redan står en MC ensam. Hur känner jag igen en sådan? Den måste innehålla "MC#" men *inte* innehålla `|`. Om den sökningen misslyckas, *då* kan jag falla tillbaka på min vanliga sökning efter en helt tom plats.

**Lösning:** I `AddVehicle`, lade jag till ett `if (vehicleChoice == "2")`-block i början. Inuti det finns en ny loop som letar efter en halvfull MC-plats. Om den hittar en, uppdaterar den platsen med `+= "|MC#" + plateNumber;` och använder `return` för att avsluta hela metoden direkt. Om den loopen inte hittar något, fortsätter koden som vanligt till den generella sökningen efter en tom plats.

**Status:** `AddVehicle`-metoden är nu i stort sett komplett för G-nivån. Jag har också aktiverat `PragueParking()`-metoden och kopplat menyval "1" till den nya, smartare `AddVehicle`.

---

## 1 oktober 2025: Stor refaktorering och implementering av sökfunktion

Idag kände jag att koden började bli rörig och svår att underhålla. `AddVehicle`-metoden gjorde för mycket, och menyn var knappt påbörjad. Mitt huvudmål var att separera ansvarsområden: menyn ska hantera dialog med användaren, och metoderna ska utföra ren logik.

**Fundering:** Min `AddVehicle`-metod frågar efter registreringsnummer och fordonstyp *inuti* metoden. Detta gör den svår att återanvända och testa. Enligt uppgiftsbeskrivningen ska man undvika användardialog i logik-metoder.

**Tanke:** Jag borde refaktorera `AddVehicle`. Istället för att den är `void` och gör allt, ska den ta emot all information den behöver via parametrar. Jag bestämde mig för signaturen `void AddVehicle(string[] garage, string vehicleChoice, string regNum)`. All logik för `Console.ReadLine` ska istället ligga i `PragueParking`-menyn.

**Lösning:** Jag genomförde refaktoreringen. `PragueParking`-metoden innehåller nu logiken för att fråga användaren om fordonstyp och registreringsnummer. Den anropar sedan den nya, renare `AddVehicle`-metoden och skickar med datan. Detta gör koden mycket mer strukturerad.

**Fundering:** Nu är det dags för sökfunktionen. Den måste kunna hitta ett fordon baserat på ett registreringsnummer.

**Tanke:** Sökningen måste loopa igenom hela `parkingGarage`. Men den måste också hantera det komplexa fallet med två motorcyklar på samma plats, t.ex. `"MC#ABC|MC#DEF"`. En enkel `.Contains()` på hela strängen är inte tillräckligt exakt. Jag måste först splitta strängen vid `|`-tecknet och sedan kontrollera varje individuell del.

**Lösning:** Skapade metoden `void SearchVehicle(string regNum)`. Inuti den loopar jag igenom `parkingGarage`. För varje plats som inte är tom, använder jag `spotContent.Split('|')` för att få en array med fordonen på just den platsen. Därefter använder jag en `foreach`-loop för att kontrollera varje `vehicleData` mot det sökta `regNum`. Om en träff hittas, skriver jag ut platsen och använder `return` för att omedelbart avbryta sökningen. Om hela loopen körs klart utan träff, meddelas användaren att fordonet inte hittades.

**Status:** Jag har nu en fungerande meny-loop, en refaktorerad `AddVehicle`-metod och en ny, robust `SearchVehicle`-metod. Jag har också lagt till ett menyval för att avsluta programmet, komplett med en "Är du säker?"-dialog. Koden känns mycket mer organiserad och följer en tydligare designprincip.

---

## 2 oktober 2025: Stor refaktorering och implementering av kärnlogik

Idag var en intensiv dag med fokus på att bygga ut de återstående G-funktionerna (`Remove`, `Search`, `Move`) och göra hela programmet mer robust och användarvänligt.

**Fundering:** Jag insåg att `while`-loopen i menyn för "Ta bort fordon" avbröts även om jag skrev in ett reg-nr som inte fanns. Hur kan menyn veta om borttagningen lyckades?

**Tanke:** Min `RemoveVehicle`-metod var `void`. Den rapporterade aldrig tillbaka till menyn hur det gick. Menyn var "blind". Jag insåg att metoden måste returnera ett svar för att meny-loopen ska kunna fatta ett beslut.

**Lösning:** Jag ändrade `RemoveVehicle` från `void` till `bool`. Nu returnerar den `true` om ett fordon togs bort och `false` om det inte hittades. I menyn kunde jag då skriva `if (RemoveVehicle(...))` och bara köra `break;` om svaret var `true`. Detta gör att loopen fortsätter om fordonet inte hittades, och användaren kan försöka igen. Jag insåg att samma princip måste gälla för `AddVehicle` (om parkeringen är full) och `MoveVehicle`.

**Fundering:** Sökfunktionen ska kunna hantera sökning på både registreringsnummer och platsnummer.

**Tanke:** Jag bör inte ha en enda stor, rörig `SearchVehicle`-metod. Det är bättre att dela upp ansvaret. Menyn kan fråga användaren hur den vill söka. Sedan kan jag ha två mindre, specialiserade metoder: `SearchByRegNum(string regNum)` och `ShowSpotContent(int spotNumber)`.

**Lösning:** I `PragueParking`-menyn, under `userChoice == "4"`, lade jag till en dialog som frågar användaren om söksätt. Baserat på svaret anropas antingen `SearchByRegNum` (som återanvänder `FindVehicleIndexByRegNum`) eller `ShowSpotContent` (som direkt tittar på ett specifikt index i arrayen). Detta gör koden mycket renare.

**Fundering:** När jag skulle ta bort en MC från en delad plats (t.ex. `MC#ABC|MC#ABCDEF`) och angav `ABC`, försvann båda. Varför?

**Tanke:** Jag felsökte och insåg att min `if (!vehicleData.Contains(regNum))` var för generell. Både "MC#ABC" och "MC#ABCDEF" innehåller ju "ABC". Min logik för att hitta fordonet "att behålla" misslyckades därför. Jag insåg att jag behövde vara mycket mer specifik i min jämförelse.

**Lösning:** Jag bytte ut den osäkra `.Contains()`-kontrollen mot en exakt jämförelse. Inuti loopen i `RemoveVehicle` delar jag nu upp varje `vehicleData` (t.ex. `MC#ABC`) vid `#`-tecknet för att få det rena registreringsnumret, och jämför det sedan exakt med `if (currentRegNum != regNum)`. Detta löste buggen.

**Fundering:** Användarupplevelsen i menyn är bristfällig. Om man gör ett fel tvingas man ofta tillbaka till huvudmenyn och måste börja om.

**Tanke:** Jag kan "fånga" användaren i en loop för varje menyval. En `while(true)`-loop för varje `if (userChoice == ...)`-block skulle tvinga användaren att stanna kvar tills de antingen slutför uppgiften korrekt eller aktivt väljer att backa med "0".

**Lösning:** Omslöt logiken för varje menyval (1, 2, 3, 4, 6) i en egen `while(true)`-loop. Lade till alternativ för "(0) för att backa" som använder `break;` för att hoppa ur den inre loopen och återgå till huvudmenyn. Detta gjorde programmet mycket mer robust och förlåtande mot felinmatningar. 

---

## 2 oktober 2025 (fortsättning): Refaktorering för robusthet och påbörjan av `MoveVehicle`

Efter att ha fått grundfunktionerna på plats stötte jag på ett stort problem i användarflödet.

**Fundering:** Jag upptäckte en bugg i mina dialoger för "Lägg till fordon" och "Ta bort fordon". `while`-loopen avbröts alltid och återgick till huvudmenyn, även om operationen misslyckades (t.ex. om parkeringen var full eller om fordonet inte hittades). Användaren fick aldrig en chans att försöka igen utan att behöva välja menyvalet på nytt.

**Tanke:** Jag insåg att min meny-loop var "blind". `AddVehicle`- och `RemoveVehicle`-metoderna var `void` och rapporterade aldrig tillbaka om de lyckades. Loopen bara anropade metoden och antog sedan att allt gick bra, varpå den körde `break`. För att fixa detta måste metoderna kunna kommunicera sitt resultat tillbaka till menyn.

**Lösning:** Jag refaktorerade `AddVehicle` och `RemoveVehicle` till att returnera en `bool`. De returnerar nu `true` vid en lyckad operation och `false` vid alla typer av misslyckanden. I menyn fångar jag nu upp detta returvärde (t.ex. `bool wasAdded = AddVehicle(...)`) och använder en `if`-sats. Endast om resultatet är `true` körs `Console.ReadKey()` och `break;`. Detta gör att "försök igen"-logiken i `while`-looparna äntligen fungerar som avsett.

**Fundering:** Nu är det dags för den mest komplicerade funktionen: `MoveVehicle`. Den behöver flera indata och har många valideringssteg.

**Tanke:** Baserat på min tidigare refaktorering bestämde jag mig för att bygga `MoveVehicle` med en `bool` som returtyp från början. Jag började skissa på signaturen och landade i `bool MoveVehicle(string regNum, int toSpot)`. Jag började också bygga den komplexa dialogen i huvudmenyn för att samla in både registreringsnummer och destinationsplats från användaren.

**Status:** `MoveVehicle`-metoden är påbörjad men inte färdig. Jag har skapat skalet och börjat tänka på valideringsstegen: fordonet måste finnas, destinationen måste vara ett giltigt nummer och den måste vara ledig. Att slutföra denna logik blir huvudfokus för nästa arbetspass. Dagens stora vinst var att göra programflödet konsekvent och logiskt med hjälp av `bool`-returvärden.

---

## 3 oktober 2025: Slutförande av `MoveVehicle` och robustare menyer

Idag var målet att slutföra de sista G-funktionerna, med huvudfokus på den mest komplicerade av dem alla: `MoveVehicle`. Jag ville också göra hela programmet mer robust och användarvänligt.

**Fundering:** `MoveVehicle` är den mest komplexa metoden hittills. Den måste hitta ett fordon, validera en ny plats, och hantera specialfallet med delade MC-platser. Hur strukturerar jag detta på bästa sätt?

**Tanke:** Baserat på mina erfarenheter från igår bestämde jag mig för att bygga metoden med en `bool` som returtyp från början, `bool MoveVehicle(string regNum, int toSpot)`. Jag strukturerade upp metoden med "guard clauses" (tidiga `return false;` om något är fel) i en logisk ordning:
1.  Hitta fordonets `fromIndex`. Om det inte finns, avbryt direkt.
2.  Validera `toSpot` (att den är inom 1-100 och att den är ledig). Om inte, avbryt.
3.  Först därefter, utför själva flytten.

**Fundering:** Jag kom ihåg buggen från igår där `.Contains()` kunde matcha fel MC på en delad plats (t.ex. "ABC" i "ABCDEF"). Jag får inte göra samma misstag i `MoveVehicle`.

**Tanke:** Jag måste återanvända den exakta jämförelsen från `RemoveVehicle`. Inuti `MoveVehicle`, när jag hanterar en delad plats, måste jag identifiera både `vehicleToMove` och `vehicleToKeep` genom att splitta varje del vid `#` och göra en exakt jämförelse av registreringsnumren.

**Lösning:** Implementerade den fullständiga logiken för `MoveVehicle`. Den hanterar nu alla valideringssteg och använder den säkrare, exakta jämförelsen för delade platser. Den uppdaterar sedan både den gamla och nya platsen i arrayen korrekt.

**Fundering:** Användarupplevelsen i menyn är fortfarande bristfällig. Om man gör ett fel tvingas man ofta tillbaka till huvudmenyn och måste börja om.

**Tanke:** Jag kan "fånga" användaren i en loop för varje menyval. En `while(true)`-loop för varje `if (userChoice == ...)`-block skulle tvinga användaren att stanna kvar tills de antingen slutför uppgiften korrekt eller aktivt väljer att backa med "0".

**Lösning:** Jag omslöt logiken för varje menyval (1, 2, 3, 4, 6) i en egen `while(true)`-loop. Jag lade till alternativ för "(0) för att backa" som använder `break;` för att hoppa ur den inre loopen och återgå till huvudmenyn. Detta gjorde programmet mycket mer robust och förlåtande mot felinmatningar.

**Slutsats:** All grundläggande funktionalitet är nu implementerad. Programmet har robusta `while`-loopar för användardialog, konsekvent felhantering, indatavalidering och en genomtänkt design. Programmet uppfyller nu alla krav för G-nivån.