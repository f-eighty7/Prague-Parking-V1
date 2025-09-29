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

string[] vehicles = new string[100];
AddVehicle();
//PragueParking();

//TODO: Systemet skall kunna ta emot ett fordon och tala om vilken parkeringsplats den skall köras till

void AddVehicle()
{
	Console.WriteLine("Vilken fordonstyp vill du parkera? (1 för Bil, 2 för MC)");
	string  vehicle= Console.ReadLine();
	Console.WriteLine("Ange fordonets registreringsnummer: ");
	string plateNumber = Console.ReadLine();	
	if (vehicle == 1)
	{
		
	}
	Console.WriteLine($"\nFordonet med nummre {vehicles[0]} har parkerats.");
}

//TODO: Manuellt flytta ett fordon från en plats till en annan.
//TODO: Ta bort fordon vid uthämtning.
//TODO: Söka efter fordon.


//TODO: Kunden önskar en textbaserad meny
//void PragueParking()
//{
//	//Färgen är baserad på Pragues Flagga
//	Console.BackgroundColor = ConsoleColor.Red;
//	Console.Clear();
//	Console.ForegroundColor = ConsoleColor.Yellow;
	
//	Console.WriteLine("\nVälj ett alternativ:");
//	Console.WriteLine("----------------------");
//	Console.WriteLine("1. Parkera ett fodon");
//	Console.WriteLine("2. Ta bort fordon");
//	Console.WriteLine("3. Flytta fordon");
//	Console.WriteLine("4. Sök efter fordon.");
//}

