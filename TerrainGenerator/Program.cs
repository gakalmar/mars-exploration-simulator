using TerrainGenerator.Calculators.Service;
using TerrainGenerator.Configuration.Model;
using TerrainGenerator.Configuration.Service;
using TerrainGenerator.MapElements.Service.Builder;
using TerrainGenerator.MapElements.Service.Generator;
using TerrainGenerator.MapElements.Service.Placer;
using TerrainGenerator.Output.Service;

internal class Program
{
    private static readonly string WorkDir = AppDomain.CurrentDomain.BaseDirectory;

    public static void Main(string[] args)
    {
        Console.WriteLine("Mars Exploration Sprint 1");
        var mapConfig = GetConfiguration();

        var dimensionCalculator = new DimensionCalculator();
        var coordinateCalculator = new CoordinateCalculator();

        var mapElementFactory = new MapElementBuilder(dimensionCalculator);
        var mapElementsGenerator = new MapElementsGenerator(mapElementFactory);

        var mapConfigValidator = new MapConfigurationValidator();
        var mapElementPlacer = new MapElementPlacer();

        var mapGenerator =
            new MapGenerator(mapConfigValidator, mapElementsGenerator, coordinateCalculator, dimensionCalculator,
                mapElementPlacer);

        CreateAndWriteMaps(3, mapGenerator, mapConfig);

        Console.WriteLine("Mars maps successfully generated.");
        Console.ReadKey();
    }

    private static void CreateAndWriteMaps(int count, IMapGenerator mapGenerator, MapConfiguration mapConfig)
    {
        var mapFileWriter = new MapFileWriter();

        foreach (var cnt in Enumerable.Range(0, count))
        {
            string outputFile = @$"{WorkDir}\exploration-{cnt}.map";
            var map = mapGenerator.Generate(mapConfig);

            if (map.SuccessfullyGenerated)
            {
                mapFileWriter.WriteMapFile(map, outputFile);
            }
        }
    }

    private static MapConfiguration GetConfiguration()
    {
        const string mountainSymbol = "#";
        const string pitSymbol = "&";
        const string mineralSymbol = "%";
        const string waterSymbol = "*";

        var mountainsCfg = new MapElementConfiguration(mountainSymbol, "mountain", new[]
        {
            new ElementToSize(2, 20),
            new ElementToSize(1, 30),
        }, 3);

        var pitsCfg = new MapElementConfiguration(pitSymbol, "pit", new[]
        {
            new ElementToSize(2, 10),
            new ElementToSize(1, 20),
        }, 10);

        var mineralsCfg = new MapElementConfiguration(mineralSymbol, "mineral", new[]
        {
            new ElementToSize(10, 1),
        }, 0, mountainSymbol);

        var waterCfg = new MapElementConfiguration(waterSymbol, "water", new[]
        {
            new ElementToSize(10, 1),
        }, 0, pitSymbol);

        List<MapElementConfiguration> elementsCfg = new() { mountainsCfg, pitsCfg, mineralsCfg, waterCfg };
        return new MapConfiguration(1000, 0.5, elementsCfg);
    }
}