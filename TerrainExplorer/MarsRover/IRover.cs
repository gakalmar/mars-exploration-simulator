using TerrainGenerator.Calculators.Model;

namespace TerrainExplorer.MarsRover;

public interface IRover
{
    int Id { get; }
    Coordinate CurrentPosition { get; set; }
    int Sight { get; }
    Dictionary<Coordinate, string?> Discoveries { get; set; }
    Dictionary<Coordinate, string?> Route { get; set; }
}