using TerrainExplorer.Exploration;
using TerrainExplorer.Simulation;
using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.OutcomeAnalyzers;

public class SuccessAnalyzerTotalResources : OutcomeAnalyzerBase
{
    public SuccessAnalyzerTotalResources(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
    {
    }

    public override ExplorationOutcome Analyze()
    {
        // Save each resource into a new variable:
        string searchItem1 = context.SearchItems.ToList()[0];
        string searchItem2 = context.SearchItems.ToList()[1];
        
        // Create a dictionary, that only includes those coordinate fields, that have searchItem1(eg. water):
        Dictionary<Coordinate, string> searchItemFields1 = context.rover.Discoveries
            .Where(kvp => kvp.Value == searchItem1)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        
        // Create a dictionary, that only includes those coordinate fields, that have searchItem2(eg. mineral):
        Dictionary<Coordinate, string> searchItemFields2 = context.rover.Discoveries
            .Where(kvp => kvp.Value == searchItem2)
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
        
        // Condition: If there are 4 minerals and 3 waters found in total:
        if (searchItemFields1.Count >= 4 && searchItemFields2.Count >= 3)
        {
            context.outcome = ExplorationOutcome.ColonizableSufficientResourcesFound;
            return ExplorationOutcome.ColonizableSufficientResourcesFound;
        }
        else
        {
            return ExplorationOutcome.NotYetDetermined;
        };
    }
}