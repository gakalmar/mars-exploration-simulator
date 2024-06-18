using TerrainGenerator.Configuration.Model;
using TerrainGenerator.MapElements.Model;
using TerrainGenerator.MapElements.Service.Builder;

namespace TerrainGenerator.MapElements.Service.Generator;

public class MapElementsGenerator : IMapElementsGenerator
{
    private readonly MapElementBuilder _mapElementBuilder;

    public MapElementsGenerator(MapElementBuilder mapElementBuilder)
    {
        _mapElementBuilder = mapElementBuilder;
    }

    public IEnumerable<MapElement> CreateAll(MapConfiguration mapConfig)
    {
        return mapConfig.MapElementConfigurations
            .SelectMany(CreateElements);
    }

    private IEnumerable<MapElement> CreateElements(MapElementConfiguration mapElementConfig)
    {
        foreach (var ets in mapElementConfig.ElementsToSizes)
        {
            for (int i = 0; i < ets.ElementCount; i++)
            {
                yield return _mapElementBuilder.Build(
                    ets.Size,
                    mapElementConfig.Symbol,
                    mapElementConfig.Name,
                    mapElementConfig.DimensionGrowth,
                    mapElementConfig.PreferredLocationSymbol);
            }
        }
    }
}
