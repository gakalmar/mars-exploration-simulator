using TerrainExplorer.Exploration;
using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.Simulation.SimulationSteps;

public class Movement : SimulationStepBase
{
    private readonly SimulationContext _simulationContext;
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly Random _random = new Random();
    public Movement(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator)
    {
        _simulationContext = simulationContext;
        _coordinateCalculator = coordinateCalculator;
    }
    public override void Execute()
    {
        if (_simulationContext.outcome == ExplorationOutcome.NotYetDetermined)
        {
            ExecuteExplorationRoutine();
        }
        else
        {
            ExecuteReturnRoutine();
        }
    }
    
    private void ExecuteReturnRoutine()
    {
        Coordinate currentCoordinate = _simulationContext.rover.CurrentPosition;
        Coordinate locationOfSpaceShip = _simulationContext.LocationSpaceship;
        var discoveredCoordinates = _simulationContext.rover.Discoveries;
        
        Dictionary<Coordinate, List<int>> returnCoordinatesDictionary = new Dictionary<Coordinate, List<int>>();
        foreach (var coordinate in discoveredCoordinates.Keys)
        {
            int distanceFromSpShip = Math.Abs(coordinate.X - locationOfSpaceShip.X) +
                                     Math.Abs(coordinate.Y - locationOfSpaceShip.Y);
            List<int> dataList = new List<int> { distanceFromSpShip, 0 };
            returnCoordinatesDictionary.TryAdd(coordinate, dataList);
        }
        
        Coordinate currentReturnCoordinate = currentCoordinate;
        int stepCounter = 0;
        while (currentReturnCoordinate != locationOfSpaceShip)
        {
            List<Coordinate> adjacentCoordinatesFromCurrent =
                _coordinateCalculator.GetAdjacentCoordinates(currentReturnCoordinate, _simulationContext.map.Dimension)
                    .Where(coord => returnCoordinatesDictionary.ContainsKey(coord)).ToList();
            
            if (adjacentCoordinatesFromCurrent.Count >= 1)
            {
                currentReturnCoordinate = adjacentCoordinatesFromCurrent.OrderBy(coord => returnCoordinatesDictionary[coord][1])
                    .ThenBy(coordinate => returnCoordinatesDictionary[coordinate][0])
                    .First();
                returnCoordinatesDictionary[currentReturnCoordinate][1]++;
            }
            else
            {
                currentReturnCoordinate = locationOfSpaceShip;
                Console.WriteLine("Rover could not find its way back to the spaceship.");
            }
            
            stepCounter++;
        }
        
        Console.WriteLine($"\nRover-{_simulationContext.rover.Id} returned in {stepCounter} steps.");
    }
    
    private void ExecuteExplorationRoutine()
    {
        Coordinate nextCoordinate = NextCoordinate();
        _simulationContext.rover.CurrentPosition = nextCoordinate;
        _simulationContext.rover.Route.TryAdd(nextCoordinate, _simulationContext.map.Representation[nextCoordinate.X, nextCoordinate.Y]);
    }

    private IEnumerable<Coordinate> EveryAdjacentCoordinate()
    {
        return _coordinateCalculator.GetAdjacentCoordinates(_simulationContext.rover.CurrentPosition, _simulationContext.map.Dimension);
    }

    private IEnumerable<Coordinate> AdjacentCoordsNotYetVisited()
    {
        return EveryAdjacentCoordinate().Where(coord => !_simulationContext.rover.Route.ContainsKey(coord));
    }

    private Coordinate NextCoordinate()
    {
        List<Coordinate> adjacentCoordinatesNotVisited = AdjacentCoordsNotYetVisited().ToList();
        
        //Start searching near a place where there is not empty space.
        List<Coordinate> adjacentCoordinatesNotVisitedPreferred = adjacentCoordinatesNotVisited.Where(coord =>
           _simulationContext.map.Representation[coord.X, coord.Y] != " ").ToList();
        if (adjacentCoordinatesNotVisitedPreferred.Count > 0)
        {
            return adjacentCoordinatesNotVisitedPreferred[_random.Next(adjacentCoordinatesNotVisitedPreferred.Count)];
        }
        
        if (adjacentCoordinatesNotVisited.Count > 0)
        {
            return adjacentCoordinatesNotVisited[_random.Next(adjacentCoordinatesNotVisited.Count)];
        }

        List<Coordinate> adjacentCoordinates = EveryAdjacentCoordinate().ToList();
        return adjacentCoordinates[_random.Next(adjacentCoordinates.Count)];
    }

}