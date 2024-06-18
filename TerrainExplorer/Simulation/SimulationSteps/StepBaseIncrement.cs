using TerrainExplorer.Exploration;

namespace TerrainExplorer.Simulation.SimulationSteps;

public class StepBaseIncrement : SimulationStepBase
{
    private SimulationContext _simulationContext;
    public StepBaseIncrement(SimulationContext simulationContext)
    {
        _simulationContext = simulationContext;
    }
    public override void Execute()
    {
        if (_simulationContext.outcome == ExplorationOutcome.NotYetDetermined)
        {
            _simulationContext.StepsUsed++;
        }
    }
}