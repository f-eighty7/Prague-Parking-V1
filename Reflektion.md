# Reflektion � Prague Parking V1.1

---

### 1. Sammanfattning
Detta projekt gick ut p� att utveckla ett textbaserat konsolprogram f�r att hantera en parkeringsplats i Prag. Systemet, "Prague Parking V1.1", �r designat f�r att vara enkelt och robust f�r personalen att anv�nda och hanterar parkering av bilar och motorcyklar enligt en upps�ttning specifika tekniska regler. Den f�rdiga applikationen uppfyller samtliga krav f�r betyget Godk�nt, inklusive grundl�ggande funktioner f�r att l�gga till, ta bort, flytta och s�ka efter fordon.

F�r att uppn� betyget V�l Godk�nt har jag ut�kat programmet med tv� betydande funktioner. F�r det f�rsta implementerade jag en avancerad visuell �versikt av parkeringshuset i form av ett f�rgkodat rutn�t, vilket ger personalen en omedelbar �verblick av lediga, halvfulla och fulla platser. F�r det andra har jag genomg�ende s�krat upp all anv�ndarinput, framf�r allt genom att skapa en centraliserad och �teranv�ndbar metod f�r att validera inmatning av parkeringsplatsnummer. Denna reflektion kommer att redog�ra f�r resan fr�n de initiala kraven till den f�rdiga, robusta applikationen.

### 2. Hur jag l�ste uppgiften
Min arbetsprocess var iterativ och byggde p� att f�rst skapa en fungerande grund och d�refter successivt f�rb�ttra och refaktorera koden.

#### Grundstruktur och G-niv�
F�r att l�sa G-kraven var mitt fokus att bygga ett fullt fungerande och robust parkeringssystem som uppfyllde alla tekniska specifikationer. Arbetet b�rjade med att etablera programmets k�rna: `string[] parkingGarage = new string[100];`, vilket direkt adresserade kravet p� 100 parkeringsrutor i en endimensionell array. Den stora utmaningen var att lagra komplex information, om en plats var tom, hade en bil, en MC, eller tv� MC, i en enkel str�ng. Jag l�ste detta genom att adoptera den rekommenderade datastrukturen och konsekvent anv�nda formatet `BIL#REGNR`, `MC#REGNR` och `MC#REGNR1|MC#REGNR2`. Detta blev grunden f�r all datamanipulering i programmet.

F�r att hantera kravet p� identifiering och validering av fordon via registreringsnummer (max 10 tecken, inga mellanslag), skapade jag tidigt den �teranv�ndbara hj�lpmetoden `isRegNumValid()`. Genom att centralisera denna logik och anropa den fr�n menyn innan n�gon �tg�rd utf�rdes, s�kerst�llde jag att endast korrekt formaterad data skickades vidare i systemet.

Slutligen, f�r att bygga sj�lva systemet med dess textbaserade meny, till�mpade jag designprincipen "Separation of Concerns". Jag l�t `PragueParking()`-metoden ensam ansvara f�r all anv�ndardialog, medan mindre, specialiserade metoder (`AddVehicle`, `RemoveVehicle` etc.) fick sk�ta den bakomliggande logiken. En avg�rande insikt f�r att skapa en robust anv�ndarupplevelse var att omsluta varje menyval i en egen `while(true)`-loop, vilket f�rhindrade att anv�ndaren kastades ut vid felinmatning. Detta ledde till en viktig refaktorering d�r jag �ndrade returtypen p� logik-metoderna fr�n `void` till `bool`, vilket gav menyn den n�dv�ndiga feedbacken f�r att veta om en "f�rs�k igen"-loop skulle forts�tta eller avbrytas.

#### VG-funktioner
N�r G-kraven var uppfyllda och koden var stabil, p�b�rjade jag arbetet med VG-funktionerna. Mitt m�l var att bygga vidare p� den befintliga, robusta grunden. F�r att ge personalen en b�ttre �verblick skapade jag en ny metod, `VisualizeParkingLot()`, som presenterar hela parkeringshuset som ett visuellt rutn�t. Utmaningen var att simulera ett tv�dimensionellt rutn�t fr�n en endimensionell array. Genom att iterera igenom arrayen kunde jag bygga upp rader av platser. Nyckeln till att skapa sj�lva rutn�tet var att anv�nda modulo-operatorn `((i + 1) % 5 == 0)` f�r att r�kna ut exakt n�r en radbrytning skulle ske. Jag lade �ven till f�rgkodning (gr�n, gul, r�d) f�r att ge omedelbar visuell feedback p� varje plats status, samt en f�rklarande text i slutet.

Parallellt med detta arbetade jag med att ytterligare s�kra upp anv�ndarinput. Jag identifierade att logiken f�r att h�mta och validera ett platsnummer (�r det en siffra? �r den inom 1-100?) upprepades p� flera st�llen i min kod. Jag skapade en �teranv�ndbar hj�lpmetod, `GetValidSpotNumber()`. Denna metod inneh�ller en egen loop som inte avslutas f�rr�n anv�ndaren har matat in ett giltigt nummer eller valt att avbryta. Genom att sedan anropa denna metod fr�n menyvalen f�r "Flytta" och "S�k" blev koden i huvudmenyn avsev�rt renare och all valideringslogik f�r platsnummer centraliserades till ett enda, s�kert st�lle.

### 3. Utmaningar i uppgiften och hur de l�stes
**Datarepresentation och exakthet:** Den st�rsta tekniska utmaningen var den begr�nsningen att anv�nda en `string`-array. Detta ledde till ett sv�rfunnet fel n�r jag skulle ta bort en av tv� motorcyklar. Om deras registreringsnummer var delstr�ngar av varandra (t.ex. "ABC" och "ABCDEF"), och jag f�rs�kte ta bort "ABC", s� misslyckades min logik. Jag ins�g att en enkel `.Contains()`-kontroll inte var tillr�ckligt exakt. L�sningen var att, i det specifika fallet med delade platser, splitta varje fordonsdata vid `#`-tecknet f�r att isolera det rena registreringsnumret. D�refter kunde jag utf�ra en exakt j�mf�relse (`==` eller `!=`), vilket l�ste buggen och gjorde funktionen robust.

**Kontrollfl�de och "blinda" metoder:** I b�rjan avbr�ts mina meny-loopar �ven n�r en operation misslyckades, till exempel om man f�rs�kte ta bort ett fordon som inte fanns. Jag ins�g att menyn var "blind" f�r resultatet av anropen. L�sningen var en st�rre refaktorering d�r jag �ndrade mina "g�r"-metoder (`AddVehicle`, `RemoveVehicle`, `MoveVehicle`) fr�n `void` till att returnera en `bool`. Detta skapade en kommunikationskanal som l�t menyn veta om den skulle avbryta loopen (`break`) eller forts�tta. Det var en avg�rande insikt i hur man bygger interaktiva och responsiva program.

**Anv�ndarupplevelse:** En tidig utmaning var att ett enda fel fr�n anv�ndaren skickade dem tillbaka till huvudmenyn. L�sningen var att implementera lokala `while(true)`-loopar f�r varje menyval. Detta, tillsammans med en "(0) f�r att avbryta"-funktion, skapade ett mycket mer f�rl�tande och professionellt fl�de. Jag lade �ven till en optimering som kontrollerar om garaget �r tomt (`IsGarageEmpty()`) innan funktioner som "Ta bort" eller "Flytta" ens p�b�rjas, f�r att undvika on�diga steg f�r anv�ndaren.

### 4. Metoder och modeller som anv�nts
**Separation of Concerns:** Detta var den viktigaste designprincipen i hela projektet. Jag har konsekvent separerat anv�ndargr�nssnittet (allt i `PragueParking`-metoden) fr�n aff�rslogiken (de andra metoderna). Menyn pratar med anv�ndaren, medan de andra metoderna utf�r tysta, logiska operationer och returnerar ett resultat.

**Don't Repeat Yourself:** N�r jag m�rkte att jag skrev samma kod p� flera st�llen, br�t jag ut den till �teranv�ndbara hj�lpmetoder. `FindVehicleIndexByRegNum`, `isRegNumValid`, `GetValidSpotNumber` och `IsGarageEmpty` �r centrala exempel som gjorde huvudkoden mycket renare, kortare och enklare att underh�lla.

**"Guard Clauses" / Tidig Retur:** I metoder som `MoveVehicle` anv�nde jag medvetet tidiga `return false`-satser f�r att validera indata. Ist�llet f�r att ha en stor `if`-sats f�r det lyckade fallet, kontrollerar jag f�r alla m�jliga fel i b�rjan av metoden. Detta g�r den huvudsakliga, "lyckade" kodv�gen plattare, mer l�ttl�st och f�rhindrar djupt n�stlade `if-else`-strukturer.

### 5. Hur jag skulle l�sa uppgiften n�sta g�ng (inom samma krav)
Jag anser att jag har optimerat koden s� gott det g�r med den kunskap jag har f�tt fr�n kursen hittills.

En annan sak jag skulle g�ra annorlunda �r hanteringen av textstr�ngar som `"BIL#"` och `"|"`. Jag har skrivit dessa direkt i koden p� flera st�llen. Jag har l�rt mig att detta kan vara riskabelt; om jag skulle beh�va �ndra hur en bil identifieras, m�ste jag hitta och �ndra det p� alla st�llen. �ven med bara grundl�ggande variabler skulle jag n�sta g�ng skapa n�gra variabler h�gst upp i programmet, till exempel `string carIdentifier = "BIL#";`. Genom att sedan anv�nda den variabeln �verallt i koden, skulle jag kunna g�ra framtida �ndringar p� ett enda, s�kert st�lle.

Slutligen har jag insett kraften i den `while(true)`-struktur vi byggde f�r menyvalen. I b�rjan var min felhantering enklare och mindre f�rl�tande. Att landa i designen d�r varje menyval har sin egen loop som "f�ngar" anv�ndaren tills uppgiften �r helt klar (eller avbruten) var en process. N�sta g�ng skulle jag bygga menyfl�det med den robusta loop-strukturen fr�n f�rsta b�rjan, eftersom det visade sig vara nyckeln till en bra anv�ndarupplevelse.

### 6. Slutsats hemuppgift
Projektet var en utm�rkt och praktisk �vning i grundl�ggande C#-programmering och, framf�r allt, i probleml�sning. Den st�rsta l�rdomen f�r mig personligen har varit resan fr�n att skriva en enkel, funktionell kod till att aktivt refaktorera den till en mer robust och v�lstrukturerad applikation.

### 7. Slutsats kurs
Kursen har hittills gett en v�ldigt solid grund i C# och det allm�nna "programmeringst�nket". Detta projekt har varit det perfekta tillf�llet att praktiskt till�mpa och verkligen f�rst� de teoretiska koncepten vi g�tt igenom, s�som metoder, parametrar, returtyper, arrayer, loopar och datatyper. Framf�rallt har projektet belyst den fundamentala skillnaden mellan kod som bara "fungerar" och kod som �r robust, underh�llbar och v�l-designad.

Att arbeta mot en specifik kravlista med ibland strikta tekniska begr�nsningar har ocks� varit en nyttig inblick i hur professionell mjukvaruutveckling kan se ut. Jag k�nner mig nu betydligt mer sj�lvs�ker i min f�rm�ga att ta ett komplext problem fr�n en specifikation till en fungerande och genomt�nkt applikation.