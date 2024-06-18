using TerrainGenerator.Calculators.Model;

namespace TerrainExplorer.MarsRover;

public class Rover : IRover
{
    public int Id { get; }
    public Coordinate CurrentPosition { get; set; }
    public int Sight { get; }
    public Dictionary<Coordinate, string?> Discoveries { get; set; } = new ();
    public Dictionary<Coordinate, string?> Route { get; set; } = new();

    public Rover(int id, Coordinate currentPosition, int sight)
    {
        Id = id;
        CurrentPosition = currentPosition;
        Sight = sight;
    }
    


    public override string ToString()
    {
        return $"ROVER-{Id}: CurrentPos: {CurrentPosition}";
    }
}