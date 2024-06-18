using TerrainExplorer.Configuration;
using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;
using TerrainGenerator.MapElements.Model;

namespace TerrainExplorer.Exploration;

public class ExplorationSimulator : IExplorationSimulator
{
    private ExplorationConfiguration _config;
    private readonly IConfigurationValidator _configValidator;
    private readonly IMapLoader _mapLoader;
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly IRoverDeployer _roverDeployer;
    private Map _map;
    private Random _random = new Random();
    private List<SimulationStepBase> _simulationSteps = new List<SimulationStepBase>();

    public SimulationContext SimContext { get; private set; }
    public Rover Rover { get; private set; }

    public ExplorationSimulator(ExplorationConfiguration config, IConfigurationValidator configValidator, IMapLoader mapLoader, IRoverDeployer roverDeployer, ICoordinateCalculator coordinateCalculator)
    {
        _config = config;
        _configValidator = configValidator;
        _mapLoader = mapLoader;
        _roverDeployer = roverDeployer;
        _coordinateCalculator = coordinateCalculator;
    }

    public void GenerateContext()
    {
        while (!ValidateContext())
        {
            string fileName = _config.FileName;
            IEnumerable<string> searchItems = _config.SearchItems;
            int steps = _config.SimulationSteps;
            Coordinate newCoord = _coordinateCalculator.GetRandomCoordinate(_map.Dimension);

            _config = new ExplorationConfiguration(fileName, newCoord, searchItems, steps);
        }

        Coordinate roverCoordinate = GenerateRoverCoordinate(_config.LandingSpot);
        
        Rover = _roverDeployer.Deploy(roverCoordinate);
        SimContext = new SimulationContext(Rover, _map, _config.SearchItems, _config.SimulationSteps, _config.LandingSpot);
    }

    public void Run(List<SimulationStepFactoryBase> simulationStepFactories)
    {
        InitialRoverSight();

        GetSteps(simulationStepFactories);
        while (SimContext.outcome == ExplorationOutcome.NotYetDetermined)
        {
            foreach (var step in _simulationSteps)
            {
                step.Execute();
            }
        }

        _simulationSteps[0].Execute();
    }

    private bool ValidateContext()
    {
        _map = _mapLoader.Load(_config.FileName);
        return _configValidator.Validate(_config, _map);
    }

    private Coordinate GenerateRoverCoordinate(Coordinate landingCoordinate)
    {
        List<Coordinate> adjacentCoordinates = _coordinateCalculator.GetAdjacentCoordinates(landingCoordinate, _map.Dimension)
            .Where(c => _map.Representation[c.X, c.Y] == " ").ToList();
        
        return adjacentCoordinates[_random.Next(adjacentCoordinates.Count)];
    }
    
    private void GetSteps(List<SimulationStepFactoryBase> simulationStepFactories )
    {
        foreach (var factory in simulationStepFactories)
        {
            _simulationSteps.Add(factory.Create(SimContext, _coordinateCalculator)); 
        }
    }
    
    private void InitialRoverSight()
    {
        Coordinate initialPosition = SimContext.rover.CurrentPosition;
        IEnumerable<Coordinate> initialSight = _coordinateCalculator
            .GetSightCoordinates(initialPosition, SimContext.map.Dimension, SimContext.rover.Sight);

        foreach (var coordinate in initialSight)
        {
            string? representation = SimContext.map.Representation[coordinate.X, coordinate.Y];
            SimContext.rover.Discoveries.TryAdd(coordinate, representation);
        }
    }
}