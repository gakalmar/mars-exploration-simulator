using TerrainGenerator.Calculators.Model;

namespace TerrainGenerator.Calculators.Service;

public class CoordinateCalculator : ICoordinateCalculator
{
    private static readonly Random Random = new();

    public Coordinate GetRandomCoordinate(int dimension)
    {
        return new Coordinate(
            Random.Next(dimension),
            Random.Next(dimension)
        );
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, int dimension, int reach = 1)
    { 
        var adjacent = new[]
        {
            coordinate with { Y = coordinate.Y + reach },
            coordinate with { Y = coordinate.Y - reach },
            coordinate with { X = coordinate.X + reach },
            coordinate with { X = coordinate.X - reach },

        };

        return adjacent.Where(c => c.X >= 0 && c.Y >= 0 && c.X < dimension && c.Y < dimension);
    }

    public IEnumerable<Coordinate> GetAdjacentCoordinates(IEnumerable<Coordinate> coordinates, int dimension)
    {
        return coordinates.SelectMany(c => GetAdjacentCoordinates(c, dimension));
    }
    
    public IEnumerable<Coordinate> GetSightCoordinates(Coordinate currentPosition, int dimension, int sight)
    {
        HashSet<Coordinate> sightCoordinates = new HashSet<Coordinate>();
        HashSet<Coordinate> outerCoordinatesToTry = new HashSet<Coordinate> { currentPosition };
        for (int i = 0; i < sight; i++)
        {
            IEnumerable<Coordinate> nextRange = GetAdjacentCoordinates(outerCoordinatesToTry, dimension);
            HashSet<Coordinate> nextCoordinatesToTry = new HashSet<Coordinate>();
            foreach (var coordinate in nextRange)
            {
                if (sightCoordinates.Add(coordinate))
                {
                    nextCoordinatesToTry.Add(coordinate);
                }
            }
            outerCoordinatesToTry = nextCoordinatesToTry;
        }

        return sightCoordinates;
    }
}