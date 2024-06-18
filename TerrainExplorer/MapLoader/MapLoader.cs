using TerrainGenerator.MapElements.Model;

namespace TerrainExplorer.MapLoader;

public class MapLoader : IMapLoader
{
    public Map Load(string mapFile)
    {
        string[] lines = File.ReadAllLines(mapFile);
        int rows = lines.Length;
        int columns = lines[0].Length;
        string[,] mapRepresentation = new string[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                char character = lines[i][j];
                mapRepresentation[i, j] = character.ToString();
            }
        }
        
        return new Map(mapRepresentation, true);
    }
}