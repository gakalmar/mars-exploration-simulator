using TerrainGenerator.Configuration.Model;
using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.MapElements.Service.Generator;

public interface IMapElementsGenerator
{
    IEnumerable<MapElement> CreateAll(MapConfiguration mapConfig);
}
