using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.Simulation.SimulationSteps;

public class Scanning : SimulationStepBase
{
    private SimulationContext _simulationContext;
    private ICoordinateCalculator _coordinateCalculator;
    public Scanning(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator)
    {
        _simulationContext = simulationContext;
        _coordinateCalculator = coordinateCalculator;
    }
    public override void Execute()
    {
        AddSightCoordinatesToDiscoveries(Scan());
    }

    private IEnumerable<Coordinate> Scan()
    {
        Coordinate currentPosition = _simulationContext.rover.CurrentPosition;
        return _coordinateCalculator
            .GetSightCoordinates(currentPosition, _simulationContext.map.Dimension, _simulationContext.rover.Sight);
    }

    private void AddSightCoordinatesToDiscoveries(IEnumerable<Coordinate> sightCoordinates)
    {
        foreach (var coordinate in sightCoordinates)
        {
            string? representation = _simulationContext.map.Representation[coordinate.X, coordinate.Y];
            _simulationContext.rover.Discoveries.TryAdd(coordinate, representation);
        }
    }
    
}