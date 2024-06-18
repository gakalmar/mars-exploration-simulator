using TerrainGenerator.Calculators.Model;

namespace TerrainExplorer.MarsRover;

public interface IRoverDeployer
{
    public int Sight { get; }
    Rover Deploy(Coordinate landingSpot);
}