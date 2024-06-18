using TerrainExplorer.Exploration;
using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.OutcomeAnalyzers;

public class SuccessAnalyzerWithinReach : OutcomeAnalyzerBase
{
    public SuccessAnalyzerWithinReach(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
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
        
        // Iterate through all fields that have "water":
        foreach (var kvp in searchItemFields1)
        {
            // Create a collection of the neighbours of the current "water" field at a specified reach:
            IEnumerable<Coordinate> adjacentCoordinates = calculator.GetSightCoordinates(kvp.Key, context.map.Dimension, 5);
            
            // Iterate through the neighbours of the "water" field:
            foreach (var coordinate in adjacentCoordinates)
            {
                // If the neighbouring coordinate is also in the "searchItems2" dictionary, it means it contains "mineral"
                if (searchItemFields2.ContainsKey(coordinate))
                {
                    context.outcome = ExplorationOutcome.ColonizableResourcesWithinRange;
                    return ExplorationOutcome.ColonizableResourcesWithinRange;
                }
            }
        }

        return ExplorationOutcome.NotYetDetermined;;
    }
}