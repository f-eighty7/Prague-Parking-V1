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

using System;
using System.Linq.Expressions;

Console.Title = "Prague Parking";

string[] parkingGarage = new string[100];

PragueParking();


//TODO: Systemet skall kunna ta emot ett fordon och tala om vilken parkeringsplats den skall köras till
void AddVehicle(string[] garage, string vehicleChoice, string regNum)
{
	if (vehicleChoice == "2")
	{
		for (int index= 0; index < garage.Length; index++)
		{
			if (!String.IsNullOrEmpty(garage[index]) &&
				garage[index].Contains("MC#") &&
				!garage[index].Contains("|"))
			{
				garage[index] += "|MC#" + regNum;
				Console.WriteLine($"\nMotorcykeln {regNum} har dubbelparkerats på plats: {index + 1}.");

				return;
			}
		}
	}

	int emptySpot = -1;
	for (int index = 0; index < garage.Length; index++)
	{
		if (String.IsNullOrEmpty(garage[index]))
		{
			emptySpot = index;
			break;
		}
	}

	if (emptySpot != -1)
	{
		if (vehicleChoice == "1")
		{
			garage[emptySpot] = "BIL#" + regNum;
			Console.WriteLine($"\nBilen {regNum} har parkerats på plats: {emptySpot + 1}.");
		}
		else if (vehicleChoice == "2")
		{
			garage[emptySpot] = "MC#" + regNum;
			Console.WriteLine($"\nMotorcykeln {regNum} har parkerats på plats: {emptySpot + 1}.");
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
// Behöver kunna söka upp med regnummer
// Behöver kunna returnera reg nr och plats + 1

void SearchVehicle(string regNum)
{
	for (int i = 0; i < parkingGarage.Length; i++)
	{
		string spotContent = parkingGarage[i];

		if (String.IsNullOrEmpty(spotContent))
		{
			continue;
		}

		string[] vehiclesInSpot = spotContent.Split('|');

		foreach (string vehicleData in vehiclesInSpot)
		{
			if (vehicleData.Contains(regNum))
			{
				Console.WriteLine($"\nFordon med registreringsnummer \"{regNum}\" hittades på plats: {i + 1}.");

				return;
				
			}

		}
	}
	Console.WriteLine($"\nFordon med registreringsnummer \"{regNum}\" kunde inte hittas.");
}

//TODO: Kunden önskar en textbaserad meny
void PragueParking()
{
	//Färgen är baserad på Pragues Flagga
	Console.BackgroundColor = ConsoleColor.Red;
	Console.Clear();
	Console.ForegroundColor = ConsoleColor.Yellow;

	while (true)
	{
		Console.Write("""

		Välj ett alternativ: 
		----------------------
		1. Parkera ett fodon
		2. Ta bort fordon
		3. Flytta fordon
		4. Sök efter fordon
		5. Logga ut

		Val: 
		""");

		string userChoice = Console.ReadLine();

		if (userChoice == "1")
		{
			Console.Write("\nVilken fordonstyp vill du parkera? (1 för BIL, 2 för MC): ");
			string vehicleChoice = Console.ReadLine();
			Console.Write("Ange fordonets registreringsnummer: ");
			string regNum = Console.ReadLine();

			AddVehicle(parkingGarage, vehicleChoice, regNum);
		}
		else if (userChoice == "4")
		{
			Console.Write("Ange fordonets registerings nummer: ");
			string regNumToSearch = Console.ReadLine();
			SearchVehicle(regNumToSearch);
			
		}
		else if (userChoice == "5")
		{
			while (true)
			{
				Console.WriteLine("Är du säker? (1 för JA och 2 för NEJ)");
				userChoice = Console.ReadLine();
				if (userChoice == "1")
				{
					Console.WriteLine("Hejdå!");
					return;
				}
				else if (userChoice == "2")
				{
					break;
				}
				else
				{
					Console.WriteLine("Felaktig knappval. Välj 1 eller 2.");
				}
			}
		}
	}
}