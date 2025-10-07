/*
	Prague Parking V1
	Tekniska krav:
	● All identifiering av fordon sker genom registeringsnummer
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
		1. Parkera ett fordon
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
				Console.Write("\nVilken fordonstyp vill du parkera? ((1) BIL, (2) MC)? (0) för att avbryta: ");
				string vehicleChoice = Console.ReadLine();
				if (vehicleChoice == "1" || vehicleChoice == "2")
				{
					Console.Write("\nAnge fordonets reg-nr (max 10 tecken): ");
					string regNum = Console.ReadLine().ToUpper();

					if (isRegNumValid(regNum))
					{
						bool wasAdded = AddVehicle(parkingGarage, vehicleChoice, regNum);

						if (wasAdded)
						{
							Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
							Console.ReadKey();
							break;
						}
					}
					else
					{
						Console.WriteLine("Felaktig inmatning av reg-nr.");
					}
				}
				else if (vehicleChoice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("Felaktig inmatning av fordonstyp. Ange 1 eller 2.");
				}
			}
		}

		else if (userChoice == "2")
		{
			while (true)
			{
				Console.Write("\nAnge reg-nr (max 10 tecken) på fordonet du vill ta bort. (0) för att avbryta: ");
				string vehicleToRemove = Console.ReadLine().ToUpper();

				if (vehicleToRemove == "0")
				{
					break;
				}

				if (isRegNumValid(vehicleToRemove))
				{
					bool wasRemoved = RemoveVehicle(vehicleToRemove);

					if (wasRemoved)
					{
						Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
						Console.ReadKey();
						break;
					}
				}
				else
				{
					Console.WriteLine("\nFelaktig inmatning av reg-nr. Försök igen.");
				}
			}
		}

		else if (userChoice == "3")
		{
			while (true)
			{
				Console.Write("\nAnge reg-nr (max 10 tecken) på fordonet som ska flyttas. (0) för att avbryta: ");
				string regNumToMove = Console.ReadLine().ToUpper();

				if (regNumToMove == "0")
				{
					break;
				}

				if (isRegNumValid(regNumToMove))
				{
					Console.Write("Ange ny plats (1-100) för fordonet. (0) för att avbryta: ");
					if (int.TryParse(Console.ReadLine(), out int toSpotNumber))
					{
						if (toSpotNumber == 0)
						{
							break;
						}

						bool success = MoveVehicle(regNumToMove, toSpotNumber);

						if (success)
						{
							Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
							Console.ReadKey();
							break;
						}
					}
					else
					{
						Console.WriteLine("\nFelaktig inmatning. Vänligen ange en siffra för platsnummer.");
					}
				}
				else
				{
					Console.WriteLine("\nOgiltigt format på reg-nr. Försök igen.");
				}
			}
		}

		else if (userChoice == "4")
		{
			while (true)
			{
				Console.Write("\nVill du söka med (1) Registreringsnummer, (2) Parkeringsplatsnummer. (0) för att avbryta: ");
				string searchChoice = Console.ReadLine();

				if (searchChoice == "0")
				{
					break;
				}
				else if (searchChoice == "1")
				{
					Console.Write("Ange reg-nr att söka efter (max 10 tecken): ");
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
						Console.WriteLine("\nFel: Du måste ange ett giltigt reg-nr.");
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
						Console.WriteLine("\nFelaktig inmatning. Vänligen ange ett platsnummer.");
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
				Console.Write("\nÄr du säker? ((1) JA, (0) NEJ): ");
				string exitChoice = Console.ReadLine();
				if (exitChoice == "1")
				{
					Console.WriteLine("\nHejdå!");
					return;
				}
				else if (exitChoice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("\nFelaktigt knappval. Välj 1 eller 0.");
				}
			}
		}
	}
}


//TODO: Systemet skall kunna ta emot ett fordon och tala om vilken parkeringsplats den skall köras till
bool AddVehicle(string[] garage, string vehicleChoice, string regNum)
{
	if (vehicleChoice == "2")
	{
		for (int index = 0; index < garage.Length; index++)
		{
			if (!String.IsNullOrEmpty(garage[index]) && garage[index].Contains("MC#") && !garage[index].Contains("|"))
			{
				garage[index] += "|MC#" + regNum;
				Console.WriteLine($"\nMotorcykeln {regNum} har dubbelparkerats på plats: {index + 1}.");
				return true;
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
			return true;
		}
		else if (vehicleChoice == "2")
		{
			garage[emptySpot] = "MC#" + regNum;
			Console.WriteLine($"\nMotorcykeln {regNum} har parkerats på plats: {emptySpot + 1}.");
			return true;
		}
		else
		{
			Console.WriteLine("\nOgiltig inmatning. Vänligen ange 1 eller 2");
			return false;
		}
	}
	else
	{
		Console.WriteLine("\nInga parkeringsplatser hittades.");
		return false;
	}
}

//TODO: Manuellt flytta ett fordon från en plats till en annan.
bool MoveVehicle(string regNum, int toSpot)
{
	int fromIndex = FindVehicleIndexByRegNum(regNum);

	if (fromIndex == -1)
	{
		Console.WriteLine($"\nFordonet med reg-nr '{regNum}' kunde inte hittas.");
		return false;
	}

	if (toSpot < 1 || toSpot > 100)
	{
		Console.WriteLine("\nOgiltig destinationsplats. Ange ett nummer mellan 1-100.");
		return false;
	}

	int toIndex = toSpot - 1;

	if (!String.IsNullOrEmpty(parkingGarage[toIndex]))
	{
		Console.WriteLine($"\nDestinationsplatsen {toSpot} är redan upptagen.");
		return false;
	}

	if (fromIndex == toIndex)
	{
		Console.WriteLine("\nFordonet står redan på denna plats. Ingen flytt utförd.");
		return false;
	}

	string spotContent = parkingGarage[fromIndex];
	string vehicleToMove = "";
	string vehicleToKeep = "";

	if (spotContent.Contains('|'))
	{
		string[] vehiclesInSpot = spotContent.Split('|');
		foreach (string vehicleData in vehiclesInSpot)
		{
			string currentRegNum = vehicleData.Split('#')[1];
			if (currentRegNum == regNum)
			{
				vehicleToMove = vehicleData;
			}
			else
			{
				vehicleToKeep = vehicleData;
			}
		}
	}
	else
	{
		vehicleToMove = spotContent;
	}


	parkingGarage[toIndex] = vehicleToMove;
	parkingGarage[fromIndex] = vehicleToKeep;

	Console.WriteLine($"\nFordonet {regNum} har flyttats från plats {fromIndex + 1} till plats {toSpot}.");
	return true;
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

			foreach (string vehicleData in vehiclesInSpot)
			{
				string currentRegNum = vehicleData.Split('#')[1];
				if (currentRegNum != regNum)
				{
					vehicleToKeep = vehicleData;
					break;
				}
			}

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
		Console.WriteLine($"\nEtt fordon med reg-nr '{regNum}' kunde inte hittas. Försök igen.");
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
			Console.WriteLine($"Plats {i + 1}: {parkingGarage[i]}");
		}
	}
	if (!foundAnyVehicles)
	{
		Console.WriteLine("Parkeringsplatsen är helt tom.");
	}
	Console.WriteLine("------------------------");
}


//TODO: Söka efter fordon.
void SearchByRegNum(string regNum)
{
	int findIndex = FindVehicleIndexByRegNum(regNum);

	if (findIndex != -1)
	{
		Console.WriteLine($"\nFordon med reg-nr '{regNum}' hittades på plats: {findIndex + 1}.");
	}
	else
	{
		Console.WriteLine($"\nFordon med reg-nr '{regNum}' kunde inte hittas.");
	}
}

int FindVehicleIndexByRegNum(string regNum)
{
	for (int i = 0; i < parkingGarage.Length; i++)
	{
		if (!String.IsNullOrEmpty(parkingGarage[i]) && parkingGarage[i].Contains(regNum))
		{
			return i;
		}
	}
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