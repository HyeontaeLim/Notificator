using MySql.Data.MySqlClient;
using static Notificator.Models.FollowModel;
using static Notificator.Models.ReturnCodeModel;

namespace Notificator.Repository;

public class FollowRepository : IFollowRepository
{
    private readonly SqlManager _sqlManager;
    private readonly ILogger<UserRepository> _logger;

    public FollowRepository(SqlManager sqlManager, ILogger<UserRepository> logger)
    {
        _sqlManager = sqlManager;
        _logger = logger;
    }

    public ReturnRs AddFollow(FollowRq followRq)
    {
        ReturnRs returnRs = new ReturnRs();
        using (var con = _sqlManager.createConnection())
        {
            try
            {
                con.Open();
                string query = "insert into Follow (user_id, channel_id, created_at) values (@userId, @channelId, @createdAt);";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", followRq.UserId);
                    cmd.Parameters.AddWithValue("@channelId", followRq.ChannelId);
                    cmd.Parameters.AddWithValue("@createdAt", followRq.CreatedAt);

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

    public bool IsFollowing(int userId, string channelId)
    {
        using (var con = _sqlManager.createConnection())
        {
            try
            {
                con.Open();
                string query = "select count(*) from Follow where user_id = @userId and channel_id = @channelId;";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@channelId", channelId);

                    // ExecuteScalar()는 쿼리 결과의 첫 번째 행의 첫 번째 열을 반환합니다.
                    // count(*) 쿼리의 결과를 가져오는 데 적절한 메서드입니다.
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                return false;
            }
        }
    }


    public ReturnRs RemoveFollow(int userId, string channelId)
    {
        ReturnRs returnRs = new ReturnRs();
        using (var con = _sqlManager.createConnection())
        {
            try
            {
                con.Open();
                string query = "delete from Follow where user_id = @userId and channel_id = @channelId;";
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@channelId", channelId);

                    int deleted = cmd.ExecuteNonQuery();
                    _logger.LogInformation($"Success! Deleted Rows: {deleted}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.StackTrace);
                returnRs.ReturnCode = -1;
                returnRs.ReturnMsg = "Delete Fail!";
            }
        }
        return returnRs;
    }

    public List<Follow> GetAllFollowList()
    {
        List<Follow> followList = new List<Follow>();
        using (var con = _sqlManager.createConnection())
        {

            con.Open();
            string query = "select * from Follow";
            using (var cmd = new MySqlCommand(query, con))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        followList.Add(new Follow(reader.GetInt32("id"), reader.GetInt32("user_id"), reader.GetString("channel_id"), reader.GetDateTime("created_at"), reader.IsDBNull(reader.GetOrdinal("update_at")) 
                            ? (DateTime?)null 
                            : reader.GetDateTime("created_at")));
                    }
                }
            }
        }
        return followList;
    }
}