namespace TerrainExplorer.Simulation.SimulationStepFactory;

public class MovementFactoryBase : SimulationStepFactoryBase
{
    public override SimulationStepBase Create(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator)
    {
        return new Movement(simulationContext, coordinateCalculator);
    }
}