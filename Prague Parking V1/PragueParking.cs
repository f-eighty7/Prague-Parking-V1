/*
	Prague Parking V1
	Tekniska krav:
	● All identifiering av fordon sker genom registreringsnummer
	● Registreringsnummer är alltid strängar med maxlängd 10 tecken. 
	● På parkeringsplatsen finns 100 parkeringsrutor 
	● En parkeringsruta kan innehålla  
		o 1 bil eller  
		o 1 mc eller  
		o 2mc eller
		o vara tom

	Parkeringsrutorna skall hanteras som en endimensionell vektor (array) av strängar. Vektorn skall hantera 
	100 element. Kundens personal är människor och förväntar sig att platserna numreras 1–100 i in- och 
	utmatningar i systemet. 
*/

Console.Title = "Prague Parking";

string[] parkingGarage = new string[100];

PragueParking();


//TODO: Systemet skall kunna ta emot ett fordon och tala om vilken parkeringsplats den skall köras till
void AddVehicle()
{
	Console.WriteLine("Vilken fordonstyp vill du parkera? (1 för Bil, 2 för MC)");
	string vehicleChoice = Console.ReadLine();
	Console.WriteLine("Ange fordonets registreringsnummer: ");
	string plateNumber = Console.ReadLine();
	string vehicleType = "";

	if (vehicleChoice == "2")
	{
		for (int index= 0; index < parkingGarage.Length; index++)
		{
			if (!String.IsNullOrEmpty(parkingGarage[index]) &&
				parkingGarage[index].Contains("MC#") &&
				!parkingGarage[index].Contains("|"))
			{
				parkingGarage[index] += "|MC#" + plateNumber;
				Console.WriteLine($"\nMotorcykeln {plateNumber} har dubbelparkerats på plats: {i + 1}.");

				return;
			}
		}
	}

	int parkingSpot = -1;
	for (int index = 0; index < parkingGarage.Length; index++)
	{
		if (String.IsNullOrEmpty(parkingGarage[index]))
		{
			parkingSpot = index;
			break;
		}
	}

	if (parkingSpot != -1)
	{
		if (vehicleChoice == "1")
		{
			vehicleType = "BIL";
			parkingGarage[parkingSpot] = vehicleType + "#" + plateNumber;
			Console.WriteLine($"\nBIL#{plateNumber} har parkerats på plats: {parkingSpot + 1}.");
		}
		else if (vehicleChoice == "2")
		{
			vehicleType = "MC";
			parkingGarage[parkingSpot] = vehicleType + "#" + plateNumber;
			Console.WriteLine($"\nMC#{plateNumber} har parkerats på plats: {parkingSpot + 1}.");
		}
		else Console.WriteLine("Ogiltigt inmatning. Vänligen ange 1 eller 2");
	}
	else
	{
		Console.WriteLine("Inga parkeringsplatser hittades.");
	}
}

//TODO: Manuellt flytta ett fordon från en plats till en annan.
//TODO: Ta bort fordon vid uthämtning.
//TODO: Söka efter fordon.


//TODO: Kunden önskar en textbaserad meny
void PragueParking()
{
	//Färgen är baserad på Pragues Flagga
	Console.BackgroundColor = ConsoleColor.Red;
	Console.Clear();
	Console.ForegroundColor = ConsoleColor.Yellow;

	Console.WriteLine("""

		Välj ett alternativ: 
		----------------------
		1. Parkera ett fodon
		2. Ta bort fordon
		3. Flytta fordon
		4. Sök efter fordon.

		""");

	string userChoice = Console.ReadLine();

	if (userChoice == "1")
	{
		AddVehicle();
	}
}

