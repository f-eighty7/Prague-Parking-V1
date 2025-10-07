# Loggbok - Prague Parking V1
**Namn:** Ahin

---

## 28 september 2025: Projektstart och strukturering

Idag startade jag projektet. Efter att ha l�st igenom kraven ins�g jag att hela parkeringshuset ska representeras av en enda string-array. Det k�ndes direkt som en utmaning att lagra komplex data som `BIL#REGNR` i en enkel str�ng.

**Fundering:** Hur h�ller jag ordning p� alla krav?

**Tanke:** Det b�sta �r nog att l�gga in kraven som kommentarer direkt i koden. Jag skapar en `/* */`-block-kommentar h�gst upp med de tekniska reglerna (100 platser, max 10 tecken etc.) och en `//TODO`-lista f�r varje G-krav. D� har jag en checklista direkt i Visual Studio som jag kan bocka av.

**L�sning:** Skapade en grundstruktur f�r projektet med en `string[] parkingGarage = new string[100];` och en tom `PragueParking()`-metod. Lade till kommentarer och en TODO-lista f�r att guida arbetet.

---

## 29 september 2025: Gr�nssnitt, menyloop och f�rsta `AddVehicle`-logik

Fokus idag var att f� till ett grundl�ggande menysystem och b�rja med den allra f�rsta versionen av `AddVehicle`-funktionen.

**Fundering:** Ett enkelt svartvitt gr�nssnitt k�nns tr�kigt. Jag vill att det ska matcha temat. Hur kan jag �ndra bakgrundsf�rgen f�r hela terminalen till r�d, som Prags flagga?

**Tanke:** Jag testade `Console.BackgroundColor`, men det �ndrade bara f�rgen precis bakom texten. Jag s�kte online och ins�g att jag kanske m�ste "tvinga" terminalen att rita om sig sj�lv. T�nk om jag s�tter f�rgen och sedan rensar hela sk�rmen?

**L�sning:** Kombinationen `Console.BackgroundColor = ConsoleColor.Red;` f�ljt av `Console.Clear();` fungerade perfekt. Jag kommenterade ut den f�rdiga `PragueParking()`-metoden f�r tillf�llet f�r att helt kunna fokusera p� `AddVehicle`.

**Fundering:** Hur b�rjar jag med `AddVehicle`? Jag beh�ver f�rst h�mta information fr�n anv�ndaren.

**Tanke:** Jag beh�ver tv� `Console.ReadLine()`: en f�r att fr�ga om fordonstyp (Bil/MC) och en f�r registreringsnumret. Jag skapar variablerna `vehicle` och `plateNumber` f�r att spara dessa.

**L�sning:** Skapade en `AddVehicle()`-metod som st�ller tv� fr�gor och l�ser in svaren.

**Fundering:** Jag f�rs�ker j�mf�ra anv�ndarens val f�r fordonstyp, men det fungerar inte. Min kod �r `if (vehicle == 1)`.

**Tanke:** Efter lite fels�kning ins�g jag mitt misstag. `Console.ReadLine()` returnerar alltid en `string`. Jag kan inte j�mf�ra en `string` direkt med ett heltal (`int`) som `1`. Jag m�ste j�mf�ra det med textstr�ngen `"1"`.

**L�sning:** �ndrade villkoret till `if (vehicle == "1")`. Just nu �r `if`-blocket tomt, men strukturen �r p� plats. Jag lade till en `Console.WriteLine` i slutet f�r att f�rs�ka se om n�got sparas, men jag ins�g snabbt att jag inte har lagt till n�gon logik f�r att faktiskt spara `plateNumber` i `vehicles`-arrayen �n. Dagens m�l var att f� strukturen p� plats, imorgon forts�tter jag med sj�lva parkeringslogiken.

---

## 30 september 2025: S�klogik, felhantering och specialfall f�r MC

Idag var m�let att bygga ut den riktiga logiken f�r `AddVehicle`-metoden. Jag ins�g snabbt att jag inte bara kan parkera p� plats 0, utan m�ste hitta den f�rsta lediga platsen.

**Fundering:** Hur hittar jag den f�rsta tomma platsen i en loop, och hur vet jag n�r jag ska sluta leta?

**Tanke:** Jag beh�ver en `for`-loop som g�r fr�n 0 till 99. Inuti loopen m�ste jag kolla om `parkingGarage[index]` �r tom med `String.IsNullOrEmpty`. Om platsen �r ledig, vill jag spara det indexet och sedan omedelbart avbryta loopen med `break`. Men vad h�nder om det �r fullt? D� kommer loopen k�ras klart. Jag beh�ver ett s�tt att signalera "hittades inte".

**L�sning:** Jag skapade en `int parkingSpot = -1;`-variabel innan loopen. V�rdet -1 �r min signal f�r "hittades inte" eftersom ett giltigt index aldrig �r negativt. Inuti loopen, om en tom plats hittas, s�tter jag `parkingSpot = index;` och anropar `break;`. Efter loopen kan jag kolla `if (parkingSpot != -1)`.

**Fundering:** Min `AddVehicle`-metod kraschade n�r jag f�rs�kte parkera n�r det var fullt.

**Tanke:** Jag ins�g att jag saknade `if (parkingSpot != -1)`-kontrollen. Min kod f�rs�kte anv�nda `parkingSpot` (som fortfarande var -1) f�r att komma �t `parkingGarage[-1]`, vilket �r en om�jlig position.

**L�sning:** Lade till en �vergripande `if/else`-struktur efter s�k-loopen. All logik f�r att parkera fordonet ligger nu inuti `if`-blocket, och ett felmeddelande om att det �r fullt ligger i `else`-blocket.

**Fundering:** Hur hanterar jag specialfallet med tv� motorcyklar? Logiken m�ste vara smartare �n att bara leta efter en helt tom plats.

**Tanke:** F�r en MC b�r jag f�rst leta efter en plats d�r det redan st�r en MC ensam. Hur k�nner jag igen en s�dan? Den m�ste inneh�lla "MC#" men *inte* inneh�lla `|`. Om den s�kningen misslyckas, *d�* kan jag falla tillbaka p� min vanliga s�kning efter en helt tom plats.

**L�sning:** I `AddVehicle`, lade jag till ett `if (vehicleChoice == "2")`-block i b�rjan. Inuti det finns en ny loop som letar efter en halvfull MC-plats. Om den hittar en, uppdaterar den platsen med `+= "|MC#" + plateNumber;` och anv�nder `return` f�r att avsluta hela metoden direkt. Om den loopen inte hittar n�got, forts�tter koden som vanligt till den generella s�kningen efter en tom plats.

**Status:** `AddVehicle`-metoden �r nu i stort sett komplett f�r G-niv�n. Jag har ocks� aktiverat `PragueParking()`-metoden och kopplat menyval "1" till den nya, smartare `AddVehicle`.

---

## 1 oktober 2025: Stor refaktorering och implementering av s�kfunktion

Idag k�nde jag att koden b�rjade bli r�rig och sv�r att underh�lla. `AddVehicle`-metoden gjorde f�r mycket, och menyn var knappt p�b�rjad. Mitt huvudm�l var att separera ansvarsomr�den: menyn ska hantera dialog med anv�ndaren, och metoderna ska utf�ra ren logik.

**Fundering:** Min `AddVehicle`-metod fr�gar efter registreringsnummer och fordonstyp *inuti* metoden. Detta g�r den sv�r att �teranv�nda och testa. Enligt uppgiftsbeskrivningen ska man undvika anv�ndardialog i logik-metoder.

**Tanke:** Jag borde refaktorera `AddVehicle`. Ist�llet f�r att den �r `void` och g�r allt, ska den ta emot all information den beh�ver via parametrar. Jag best�mde mig f�r signaturen `void AddVehicle(string[] garage, string vehicleChoice, string regNum)`. All logik f�r `Console.ReadLine` ska ist�llet ligga i `PragueParking`-menyn.

**L�sning:** Jag genomf�rde refaktoreringen. `PragueParking`-metoden inneh�ller nu logiken f�r att fr�ga anv�ndaren om fordonstyp och registreringsnummer. Den anropar sedan den nya, renare `AddVehicle`-metoden och skickar med datan. Detta g�r koden mycket mer strukturerad.

**Fundering:** Nu �r det dags f�r s�kfunktionen. Den m�ste kunna hitta ett fordon baserat p� ett registreringsnummer.

**Tanke:** S�kningen m�ste loopa igenom hela `parkingGarage`. Men den m�ste ocks� hantera det komplexa fallet med tv� motorcyklar p� samma plats, t.ex. `"MC#ABC|MC#DEF"`. En enkel `.Contains()` p� hela str�ngen �r inte tillr�ckligt exakt. Jag m�ste f�rst splitta str�ngen vid `|`-tecknet och sedan kontrollera varje individuell del.

**L�sning:** Skapade metoden `void SearchVehicle(string regNum)`. Inuti den loopar jag igenom `parkingGarage`. F�r varje plats som inte �r tom, anv�nder jag `spotContent.Split('|')` f�r att f� en array med fordonen p� just den platsen. D�refter anv�nder jag en `foreach`-loop f�r att kontrollera varje `vehicleData` mot det s�kta `regNum`. Om en tr�ff hittas, skriver jag ut platsen och anv�nder `return` f�r att omedelbart avbryta s�kningen. Om hela loopen k�rs klart utan tr�ff, meddelas anv�ndaren att fordonet inte hittades.

**Status:** Jag har nu en fungerande meny-loop, en refaktorerad `AddVehicle`-metod och en ny, robust `SearchVehicle`-metod. Jag har ocks� lagt till ett menyval f�r att avsluta programmet, komplett med en "�r du s�ker?"-dialog. Koden k�nns mycket mer organiserad och f�ljer en tydligare designprincip.

---

## 2 oktober 2025: Stor refaktorering och implementering av k�rnlogik

Idag var en intensiv dag med fokus p� att bygga ut de �terst�ende G-funktionerna (`Remove`, `Search`, `Move`) och g�ra hela programmet mer robust och anv�ndarv�nligt.

**Fundering:** Jag ins�g att `while`-loopen i menyn f�r "Ta bort fordon" avbr�ts �ven om jag skrev in ett reg-nr som inte fanns. Hur kan menyn veta om borttagningen lyckades?

**Tanke:** Min `RemoveVehicle`-metod var `void`. Den rapporterade aldrig tillbaka till menyn hur det gick. Menyn var "blind". Jag ins�g att metoden m�ste returnera ett svar f�r att meny-loopen ska kunna fatta ett beslut.

**L�sning:** Jag �ndrade `RemoveVehicle` fr�n `void` till `bool`. Nu returnerar den `true` om ett fordon togs bort och `false` om det inte hittades. I menyn kunde jag d� skriva `if (RemoveVehicle(...))` och bara k�ra `break;` om svaret var `true`. Detta g�r att loopen forts�tter om fordonet inte hittades, och anv�ndaren kan f�rs�ka igen. Jag ins�g att samma princip m�ste g�lla f�r `AddVehicle` (om parkeringen �r full) och `MoveVehicle`.

**Fundering:** S�kfunktionen ska kunna hantera s�kning p� b�de registreringsnummer och platsnummer.

**Tanke:** Jag b�r inte ha en enda stor, r�rig `SearchVehicle`-metod. Det �r b�ttre att dela upp ansvaret. Menyn kan fr�ga anv�ndaren hur den vill s�ka. Sedan kan jag ha tv� mindre, specialiserade metoder: `SearchByRegNum(string regNum)` och `ShowSpotContent(int spotNumber)`.

**L�sning:** I `PragueParking`-menyn, under `userChoice == "4"`, lade jag till en dialog som fr�gar anv�ndaren om s�ks�tt. Baserat p� svaret anropas antingen `SearchByRegNum` (som �teranv�nder `FindVehicleIndexByRegNum`) eller `ShowSpotContent` (som direkt tittar p� ett specifikt index i arrayen). Detta g�r koden mycket renare.

**Fundering:** N�r jag skulle ta bort en MC fr�n en delad plats (t.ex. `MC#ABC|MC#ABCDEF`) och angav `ABC`, f�rsvann b�da. Varf�r?

**Tanke:** Jag fels�kte och ins�g att min `if (!vehicleData.Contains(regNum))` var f�r generell. B�de "MC#ABC" och "MC#ABCDEF" inneh�ller ju "ABC". Min logik f�r att hitta fordonet "att beh�lla" misslyckades d�rf�r. Jag ins�g att jag beh�vde vara mycket mer specifik i min j�mf�relse.

**L�sning:** Jag bytte ut den os�kra `.Contains()`-kontrollen mot en exakt j�mf�relse. Inuti loopen i `RemoveVehicle` delar jag nu upp varje `vehicleData` (t.ex. `MC#ABC`) vid `#`-tecknet f�r att f� det rena registreringsnumret, och j�mf�r det sedan exakt med `if (currentRegNum != regNum)`. Detta l�ste buggen.

**Fundering:** Anv�ndarupplevelsen i menyn �r bristf�llig. Om man g�r ett fel tvingas man ofta tillbaka till huvudmenyn och m�ste b�rja om.

**Tanke:** Jag kan "f�nga" anv�ndaren i en loop f�r varje menyval. En `while(true)`-loop f�r varje `if (userChoice == ...)`-block skulle tvinga anv�ndaren att stanna kvar tills de antingen slutf�r uppgiften korrekt eller aktivt v�ljer att backa med "0".

**L�sning:** Omsl�t logiken f�r varje menyval (1, 2, 3, 4, 6) i en egen `while(true)`-loop. Lade till alternativ f�r "(0) f�r att backa" som anv�nder `break;` f�r att hoppa ur den inre loopen och �terg� till huvudmenyn. Detta gjorde programmet mycket mer robust och f�rl�tande mot felinmatningar. 

---

## 2 oktober 2025 (forts�ttning): Refaktorering f�r robusthet och p�b�rjan av `MoveVehicle`

Efter att ha f�tt grundfunktionerna p� plats st�tte jag p� ett stort problem i anv�ndarfl�det.

**Fundering:** Jag uppt�ckte en bugg i mina dialoger f�r "L�gg till fordon" och "Ta bort fordon". `while`-loopen avbr�ts alltid och �tergick till huvudmenyn, �ven om operationen misslyckades (t.ex. om parkeringen var full eller om fordonet inte hittades). Anv�ndaren fick aldrig en chans att f�rs�ka igen utan att beh�va v�lja menyvalet p� nytt.

**Tanke:** Jag ins�g att min meny-loop var "blind". `AddVehicle`- och `RemoveVehicle`-metoderna var `void` och rapporterade aldrig tillbaka om de lyckades. Loopen bara anropade metoden och antog sedan att allt gick bra, varp� den k�rde `break`. F�r att fixa detta m�ste metoderna kunna kommunicera sitt resultat tillbaka till menyn.

**L�sning:** Jag refaktorerade `AddVehicle` och `RemoveVehicle` till att returnera en `bool`. De returnerar nu `true` vid en lyckad operation och `false` vid alla typer av misslyckanden. I menyn f�ngar jag nu upp detta returv�rde (t.ex. `bool wasAdded = AddVehicle(...)`) och anv�nder en `if`-sats. Endast om resultatet �r `true` k�rs `Console.ReadKey()` och `break;`. Detta g�r att "f�rs�k igen"-logiken i `while`-looparna �ntligen fungerar som avsett.

**Fundering:** Nu �r det dags f�r den mest komplicerade funktionen: `MoveVehicle`. Den beh�ver flera indata och har m�nga valideringssteg.

**Tanke:** Baserat p� min tidigare refaktorering best�mde jag mig f�r att bygga `MoveVehicle` med en `bool` som returtyp fr�n b�rjan. Jag b�rjade skissa p� signaturen och landade i `bool MoveVehicle(string regNum, int toSpot)`. Jag b�rjade ocks� bygga den komplexa dialogen i huvudmenyn f�r att samla in b�de registreringsnummer och destinationsplats fr�n anv�ndaren.

**Status:** `MoveVehicle`-metoden �r p�b�rjad men inte f�rdig. Jag har skapat skalet och b�rjat t�nka p� valideringsstegen: fordonet m�ste finnas, destinationen m�ste vara ett giltigt nummer och den m�ste vara ledig. Att slutf�ra denna logik blir huvudfokus f�r n�sta arbetspass. Dagens stora vinst var att g�ra programfl�det konsekvent och logiskt med hj�lp av `bool`-returv�rden.

---

## 3 oktober 2025: Slutf�rande av `MoveVehicle` och robustare menyer

Idag var m�let att slutf�ra de sista G-funktionerna, med huvudfokus p� den mest komplicerade av dem alla: `MoveVehicle`. Jag ville ocks� g�ra hela programmet mer robust och anv�ndarv�nligt.

**Fundering:** `MoveVehicle` �r den mest komplexa metoden hittills. Den m�ste hitta ett fordon, validera en ny plats, och hantera specialfallet med delade MC-platser. Hur strukturerar jag detta p� b�sta s�tt?

**Tanke:** Baserat p� mina erfarenheter fr�n ig�r best�mde jag mig f�r att bygga metoden med en `bool` som returtyp fr�n b�rjan, `bool MoveVehicle(string regNum, int toSpot)`. Jag strukturerade upp metoden med "guard clauses" (tidiga `return false;` om n�got �r fel) i en logisk ordning:
1.  Hitta fordonets `fromIndex`. Om det inte finns, avbryt direkt.
2.  Validera `toSpot` (att den �r inom 1-100 och att den �r ledig). Om inte, avbryt.
3.  F�rst d�refter, utf�r sj�lva flytten.

**Fundering:** Jag kom ih�g buggen fr�n ig�r d�r `.Contains()` kunde matcha fel MC p� en delad plats (t.ex. "ABC" i "ABCDEF"). Jag f�r inte g�ra samma misstag i `MoveVehicle`.

**Tanke:** Jag m�ste �teranv�nda den exakta j�mf�relsen fr�n `RemoveVehicle`. Inuti `MoveVehicle`, n�r jag hanterar en delad plats, m�ste jag identifiera b�de `vehicleToMove` och `vehicleToKeep` genom att splitta varje del vid `#` och g�ra en exakt j�mf�relse av registreringsnumren.

**L�sning:** Implementerade den fullst�ndiga logiken f�r `MoveVehicle`. Den hanterar nu alla valideringssteg och anv�nder den s�krare, exakta j�mf�relsen f�r delade platser. Den uppdaterar sedan b�de den gamla och nya platsen i arrayen korrekt.

**Fundering:** Anv�ndarupplevelsen i menyn �r fortfarande bristf�llig. Om man g�r ett fel tvingas man ofta tillbaka till huvudmenyn och m�ste b�rja om.

**Tanke:** Jag kan "f�nga" anv�ndaren i en loop f�r varje menyval. En `while(true)`-loop f�r varje `if (userChoice == ...)`-block skulle tvinga anv�ndaren att stanna kvar tills de antingen slutf�r uppgiften korrekt eller aktivt v�ljer att backa med "0".

**L�sning:** Jag omsl�t logiken f�r varje menyval (1, 2, 3, 4, 6) i en egen `while(true)`-loop. Jag lade till alternativ f�r "(0) f�r att backa" som anv�nder `break;` f�r att hoppa ur den inre loopen och �terg� till huvudmenyn. Detta gjorde programmet mycket mer robust och f�rl�tande mot felinmatningar.

**Slutsats:** All grundl�ggande funktionalitet �r nu implementerad. Programmet har robusta `while`-loopar f�r anv�ndardialog, konsekvent felhantering, indatavalidering och en genomt�nkt design. Programmet uppfyller nu alla krav f�r G-niv�n.