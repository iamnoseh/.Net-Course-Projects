using Npgsql;

namespace Infrastructure.Data;

public class DataContext
{
    private readonly string connectionString = "Server=localhost;Database=productdb;Username=postgres;Password=12345;";

    public NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(connectionString);
    }
}