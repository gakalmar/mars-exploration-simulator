using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;
using TerrainGenerator.Configuration.Model;
using TerrainGenerator.Configuration.Service;
using TerrainGenerator.MapElements.Model;
using TerrainGenerator.MapElements.Service.Placer;

namespace TerrainGenerator.MapElements.Service.Generator;

public class MapGenerator : IMapGenerator
{
    private static readonly Random Random = new();
    private static Map EmptyMap => new(new string[,] { }, false);

    private readonly IMapConfigurationValidator _mapConfigValidator;
    private readonly IMapElementsGenerator _mapElementsGenerator;
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly IDimensionCalculator _dimensionCalculator;
    private readonly IMapElementPlacer _mapElementPlacer;

    public MapGenerator(
        IMapConfigurationValidator mapConfigValidator,
        IMapElementsGenerator mapElementsGenerator,
        ICoordinateCalculator coordinateCalculator,
        IDimensionCalculator dimensionCalculator,
        IMapElementPlacer mapElementPlacer)
    {
        _mapConfigValidator = mapConfigValidator;
        _mapElementsGenerator = mapElementsGenerator;
        _coordinateCalculator = coordinateCalculator;
        _dimensionCalculator = dimensionCalculator;
        _mapElementPlacer = mapElementPlacer;
    }

    public Map Generate(MapConfiguration mapConfig)
    {
        if (!_mapConfigValidator.Validate(mapConfig))
        {
            return EmptyMap;
        }

        var dimension = _dimensionCalculator.CalculateDimension(mapConfig.MapSize, 0);
        string?[,] map = new string?[dimension, dimension];

        var elements =
            new Stack<MapElement>(_mapElementsGenerator.CreateAll(mapConfig).OrderBy(e => e.Dimension));

        while (elements.Any())
        {
            var element = elements.Pop();
            var coord = GetTargetCoordinate(element, map);
            if (_mapElementPlacer.CanPlaceElement(element, map, coord))
            {
                _mapElementPlacer.PlaceElement(element, map, coord);
            }
            else
            {
                elements.Push(element);
            }
        }

        return new Map(map, true);
    }

    private Coordinate GetTargetCoordinate(MapElement element, string?[,] map)
    {
        if (element.PreferredLocationSymbol != null)
        {
            var preferredLocations = GetPreferredLocations(element.PreferredLocationSymbol, map);
            var emptyAdjacent = GetEmptyAdjacent(preferredLocations, map).ToList();

            return emptyAdjacent.Any()
                ? emptyAdjacent[Random.Next(emptyAdjacent.Count)]
                : _coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
        }

        return _coordinateCalculator.GetRandomCoordinate(map.GetLength(0));
    }

    private static IEnumerable<Coordinate> GetPreferredLocations(string preferredLocationSymbol, string?[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if (map[i, j] == preferredLocationSymbol)
                {
                    yield return new Coordinate(i, j);
                }
            }
        }
    }

    private IEnumerable<Coordinate> GetEmptyAdjacent(IEnumerable<Coordinate> coordinates, string?[,] map)
    {
        return _coordinateCalculator.GetAdjacentCoordinates(coordinates, map.GetLength(0))
            .Where(c => map[c.X, c.Y] == null);
    }
}