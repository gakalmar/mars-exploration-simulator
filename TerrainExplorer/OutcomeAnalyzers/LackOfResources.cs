namespace TerrainExplorer.OutcomeAnalyzers;

public class LackOfResources : OutcomeAnalyzerBase
{
    public LackOfResources(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
    {
    }

    public override ExplorationOutcome Analyze()
    {
        if (context.rover.Discoveries.Count >= Math.Pow(context.map.Dimension, 2) * 0.4)    // Set the max discovery rate, before it is considered unsuccessful
        {
            context.outcome = ExplorationOutcome.DiscoveryRateReached;
            return ExplorationOutcome.DiscoveryRateReached;
        }
        else
        {
            return ExplorationOutcome.NotYetDetermined;
        };
    }
}