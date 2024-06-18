using TerrainExplorer.Simulation;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.OutcomeAnalyzers.AnalyzerFactories;

public class TimeoutAnalyzerFactory : AnalyzerFactoryBase
{
    public TimeoutAnalyzerFactory(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
    {
    }

    public override OutcomeAnalyzerBase Create()
    {
        return new TimeoutAnalyzer(context, calculator);
    }
}