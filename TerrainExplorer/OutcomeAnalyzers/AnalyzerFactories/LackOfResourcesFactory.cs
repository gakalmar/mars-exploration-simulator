using TerrainExplorer.Simulation;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.OutcomeAnalyzers.AnalyzerFactories;

public class LackOfResourcesFactory : AnalyzerFactoryBase
{
    public LackOfResourcesFactory(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
    {
    }
    
    public override OutcomeAnalyzerBase Create()
    {
        return new LackOfResources(context, calculator);
    }
}