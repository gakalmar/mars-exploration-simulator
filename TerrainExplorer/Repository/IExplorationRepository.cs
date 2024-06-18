using TerrainExplorer.Exploration;

namespace TerrainExplorer.Repository;

public interface IExplorationRepository
{
    void Add(int steps, int mineralsFound, int waterFound, ExplorationOutcome outcome);
    void DeleteAll();
}