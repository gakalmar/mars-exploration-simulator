using TerrainGenerator.Calculators.Model;
using TerrainGenerator.Calculators.Service;
using TerrainGenerator.MapElements.Model;

namespace TerrainExplorer.Configuration;

public class ConfigurationValidator : IConfigurationValidator
{
    private readonly ICoordinateCalculator _coordinateCalculator;

    public ConfigurationValidator(ICoordinateCalculator coordinateCalculator)
    {
        _coordinateCalculator = coordinateCalculator;
    }

    public bool Validate(ExplorationConfiguration explorationConfiguration, Map map)
    {
        Coordinate landingSpot = explorationConfiguration.LandingSpot;
        
        //landing spot is empty
        if (map.Representation[landingSpot.X, landingSpot.Y] != " " )
        {
            Console.WriteLine("Landing spot is not empty.");
            return false;
        }

        //validate if landing spot has at least 1 empty neighbouring space
        int mapDimension = map.Dimension;
        IEnumerable<Coordinate> adjacentCoordinates = _coordinateCalculator.GetAdjacentCoordinates(landingSpot, mapDimension);
        if (!adjacentCoordinates.Any(coord => map.Representation[coord.X, coord.Y] == " "))
        {
            Console.WriteLine("Landing spot does not have empty adjacent coordinates.");
            return false;
        }
        
        //validate if provided path to the map file is not empty
        if (explorationConfiguration.FileName.Length == 0)
        {
            Console.WriteLine("File path string is empty.");
            return false;
        }
        
        //The resources are specified
        if (!explorationConfiguration.SearchItems.Any(
                item => item == "#" || item == "%" || item == "&" || item == "*"  ))
        {
            return false;
        }
        
        //Timeout is greater than zero
        if (explorationConfiguration.SimulationSteps <= 0)
        {
            return false;
        }

        return true;
    }
}