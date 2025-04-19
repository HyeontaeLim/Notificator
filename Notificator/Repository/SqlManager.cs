using MySql.Data.MySqlClient;

namespace Notificator.Repository;
public class SqlManager
{
    private readonly ILogger<SqlManager> _logger;
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    public SqlManager(ILogger<SqlManager> logger, IConfiguration config)
    {
        _logger = logger;
        _config = config;
        _connectionString = config["ConnectionStrings:DefaultConnection"];
    }

    public MySqlConnection createConnection()
    {
        return new MySqlConnection(_connectionString);
    }
}