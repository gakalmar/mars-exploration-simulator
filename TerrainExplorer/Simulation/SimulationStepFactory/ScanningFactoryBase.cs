namespace TerrainExplorer.Simulation.SimulationStepFactory;

public class ScanningFactoryBase : SimulationStepFactoryBase
{
    public override SimulationStepBase Create(SimulationContext simulationContext, ICoordinateCalculator coordinateCalculator)
    {
        return new Scanning(simulationContext, coordinateCalculator);
    }
}