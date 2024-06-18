using TerrainGenerator.MapElements.Model;

namespace TerrainGenerator.Output.Service;

public class MapFileWriter : IMapFileWriter
{
    public void WriteMapFile(Map map, string file)
    {
        File.WriteAllText(file, map.ToString());
    }
}
