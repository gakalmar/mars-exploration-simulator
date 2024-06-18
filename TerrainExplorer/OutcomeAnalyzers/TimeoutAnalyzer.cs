using TerrainExplorer.Exploration;
using TerrainExplorer.Simulation;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.OutcomeAnalyzers;

public class TimeoutAnalyzer : OutcomeAnalyzerBase
{
    public TimeoutAnalyzer(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
    {
    }
    
    public override ExplorationOutcome Analyze()
    {
        if (context.StepsUsed >= context.StepsToTimeout)
        {
            context.outcome = ExplorationOutcome.Timeout;
            
            return ExplorationOutcome.Timeout;
        }
        else
        {
            return ExplorationOutcome.NotYetDetermined;
        };
    }
    
}