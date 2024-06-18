using TerrainGenerator.Calculators.Service;

namespace TerrainExplorer.Simulation.SimulationStepFactory;

public abstract class SimulationStepFactoryBase
{
    protected SimulationStepFactoryBase()
    {
    }
    
    public abstract SimulationStepBase Create(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator);
}