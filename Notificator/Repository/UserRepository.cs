using MySql.Data.MySqlClient;
using static Notificator.Models.ReturnCodeModel;
using static Notificator.Models.UsersModel;

namespace Notificator.Repository;

public class UserRepository : IUserRepository
{
    private readonly SqlManager _sqlManager;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(SqlManager sqlManager, ILogger<UserRepository> logger)
    {
        _sqlManager = sqlManager;
        _logger = logger;
    }

    public List<User> FindAllUsers()
    {
        List<User> usersList = new List<User>();
        using (var con = _sqlManager.createConnection())
        {
            con.Open();
            string query = "SELECT id, user_id, password, email, name, discord_webhook_url FROM Users";
            using (var cmd = new MySqlCommand(query, con))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetInt32("id"), reader.GetString("user_id"), reader.GetString("email"), reader.GetString("password"), reader.GetString("name"), reader.GetString("discord_webhook_url"));
                        usersList.Add(user);
                    }
                }
            }
        }
        return usersList;
    }


    public List<User> FindUsersByName(string name)
    {
        List<User> usersList = new List<User>();
        using (var con = _sqlManager.createConnection())
        {
            con.Open();
            string query = "SELECT id, user_id, password, email, name, discord_webhook_url FROM Users WHERE name LIKE CONCAT('%', @name, '%')";
            using (var cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name", name);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User user = new User(reader.GetInt32("id"), reader.GetString("user_id"), reader.GetString("email"), reader.GetString("password"), reader.GetString("name"), reader.GetString("discord_webhook_url"));
                        usersList.Add(user);
                    }
                }
            }
        }

        return usersList;
    }

    public User? FindUsersById(int id)
    {
        using (var con = _sqlManager.createConnection())
        {
            con.Open();
            string query = "SELECT id, user_id, password, email, name, discord_webhook_url FROM Users WHERE id = @id";
            using (var cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(reader.GetInt32("id"), reader.GetString("user_id"), reader.GetString("email"), reader.GetString("password"), reader.GetString("name"), reader.GetString("discord_webhook_url"));
                    }
                }
            }
        }
        return null;
    }

    public User? FindUsersByUserId(string userId)
    {
        using (var con = _sqlManager.createConnection())
        {
            con.Open();
            string query = "SELECT id, user_id, password, email, name, discord_webhook_url FROM Users WHERE user_id = @userId";
            using (var cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User(reader.GetInt32("id"), reader.GetString("user_id"), reader.GetString("email"), reader.GetString("password"), reader.GetString("name"), reader.GetString("discord_webhook_url"));
                    }
                }
            }
        }
        return null;
    }

    public ReturnRs PutUser(UsersRq usersRq)
    {
        ReturnRs returnRs = new ReturnRs();
        using (var con = _sqlManager.createConnection())
        {
            try
            {
                con.Open();
                string query = "insert into Users (user_id, email, created_at, password, name, discord_webhook_url) values (@userId, @email, @createdAt, @password, @name, @discordWebhookUrl);";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", usersRq.UserId);
                    cmd.Parameters.AddWithValue("@email", usersRq.Email);
                    cmd.Parameters.AddWithValue("@createdAt", usersRq.CreatedAt);
                    cmd.Parameters.AddWithValue("@password", usersRq.Password);
                    cmd.Parameters.AddWithValue("@name", usersRq.Name);
                    cmd.Parameters.AddWithValue("@discordWebhookUrl", usersRq.DiscordWebhookUrl);

                    int inserted = cmd.ExecuteNonQuery();
                    _logger.LogInformation($"Success! Inserted Rows: {inserted}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                returnRs.ReturnCode = -1;
                returnRs.ReturnMsg = "Insert Fail!";
            }
        }
        return returnRs;
    }
}