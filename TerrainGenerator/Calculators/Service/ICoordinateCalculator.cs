using TerrainGenerator.Calculators.Model;

namespace TerrainGenerator.Calculators.Service;

public interface ICoordinateCalculator
{
    Coordinate GetRandomCoordinate(int dimension);
    IEnumerable<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, int dimension, int reach = 1);
    public IEnumerable<Coordinate> GetAdjacentCoordinates(IEnumerable<Coordinate> coordinates, int dimension);

    public IEnumerable<Coordinate> GetSightCoordinates(Coordinate currentPosition, int dimension, int sight);
}