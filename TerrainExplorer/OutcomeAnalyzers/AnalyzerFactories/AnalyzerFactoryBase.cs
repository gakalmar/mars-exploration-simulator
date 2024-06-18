using TerrainExplorer.Simulation;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.OutcomeAnalyzers.AnalyzerFactories;

public abstract class AnalyzerFactoryBase
{
    protected readonly SimulationContext context;
    protected readonly ICoordinateCalculator calculator;

    protected AnalyzerFactoryBase(SimulationContext simContext, ICoordinateCalculator coordinateCalculator)
    {
        context = simContext;
        calculator = coordinateCalculator;
    }

    public abstract OutcomeAnalyzerBase Create();
}