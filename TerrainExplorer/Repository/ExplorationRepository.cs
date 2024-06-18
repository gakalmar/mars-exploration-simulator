using Microsoft.Data.Sqlite;
using TerrainExplorer.Exploration;

namespace TerrainExplorer.Repository;

public class ExplorationRepository : IExplorationRepository
{
    private readonly string _dbFilePath;

    public ExplorationRepository(string dbFilePath)
    {
        _dbFilePath = dbFilePath;
    }
    
    private SqliteConnection GetPhysicalDbConnection()
    {
        var dbConnection = new SqliteConnection($"Data Source ={_dbFilePath};Mode=ReadWrite");
        dbConnection.Open();
        return dbConnection;
    }
    
    private void ExecuteNonQuery(string query)
    {
        using var connection = GetPhysicalDbConnection();
        using var command = GetCommand(query, connection);
        command.ExecuteNonQuery();
    }
    
    private static SqliteCommand GetCommand(string query, SqliteConnection connection)
    {
        return new SqliteCommand
        {
            CommandText = query,
            Connection = connection,
        };
    }

    public void Add(int steps, int mineralsFound, int waterFound, ExplorationOutcome outcome)
    {
        var query = $"INSERT INTO Summaries (Created, Steps, Water, Mineral, Outcome) " +
                    $"VALUES (" +
                    $"'{DateTime.Now}'," +
                    $" '{steps}'," +
                    $" '{waterFound}'," +
                    $" '{mineralsFound}'," +
                    $" '{outcome.ToString()}')";
        ExecuteNonQuery(query);
    }

    public void DeleteAll()
    {
        var query = "DELETE FROM Summaries";
        ExecuteNonQuery(query);
    }
}