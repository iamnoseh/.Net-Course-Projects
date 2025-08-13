using Npgsql;
namespace Infrastructure.Data;
public class DataContext
{
    private string connectionString = "Server=localhost;Database=library-management-db;Username=postgres;Password=12345;Include Error Detail=true";
    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}