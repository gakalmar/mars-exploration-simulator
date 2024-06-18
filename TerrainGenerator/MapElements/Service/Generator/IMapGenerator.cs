using TerrainGenerator.Configuration.Model;
using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.MapElements.Service.Generator;

public interface IMapGenerator
{
    Map Generate(MapConfiguration mapConfig);
}
