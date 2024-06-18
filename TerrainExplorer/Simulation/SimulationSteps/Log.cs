using TerrainExplorer.Logger;

namespace TerrainExplorer.Simulation.SimulationSteps;

public class Log : SimulationStepBase
{
    private SimulationContext _simulationContext;
    private ILogger _fileLogger = new FileLogger("Output/exploration_outcome.txt");
    private ILogger _consoleLogger = new ConsoleLogger();
    public Log(SimulationContext simulationContext)
    {
        _simulationContext = simulationContext;
    }
    public override void Execute()
    {
        _fileLogger.LogInfo(_simulationContext.ToString());
        _consoleLogger.LogInfo(_simulationContext.ToString());
    }
}