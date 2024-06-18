namespace TerrainExplorer.OutcomeAnalyzers;

public abstract class OutcomeAnalyzerBase
{
    protected SimulationContext context;
    protected ICoordinateCalculator calculator;

    protected OutcomeAnalyzerBase(SimulationContext simContext, ICoordinateCalculator coordinateCalculator)
    {
        context = simContext;
        calculator = coordinateCalculator;
    }

    public abstract ExplorationOutcome Analyze();
}