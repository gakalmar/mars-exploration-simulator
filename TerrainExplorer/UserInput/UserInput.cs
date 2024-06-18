using TerrainGenerator.Calculators.Model;

namespace TerrainExplorer.UserInput;

public class UserInputProvider
{
    private readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;
    
    public string mapFile;
    public Coordinate landingSpot;
    public IEnumerable<string> searchItems = new[] { "*", "%" };
    public int simulationSteps;
    public int sight;

    public UserInputProvider()
    {
        WelcomeMessage();
        mapFile = SelectMapFile();
        landingSpot = GetRandomCoordinate();
        simulationSteps = GetSimSteps();
        sight = GetRoverSight();
    }

    private void WelcomeMessage()
    {
        Console.WriteLine($"\nWelcome to MARS exploration - Sprint 2!\n" +
                          $"\n" +
                          $"In your mission you will be sending your Rover (aka. 'Rover DeNiro') to a distant planet recently shortlisted to be the potential new home for Humanity.\n" +
                          $"Rovi's task is to perform it's experiments and search for any resources that could make the planet habitalbe, and later colonized." +
                          $"\n");
    }

    private string SelectMapFile()
    {
        Console.WriteLine("Please select a planet to discover:");
        List<string> maps = Directory.GetFiles(WorkDir.Replace("\\bin\\Debug\\net6.0", "\\Resources")).ToList();
        for (int i = 0; i < maps.Count; i++)
        {
            Console.WriteLine($"[{i+1}] {maps[i].Split("\\").Last()}");
        }

        int selected = AskingForInputCycle(maps.Count);
        return $"{maps[selected-1]}";
    }
    
    private Coordinate GetRandomCoordinate()
    {
        return new Coordinate(6, 6);
    }

    private int GetSimSteps()
    {
        Console.WriteLine("How many simulation steps would you like to use? (up to 10000)");
        return AskingForInputCycle(10000);
    }
    
    private int GetRoverSight()
    {
        Console.WriteLine("Please provide a number for the Rover's sight: (1 to 5)");
        return AskingForInputCycle(5);
    }
    
    private static int AskingForInputCycle(int validOptions)
    {
        // Console.WriteLine(inputSentence);
        int inputNumber = 0;
        bool valid = false;
        while (!valid)
        {
            string inputString = Console.ReadLine();
            if (int.TryParse(inputString, out int parsedInt) && parsedInt > 0 && parsedInt <= validOptions)
            {
                valid = true;
                inputNumber = parsedInt;
            }
            else
            {
                Console.WriteLine("Invalid input, please enter a valid positive integer number!");
            }
        }

        return inputNumber;
    }
}