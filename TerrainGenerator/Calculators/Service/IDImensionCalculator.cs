namespace TerrainGenerator.Calculators.Service;

public interface IDimensionCalculator
{
    int CalculateDimension(int size, int dimensionGrowth);
}