# Prague Parking V1.1 🚗

This is a text-based console application written in C# to manage a 100-space parking garage. The application is designed to be a simple and robust tool for parking staff, simulating the core functionalities of a parking management system.

The project was developed as part of a programming course with specific technical requirements, the main challenge being to handle all parking data within a single-dimensional `string` array.

## About the Application

The system can handle two types of vehicles: Cars (CAR) and Motorcycles (MC). The rules are simple:
* A **Car** occupies one full parking spot.
* Two **Motorcycles** can share a single parking spot.

All data is stored and managed in real-time during execution by parsing and manipulating strings in the format `VEHICLE#REG_NUMBER`.

## Features

The application features a menu-driven interface and includes the following functionalities:

#### Core Features
* **Park Vehicle**: Add a car or motorcycle to the first available spot. The system handles the logic for placing a second MC in a spot that already contains one.
* **Check-out Vehicle**: Remove a vehicle from the garage using its registration number.
* **Move Vehicle**: Move an existing vehicle from one parking spot to another.
* **Find Vehicle**: Quickly locate which spot a specific vehicle is parked in.

#### Advanced Features
* **✅ Visual Overview**: Get a color-coded overview of the entire parking garage presented as a 5x20 grid.
    * <span style="color:green">**GREEN**</span>: Completely empty spot.
    * <span style="color:yellow">**YELLOW**</span>: Spot with one motorcycle (room for one more).
    * <span style="color:red">**RED**</span>: Fully occupied spot (one car or two motorcycles).
* **🔒 Robust Input Validation**: All user input is validated to prevent errors and crashes. If a user enters invalid data (e.g., a letter instead of a number), they receive a clear error message and another chance to enter it correctly without being sent back to the main menu.

## Getting Started

To run and test the application on your own computer, follow the instructions below.

### Prerequisites
* For the **Visual Studio** method: [Visual Studio 2022](https://visualstudio.microsoft.com/) with the ".NET desktop development" workload installed.
* For the **Terminal** method: [.NET 8 SDK](https://dotnet.microsoft.com/download) (or later).

---

### Option 1: Using Visual Studio

This method is great for exploring the code, debugging, and seeing how the project is structured.

1.  **Clone or download the repository**
    Use `git clone` as described in Option 2, or download the project as a ZIP file from the repository page and extract it to a folder on your computer.

2.  **Open the project in Visual Studio**
    * Launch Visual Studio.
    * On the start screen, select **"Open a project or solution"**.
    * Navigate to the folder where you cloned or extracted the project.
    * Select the solution file (the file ending with `.sln`) and click **"Open"**.

3.  **Run the application**
    * Once the project is loaded, simply press the **Start button** (the green play icon ▶️) in the top toolbar.
    * Alternatively, you can press the `F5` key.

Visual Studio will build the project, and a new console window will appear, running the application.

---

### Option 2: Using the Terminal (.NET CLI)

This method is ideal for a quick and simple way to run the application from your command line.

1.  **Clone the repository**
    Open a terminal (like Command Prompt, PowerShell, or Bash) and clone this repository to your local machine.
    ```sh
    git clone [URL-to-your-repository]
    ```

2.  **Navigate to the project directory**
    ```sh
    cd [name-of-repository-folder]
    ```

3.  **Run the application**
    Use the .NET CLI to build and run the project.
    ```sh
    dotnet run
    ```
The application will now start in your terminal, and you can interact with the parking system through its menu.

## Author
Ahin
