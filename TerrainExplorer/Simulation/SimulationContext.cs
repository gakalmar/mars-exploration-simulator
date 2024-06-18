using TerrainExplorer.Exploration;
using TerrainExplorer.MarsRover;
using TerrainGenerator.Calculators.Model;
using TerrainGenerator.MapElements.Model;

namespace TerrainExplorer.Simulation;

public class SimulationContext
{
    public int StepsUsed { get; set; }
    public int StepsToTimeout { get; }
    public IRover rover;
    public Coordinate LocationSpaceship { get; }
    public Map map;
    public IEnumerable<string> SearchItems;
    public ExplorationOutcome outcome;

    public SimulationContext(IRover rover, Map map, IEnumerable<string> searchItems, int stepsToTimeout, Coordinate locationSpaceship)
    {
        this.rover = rover;
        this.map = map;
        SearchItems = searchItems;
        StepsUsed = 0;
        StepsToTimeout = stepsToTimeout;
        LocationSpaceship = locationSpaceship;
    }

    public override string ToString()
    {
        return $"STEP: {StepsUsed}\n" +
               $"\t{rover}:\n" +
               $"\tSTATUS: {nameof(StepsUsed)}: {StepsUsed}, {nameof(StepsToTimeout)}: {StepsToTimeout}\n" +
               $"\tFINDINGS: Water found: {rover.Discoveries.Where(kvp => kvp.Value == "*").ToList().Count}," +
               $" Minerals found: {rover.Discoveries.Where(kvp => kvp.Value == "%").ToList().Count}\n" +
               $"\tOUTCOME: {outcome}";
    }
    
    
}