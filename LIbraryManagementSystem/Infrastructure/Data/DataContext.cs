using Npgsql;
namespace Infrastructure.Data;
public class DataContext
{
    private readonly string _connectionString = "Server=localhost;Database=library-management-db;Username=postgres;Password=12345;";
    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}