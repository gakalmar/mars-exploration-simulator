using TerrainGenerator.Calculators.Model;

namespace TerrainExplorer.Configuration;

public record ExplorationConfiguration(
    string FileName, 
    Coordinate LandingSpot, 
    IEnumerable<string> SearchItems, 
    int SimulationSteps
);