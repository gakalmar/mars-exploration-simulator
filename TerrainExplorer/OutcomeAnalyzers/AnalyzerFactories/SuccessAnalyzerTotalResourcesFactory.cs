namespace TerrainExplorer.OutcomeAnalyzers.AnalyzerFactories;

public class SuccessAnalyzerTotalResourcesFactory : AnalyzerFactoryBase
{
    public SuccessAnalyzerTotalResourcesFactory(SimulationContext simContext, ICoordinateCalculator coordinateCalculator) : base(simContext, coordinateCalculator)
    {
    }

    public override OutcomeAnalyzerBase Create()
    {
        return new SuccessAnalyzerTotalResources(context, calculator);
    }
}