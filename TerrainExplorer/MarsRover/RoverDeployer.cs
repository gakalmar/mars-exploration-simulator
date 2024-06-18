using TerrainGenerator.Calculators.Model;

namespace TerrainExplorer.MarsRover;

public class RoverDeployer : IRoverDeployer
{
    private int _id = 0;
    public int Sight { get; }

    public RoverDeployer(int sight)
    {
        Sight = sight;
    }

    public Rover Deploy(Coordinate landingSpot)
    {
        _id++;
        return new Rover(_id, landingSpot, Sight);
    }
}