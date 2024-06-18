namespace TerrainGenerator.Configuration.Model;

public record MapElementConfiguration(
    string Symbol,
    string Name, IEnumerable<ElementToSize> ElementsToSizes,
    int DimensionGrowth = 0,
    string? PreferredLocationSymbol = null);