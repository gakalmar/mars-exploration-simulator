using TerrainExplorer.Configuration;
using TerrainExplorer.Exploration;
using TerrainExplorer.MapLoader;
using TerrainExplorer.MarsRover;
using TerrainExplorer.Repository;
using TerrainExplorer.Simulation.SimulationStepFactory;
using TerrainExplorer.UserInput;
using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;

namespace TerrainGenerator.MapExplorer;

class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        UserInputProvider userInput = new UserInputProvider();
        
        string mapFile = userInput.mapFile;
        Coordinate landingSpot = userInput.landingSpot;
        IEnumerable<string> searchItems = userInput.searchItems;
        int simulationSteps = userInput.simulationSteps;
        int sight = userInput.sight;

        ExplorationConfiguration initialExplorationConfiguration =
            new ExplorationConfiguration(mapFile, landingSpot, searchItems, simulationSteps);

        ICoordinateCalculator coordinateCalculator = new CoordinateCalculator();
        IConfigurationValidator configurationValidator = new ConfigurationValidator(coordinateCalculator);
        IMapLoader mapLoader = new MapLoader();
        IRoverDeployer roverDeployer = new RoverDeployer(sight);
        
        IExplorationSimulator explorationSimulator = new ExplorationSimulator(initialExplorationConfiguration,
            configurationValidator, mapLoader, roverDeployer, coordinateCalculator);
        
        explorationSimulator.GenerateContext();

        List<SimulationStepFactoryBase> simulationStepFactories = new List<SimulationStepFactoryBase>()
        {
            new MovementFactoryBase(),
            new ScanningFactoryBase(),
            new AnalysisFactoryBase(),
            new LogFactoryBase(),
            new StepIncrementFactoryBase()
        };
        
        while (explorationSimulator.SimContext.outcome == ExplorationOutcome.NotYetDetermined)
        {
            explorationSimulator.Run(simulationStepFactories);
        }
        
        string workDir = AppDomain.CurrentDomain.BaseDirectory;
        var dbFile = $"{workDir}Repository\\DB\\mars_exploration.db";

        IExplorationRepository repository = new ExplorationRepository(dbFile);

        int steps = explorationSimulator.SimContext.StepsUsed;
        int water = explorationSimulator.SimContext.rover.Discoveries.Where(kvp => kvp.Value == "*").ToList().Count;
        int minerals = explorationSimulator.SimContext.rover.Discoveries.Where(kvp => kvp.Value == "%").ToList().Count;
        ExplorationOutcome outcome = explorationSimulator.SimContext.outcome;
        repository.Add(steps, water, minerals, outcome);

    }
}
