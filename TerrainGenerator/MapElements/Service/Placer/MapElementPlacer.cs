using TerrainGenerator.Calculators.Model;
using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.MapElements.Service.Placer;

public class MapElementPlacer : IMapElementPlacer
{
    public bool CanPlaceElement(MapElement element, string?[,] map, Coordinate coordinate)
    {
        return element.Dimension == 1
            ? CanPlaceOneDimensionalElement(map, coordinate)
            : CanPlaceMultiDimensionalElement(element, map, coordinate);
    }

    private static bool CanPlaceOneDimensionalElement(string?[,] map, Coordinate coordinate)
    {
        return coordinate.X >= 0
               && coordinate.X < map.GetLength(0)
               && coordinate.Y >= 0
               && coordinate.Y < map.GetLength(1)
               && map[coordinate.X, coordinate.Y] == null;
    }

    private static bool CanPlaceMultiDimensionalElement(MapElement element, string?[,] map, Coordinate coordinate)
    {
        if (coordinate.X + element.Dimension >= map.GetLength(0) || coordinate.X < 0)
        {
            return false;
        }

        if (coordinate.Y + element.Dimension >= map.GetLength(1) || coordinate.Y < 0)
        {
            return false;
        }

        var allXCoordinates =
            Enumerable.Range(coordinate.X, element.Dimension)
                .Select(e => coordinate with { X = e });

        var allYCoordinates =
            Enumerable.Range(coordinate.Y, element.Dimension)
                .Select(e => coordinate with { Y = e });


        return map[coordinate.X, coordinate.Y] == null
               && allXCoordinates.Concat(allYCoordinates).All(c => map[c.X, c.Y] == null);
    }

    public void PlaceElement(MapElement element, string?[,] map, Coordinate coordinate)
    {
        for (int i = 0; i < element.Representation.GetLength(0); i++)
        {
            for (int j = 0; j < element.Representation.GetLength(1); j++)
            {
                map[coordinate.X + i, coordinate.Y + j] = element.Representation[i, j];
            }
        }
    }
}
