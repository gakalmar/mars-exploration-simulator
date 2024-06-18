using System.Text;
using TerrainGenerator.Calculators.Model;

namespace TerrainGenerator.MapElements.Model;

public record Map(string?[,] Representation, bool SuccessfullyGenerated = false)
{
    public int Dimension => Representation.GetLength(0);

    private static string CreateStringRepresentation(string?[,] arr)
    {
        var sb = new StringBuilder();

        for (int i = 0; i < arr.GetLength(0); i++)
        {
            string s = "";
            for (int j = 0; j < arr.GetLength(1); j++)
            {
                s += arr[i, j] == null ? " " : arr[i, j];
            }

            sb.AppendLine(s);
        }

        return sb.ToString();
    }

    public string? GetByCoordinate(Coordinate coordinate)
    {
        return Representation[coordinate.X, coordinate.Y];
    }

    public bool IsEmpty(Coordinate coordinate)
    {
        return string.IsNullOrEmpty(Representation[coordinate.X, coordinate.Y])
               || Representation[coordinate.X, coordinate.Y] == " ";
    }

    public override string ToString()
    {
        return CreateStringRepresentation(Representation);
    }
}
