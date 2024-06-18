using TerrainExplorer.Simulation;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.OutcomeAnalyzers.AnalyzerFactories;

public class SuccessAnalyzerWithinReachFactory : AnalyzerFactoryBase
{
    public SuccessAnalyzerWithinReachFactory(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
    {
    }

    public override OutcomeAnalyzerBase Create()
    {
        return new SuccessAnalyzerWithinReach(context, calculator);
    }
}