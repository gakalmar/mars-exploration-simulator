using TerrainGenerator.Calculators.Model;
using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.MapElements.Service.Placer;

public interface IMapElementPlacer
{
    bool CanPlaceElement(MapElement element, string?[,] map, Coordinate coordinate);
    void PlaceElement(MapElement element, string?[,] map, Coordinate coordinate);
}
