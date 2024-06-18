using TerrainGenerator.MapElements.Model;

namespace TerrainExplorer.Configuration;

public interface IConfigurationValidator
{
    bool Validate(ExplorationConfiguration explorationConfiguration, Map map);
}