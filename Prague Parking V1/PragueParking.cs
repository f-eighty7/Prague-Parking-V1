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


//TODO: Kunden önskar en textbaserad meny
void PragueParking()
{
	//Färgen är baserad på Pragues Flagga
	Console.BackgroundColor = ConsoleColor.Red;
	Console.Clear();
	Console.ForegroundColor = ConsoleColor.Yellow;

	while (true)
	{
		Console.Clear();
		Console.Write("""

		Välj ett alternativ: 
		----------------------
		1. Parkera ett fodon
		2. Ta bort fordon
		3. Flytta fordon
		4. Sök efter fordon
		5. Visa parkerade fordon
		6. Logga ut

		Val: 
		""");

		string userChoice = Console.ReadLine();

		if (userChoice == "1")
		{
			while (true)
			{
				Console.Write("\nVilken fordonstyp vill du parkera? ((1) BIL, (2) MC) eller (0) för att backa: ");
				string vehicleChoice = Console.ReadLine();
				if (vehicleChoice == "1" || vehicleChoice == "2")
				{
					Console.Write("\nAnge fordonets registreringsnummer: ".ToUpper());
					string regNum = Console.ReadLine();

					if (isRegNumValid(regNum))
					{
						AddVehicle(parkingGarage, vehicleChoice, regNum);
						Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
						Console.ReadKey();
						break;
					}
					else
					{
						Console.WriteLine("Felaktig inmatning av registeringsnummer.");
					}
				}
				else if (vehicleChoice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("Felakting inmatning av fordonstyp. Ange 1 eller 2");
				}
			}
		}

		else if (userChoice == "2")
		{
			while (true)
			{
				Console.Write("\nAnge registreringsnummer på fordonet du vill ta bort eller (0) backa: ");
				string regNumToRemove = Console.ReadLine().ToUpper();

				if (isRegNumValid(regNumToRemove))
				{
					if (regNumToRemove == "0")
					{
						break;
					}
					else if (!String.IsNullOrEmpty(regNumToRemove))
					{
						bool wasRemoved = RemoveVehicle(regNumToRemove);

						if (wasRemoved)
						{
							Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
							Console.ReadKey();
							break;
						}
					}
				}
				else
				{
					Console.WriteLine("\nFelaktig inmatning av registeringsnummer. Försök igen.");
				}
			}
		}

		else if (userChoice == "3")
		{
			Console.WriteLine("Ange registeringsnummret på fordonet du vill flytta: ");
			Console.Write("\nRegisteringsnummret får max ha 10 Tecken: );");
			string vehicleToMove = Console.ReadLine().ToUpper();

			if (isRegNumValid(vehicleToMove))
			{
				MoveVehicle(vehicleToMove);
			}
		}

		else if (userChoice == "4")
		{
			while (true)
			{
				Console.Write("\nVill du söker med (1) Registeringsnummer, (2) Parkeringsplatsnummer eller (0) backa: ");
				string searchChoice = Console.ReadLine();

				if (searchChoice == "0")
				{
					break;
				}
				else if (searchChoice == "1")
				{
					Console.WriteLine("Ange registreringsnummer att söka efter");
					Console.Write("\nRegisteringsnummret får max ha 10 Tecken: ");
					string regNumToSearch = Console.ReadLine().ToUpper();

					if (isRegNumValid(regNumToSearch))
					{

						SearchByRegNum(regNumToSearch);
						Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
						Console.ReadKey();
						break;
					}
					else
					{
						Console.WriteLine("\nFel: Du måste ange ett registreringsnummer.");
					}
				}

				else if (searchChoice == "2")
				{
					Console.Write("\nAnge platsnummer att visa (1-100): ");
					if (int.TryParse(Console.ReadLine(), out int spotNumToShow))
					{
						ShowSpotContent(spotNumToShow);
						Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
						Console.ReadKey();
						break;
					}
					else
					{
						Console.WriteLine("\nFelaktig inmatnig. Vänligen ange en siffra.");
					}
				}
				else
				{
					Console.WriteLine("\nOgiltigt val. Vänligen välj 1 eller 2.");
				}
				}
			}

		else if (userChoice == "5")
		{
			UsedParkingSpots();
			Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
			Console.ReadKey();
		}

		else if (userChoice == "6")
		{
			while (true)
			{
				Console.WriteLine("\nÄr du säker? ((1) JA och (0) NEJ)");
				userChoice = Console.ReadLine();
				if (userChoice == "1")
				{
					Console.WriteLine("\nNHejdå!");
					return;
				}
				else if (userChoice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("\nFelaktig knappval. Välj 1 eller 0.");
				}
			}
		}
	}
}


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
		else Console.WriteLine("\nOgiltigt inmatning. Vänligen ange 1 eller 2");
	}
	else
	{
		Console.WriteLine("\nInga parkeringsplatser hittades.");
	}
}

//TODO: Manuellt flytta ett fordon från en plats till en annan.
void MoveVehicle(string regNum)
{
	
}

//TODO: Ta bort fordon vid uthämtning.
bool RemoveVehicle(string regNum)
{
	int findIndex = FindVehicleIndexByRegNum(regNum);

	if (findIndex != -1)
	{
		string spotContent = parkingGarage[findIndex];

		if (spotContent.Contains('|'))
		{
			string[] vehiclesInSpot = spotContent.Split('|');

			string vehicleToKeep = "";

			// Loopa igenom de två fordonen på platsen.
			foreach (string vehicleData in vehiclesInSpot)
			{
				// 1. Dela upp fordonets data, t.ex. "MC#ABC123" blir till ["MC", "ABC123"]
				string[] parts = vehicleData.Split('#');
				// Hämta det rena registreringsnumret (som ligger på index 1)
				string currentRegNum = parts[1];

				// 2. Gör en EXAKT jämförelse.
				// Fråga: Är detta fordonets regnr INTE samma som det vi vill ta bort?
				if (currentRegNum != regNum)
				{
					// Om ja, då är det detta fordon vi vill SPARA.
					vehicleToKeep = vehicleData;
					break; // Vi kan sluta loopen, vi har hittat den vi ska behålla.
				}
			}

			// Uppdatera parkeringsplatsen med enbart det kvarvarande fordonet.
			parkingGarage[findIndex] = vehicleToKeep;
			Console.WriteLine($"\nEn motorcykel har tagits bort från den delade platsen {findIndex + 1}.");
			return true;
		}
		else
		{
			parkingGarage[findIndex] = null;
			Console.WriteLine($"\nFordonet på plats {findIndex + 1} har tagits bort.");
			return true;
		}
	}
	else
	{
		Console.WriteLine($"\nEtt fordon med registreringsnummer '{regNum}' kunde inte hittas.");
		return false;
	}
}


//TODO: Visa vilka fordon som är parkerade och vart
void UsedParkingSpots()
{
	Console.WriteLine("\n--- Parkerade Fordon ---");

	bool foundAnyVehicles = false;

	for (int i = 0; i < parkingGarage.Length; i++)
	{
		if (!String.IsNullOrEmpty(parkingGarage[i]))
		{
			foundAnyVehicles = true;
			Console.WriteLine($"\nPlats {i + 1}: {parkingGarage[i]}");
		}
	}
	if (!foundAnyVehicles)
	{
		Console.WriteLine("Parkeringsplatsen är helt tom.");
	}
	Console.WriteLine("------------------------\n");
}


//TODO: Söka efter fordon.
void SearchByRegNum(string regNum)
{
	int findIndex = FindVehicleIndexByRegNum(regNum);

	if (findIndex != -1)
	{
		Console.WriteLine($"\nFordon med registreringsnummer '{regNum}' hittades på plats: {findIndex + 1}.");
	}
	else
	{
		Console.WriteLine($"\nFordon med registreringsnummer '{regNum}' kunde inte hittas.");
	}
}

int FindVehicleIndexByRegNum(string regNum)
{

	for (int i = 0; i < parkingGarage.Length; i++)
	{
		if (!String.IsNullOrEmpty(parkingGarage[i]))
		{
			if (parkingGarage[i].Contains(regNum))
			{
				return i;
			}
		}
	}
	// 3. "HITTADES INTE": Om loopen blir klar utan att någonsin ha kört 'return i',
	// betyder det att fordonet inte hittades. Då returnerar vi -1.
	return -1;
}

void ShowSpotContent(int spotNumber)
{
	if (spotNumber >= 1 && spotNumber <= 100)
	{
		int index = spotNumber - 1;

		if (string.IsNullOrEmpty(parkingGarage[index]))
		{
			Console.WriteLine($"\nPlats {spotNumber} är tom.");
		}
		else
		{
			Console.WriteLine($"\nPå plats {spotNumber} står: {parkingGarage[index]}");
		}
	}
	else
	{
		Console.WriteLine($"\nOgiltigt platsnummer. Ange ett nummer mellan 1 och {parkingGarage.Length}.");
	}
}

bool isRegNumValid(string regNum)
{
	return !String.IsNullOrEmpty(regNum) && regNum.Length <= 10 && !regNum.Contains(" ");
}