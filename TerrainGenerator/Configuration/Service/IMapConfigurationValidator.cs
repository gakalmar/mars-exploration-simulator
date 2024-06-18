using TerrainGenerator.Configuration.Model;

namespace TerrainGenerator.Configuration.Service;

public interface IMapConfigurationValidator
{
    bool Validate(MapConfiguration mapConfig);
}
