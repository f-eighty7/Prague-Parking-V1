/*
	Lägg till minst två av nedanstående funktioner. 
		1. Visualisering av vad som finns på parkeringsplatsen. Dvs vilka platser är lediga, halvfulla och 
		fyllda så att personalen får en god överblick. Pluspoäng för kreativa lösningar och färgmarkering. 
		Ni får lov att göra mer än en rapport tex alla motorcyklar, alla bilar, alla tomma platser etc. En 
		lista som visar vilka regnr och fordonstyper som finns på vilken plats är minimum.

		3. Säkra upp användarinput - tex be om nya data om användare matar in felaktiga data när 
		registreringsnummer och eventuellt platsnummer skall anges. P-platsnummer måste vara tal i 
		intervallet 1–100, registreringsnummer får inte innehålla mellanslag o. dyl., samt måste hantera 
		västeuropeiska alfabeten samt siffror. Tex Ü, Å, Ä, Ö etc. Titta i Wikipedia: 
		https://sv.wikipedia.org/wiki/Registreringsskylt
*/

using System;

Console.Title = "Prague Parking V1.1";

string[] parkingGarage = new string[100];

PragueParking();

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
			Console.Clear();
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
						if (AddVehicle(parkingGarage, vehicleChoice, regNum))
						{
							Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
							Console.ReadKey();
							break;
						}
					}
					else
					{
						Console.WriteLine("\nFelaktig inmatning av reg-nr.");
					}
				}
				else if (vehicleChoice == "0")
				{
					break;
				}
				else
				{
					Console.WriteLine("\nFelaktig inmatning av fordonstyp. Ange 1 eller 2.");
				}
			}
		}

		else if (userChoice == "2")
		{
			Console.Clear();
			if (IsGarageEmpty())
			{
				Console.WriteLine("\nParkeringshuset är helt tomt. Det finns inget att ta bort.");
				Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
				Console.ReadKey();
			}
			else
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
						if (RemoveVehicle(vehicleToRemove))
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
		}

		else if (userChoice == "3")
		{
			Console.Clear();
			if (IsGarageEmpty())
			{
				Console.WriteLine("\nParkeringshuset är helt tomt. Det finns inget att flytta.");
				Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
				Console.ReadKey();
			}
			else
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
						int toSpotNumber = GetValidSpotNumber("\nAnge ny plats (1-100) för fordonet. (0) för att avbryta: ");

						if (toSpotNumber == 0)
						{
							break;
						}

						if (MoveVehicle(regNumToMove, toSpotNumber))
						{
							Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
							Console.ReadKey();
							break;
						}
					}
					else
					{
						Console.WriteLine("\nOgiltigt format på reg-nr. Försök igen.");
					}
				}
			}
		}

		else if (userChoice == "4")
		{
			Console.Clear();

			if (IsGarageEmpty())
			{
				Console.WriteLine("\nParkeringshuset är helt tomt. Det finns inget att söka efter.");
				Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
				Console.ReadKey();
			}
			else
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
						Console.Write("\nAnge reg-nr att söka efter (max 10 tecken): ");
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
						int spotNumToShow = GetValidSpotNumber("\nAnge platsnummer att visa (1-100). (0) för att avbryta: ");

						if (spotNumToShow == 0)
						{
							break;
						}

						ShowSpotContent(spotNumToShow);
						Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
						Console.ReadKey();
						break;
					}
					else
					{
						Console.WriteLine("\nOgiltigt val. Vänligen välj 1 eller 2.");
					}
				}
			}
		}

		else if (userChoice == "5")
		{
			Console.Clear();
			VisualizeParkingLot();
			Console.WriteLine("\nTryck valfri tangent för att återgå till menyn...");
			Console.ReadKey();
		}

		else if (userChoice == "6")
		{
			Console.Clear();
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

//TODO: Skapa en funktion som säkrar inmatning av p-plats.
int GetValidSpotNumber(string prompt)
{
	while (true)
	{
		Console.Write(prompt);
		if (int.TryParse(Console.ReadLine(), out int spotNumber))
		{
			if (spotNumber == 0)
			{
				return 0;
			}
			if (spotNumber >= 1 && spotNumber <= 100)
			{
				return spotNumber;
			}
			else
			{
				Console.WriteLine("\nOgiltigt platsnummer. Ange ett nummer mellan 1-100.");
			}
		}
		else
		{
			Console.WriteLine("\nFelaktig inmatning. Vänligen ange en siffra.");
		}
	}
}

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
bool MoveVehicle(string regNum, int toSpot)
{
	int fromIndex = FindVehicleIndexByRegNum(regNum);

	if (fromIndex == -1)
	{
		Console.WriteLine($"\nFordonet med reg-nr '{regNum}' kunde inte hittas.");
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

//TODO: Visualisering av vad som finns på parkeringsplatsen
void VisualizeParkingLot()
{
	Console.Clear();
	Console.WriteLine("\n--- Visuell Översikt - Parkeringshuset ---");
	Console.WriteLine("------------------------------------------");

	for (int i = 0; i < parkingGarage.Length; i++)
	{
		string spotContent = parkingGarage[i];

		if (!String.IsNullOrEmpty(spotContent) && spotContent.Contains("MC#") && !spotContent.Contains('|'))
		{
			Console.BackgroundColor = ConsoleColor.DarkYellow;
			Console.ForegroundColor = ConsoleColor.Black;
		}
		else if (!String.IsNullOrEmpty(spotContent))
		{
			Console.BackgroundColor = ConsoleColor.DarkRed;
			Console.ForegroundColor = ConsoleColor.White;
		}
		else
		{
			Console.BackgroundColor = ConsoleColor.Green;
			Console.ForegroundColor = ConsoleColor.Black;
		}

		Console.Write($"[{i + 1:D2}:{spotContent}]");

		Console.BackgroundColor = ConsoleColor.Red;
		Console.ForegroundColor = ConsoleColor.Yellow;

		Console.Write(" ");

		if ((i + 1) % 5 == 0)
		{
			Console.WriteLine();
			Console.WriteLine();
		}
	}

	Console.BackgroundColor = ConsoleColor.Red;
	Console.ForegroundColor = ConsoleColor.Yellow;

	Console.WriteLine("\n--------------------------------------------------------------------------------");
	Console.WriteLine("FÖRKLARING:");
	Console.BackgroundColor = ConsoleColor.Green;
	Console.ForegroundColor = ConsoleColor.Black;
	Console.Write("[XX:---]      ");
	Console.ResetColor();
	Console.BackgroundColor = ConsoleColor.Red;
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.WriteLine(" = Ledig Plats");

	Console.BackgroundColor = ConsoleColor.DarkYellow;
	Console.ForegroundColor = ConsoleColor.Black;
	Console.Write("[XX:REGNUM]  ");
	Console.ResetColor();
	Console.BackgroundColor = ConsoleColor.Red;
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.WriteLine(" = Halvfull Plats (1 MC)");

	Console.BackgroundColor = ConsoleColor.DarkRed;
	Console.ForegroundColor = ConsoleColor.White;
	Console.Write("[XX:REGNUM]  ");
	Console.ResetColor();
	Console.BackgroundColor = ConsoleColor.Red;
	Console.ForegroundColor = ConsoleColor.Yellow;
	Console.WriteLine(" = Full Plats (Bil eller 2 MC)");
	Console.WriteLine("--------------------------------------------------------------------------------");
}

//void UsedParkingSpots()
//{
//	Console.Clear();
//	Console.WriteLine("\n--- Parkerade Fordon ---");

//	bool foundAnyVehicles = false;

//	for (int i = 0; i < parkingGarage.Length; i++)
//	{
//		if (!String.IsNullOrEmpty(parkingGarage[i]))
//		{
//			foundAnyVehicles = true;
//			Console.WriteLine($"Plats {i + 1}: {parkingGarage[i]}");
//		}
//	}
//	if (!foundAnyVehicles)
//	{
//		Console.WriteLine("Parkeringsplatsen är helt tom.");
//	}
//	Console.WriteLine("------------------------");
//}

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

// Validerar ett registreringsnummer. Inte tomt, max 10 tecken, inga mellanslag, och endast bokstäver eller siffror.
bool isRegNumValid(string regNum)
{
	if (String.IsNullOrEmpty(regNum) || regNum.Length > 10 || regNum.Contains(" "))
	{
		return false;
	}

	foreach (char c in regNum)
	{
		if (!char.IsLetterOrDigit(c))
		{
			return false;
		}
	}

	return true;
}

// TODO: Skapa en funktion som direkt säger att p-huset är tom när man vill söka, flytta, eller ta bort
bool IsGarageEmpty()
{
	foreach (string spot in parkingGarage)
	{
		if (!String.IsNullOrEmpty(spot))
		{
			return false;
		}
	}
	return true;
}
