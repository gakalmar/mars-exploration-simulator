using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.MapElements.Service.Builder;

public interface IMapElementBuilder
{
    MapElement Build(int size, string symbol, string name, int dimensionGrowth, string? preferredLocationSymbol = null);
}
