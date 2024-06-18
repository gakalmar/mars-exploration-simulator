namespace TerrainGenerator.MapElements.Model;

public record MapElement(string?[,] Representation, string Name, int Dimension, string? PreferredLocationSymbol = null) : Map(Representation)
{
    public override string ToString()
    {
        return base.ToString();
    }
}
