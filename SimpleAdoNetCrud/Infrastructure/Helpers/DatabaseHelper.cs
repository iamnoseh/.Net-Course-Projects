namespace Infrastructure.Helpers;

public static class DatabaseHelper
{
    private static readonly string _connectionString = "Server=localhost;Database=Test-db;Username=postgres;Password=12345;";
    
    public static string GetConnectionString()
    {
        return _connectionString;
    }
}
