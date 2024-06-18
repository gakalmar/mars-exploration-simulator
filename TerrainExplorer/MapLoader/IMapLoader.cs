using TerrainGenerator.MapElements.Model;

namespace TerrainExplorer.MapLoader;

public interface IMapLoader
{
    Map Load(string mapFile);
}