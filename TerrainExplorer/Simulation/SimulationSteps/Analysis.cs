using TerrainExplorer.OutcomeAnalyzers;
using TerrainExplorer.OutcomeAnalyzers.AnalyzerFactories;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.Simulation.SimulationSteps;

public class Analysis : SimulationStepBase
{
    private readonly SimulationContext _simulationContext;
    private readonly ICoordinateCalculator _coordinateCalculator;
    private readonly List<AnalyzerFactoryBase> _analysisFactories;
    private readonly List<OutcomeAnalyzerBase> _analyzers = new List<OutcomeAnalyzerBase>();

    public Analysis(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator)
    {
        _simulationContext = simulationContext;
        _coordinateCalculator = coordinateCalculator;
        _analysisFactories = new List<AnalyzerFactoryBase>()
        {
            new LackOfResourcesFactory(_simulationContext, _coordinateCalculator),
            new SuccessAnalyzerTotalResourcesFactory(_simulationContext, _coordinateCalculator),
            new SuccessAnalyzerWithinReachFactory(_simulationContext, _coordinateCalculator),
            new TimeoutAnalyzerFactory(_simulationContext, _coordinateCalculator)
        };
    }

    public override void Execute()
    {
        if (_analyzers.Count == 0)
        {
            GetAnalyzers();
        }

        foreach (var analyzer in _analyzers)
        {
            analyzer.Analyze();
        }
    }

    private void GetAnalyzers()
    {
        foreach (var factory in _analysisFactories)
        {
            _analyzers.Add(factory.Create());
        }
    }
}