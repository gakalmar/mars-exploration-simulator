using TerrainGenerator.Configuration.Model;

namespace TerrainGenerator.Configuration.Service;

public class MapConfigurationValidator : IMapConfigurationValidator
{
    public bool Validate(MapConfiguration mapConfig)
    {
        var totalSquares = CalculateTotalSquares(mapConfig);
        var elementSquares = CalculateElementSquares(mapConfig);
        var maximumElementSquares = totalSquares * mapConfig.ElementToSpaceRatio;

        return elementSquares < maximumElementSquares;
    }

    private static double CalculateTotalSquares(MapConfiguration mapConfig) => Math.Pow(mapConfig.MapSize, 2);

    private static int CalculateElementSquares(MapConfiguration mapConfig)
    {
        return mapConfig.MapElementConfigurations.SelectMany(cfg => cfg.ElementsToSizes).Sum(ets => ets.ElementCount);
    }
}
