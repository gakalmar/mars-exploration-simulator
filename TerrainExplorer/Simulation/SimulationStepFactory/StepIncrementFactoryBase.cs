using TerrainExplorer.Simulation.SimulationSteps;
using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.Simulation.SimulationStepFactory;

public class StepIncrementFactoryBase : SimulationStepFactoryBase
{
    public override SimulationStepBase Create(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator)
    {
        return new StepBaseIncrement(simulationContext);
    }
}