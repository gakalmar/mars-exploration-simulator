using TerrainGenerator.Calculators.Service;
using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.MapElements.Service.Builder;

public class MapElementBuilder : IMapElementBuilder
{
    private readonly IDimensionCalculator _dimensionCalculator;
    private static readonly Random Random = new();

    public MapElementBuilder(IDimensionCalculator dimensionCalculator)
    {
        _dimensionCalculator = dimensionCalculator;
    }

    public MapElement Build(int size, string symbol, string name, int dimensionGrowth,
        string? preferredLocationSymbol = null)
    {
        return size == 1
            ? BuildSingleElement(symbol, name, preferredLocationSymbol)
            : BuildElement(size, symbol, name, dimensionGrowth, preferredLocationSymbol);
    }

    private static MapElement BuildSingleElement(string symbol, string name, string? preferredLocationSymbol = null)
    {
        var arr = new string?[1, 1];
        arr[0, 0] = symbol;

        return new MapElement(arr, name, 1, preferredLocationSymbol);
    }

    private MapElement BuildElement(int size, string symbol, string name, int dimensionGrowth,
        string? preferredLocationSymbol = null)
    {
        var dimension = _dimensionCalculator.CalculateDimension(size, dimensionGrowth);

        int cnt = size;
        string?[,] representation = new string?[dimension, dimension];

        while (cnt > 0)
        {
            var x = Random.Next(dimension);
            var y = Random.Next(dimension);

            if (representation[x, y] is null)
            {
                representation[x, y] = symbol;
                cnt--;
            }
        }

        return new MapElement(representation, name, dimension, preferredLocationSymbol);
    }
}
