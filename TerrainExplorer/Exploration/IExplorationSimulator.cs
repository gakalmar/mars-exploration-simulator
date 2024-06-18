using TerrainExplorer.MarsRover;
using TerrainExplorer.Simulation;
using TerrainExplorer.Simulation.SimulationStepFactory;

namespace TerrainExplorer.Exploration;

public interface IExplorationSimulator
{
    public SimulationContext SimContext { get; }
    public Rover Rover { get; }
    public void GenerateContext();
    public void Run(List<SimulationStepFactoryBase> simulationStepFactories);
}