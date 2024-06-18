using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.Output.Service;

public interface IMapFileWriter
{
    void WriteMapFile(Map map, string file);
}
