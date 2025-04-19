using System.Text;
using Newtonsoft.Json;

namespace Notificator.ApiSend;

public class DiscordApiSend
{
    public readonly IConfiguration _config;
 
    public DiscordApiSend(IConfiguration config)
    {
        _config = config;
    }

    public async Task sendMessage(string content, string? username, string? avatarUrl, string webhookUrl)
    {
        using (var httpClient = new HttpClient())
        {
            var payload = new
            {
                content = content,
                username = username,
                avatar_url = avatarUrl
            };
            
            var stringContent = new StringContent(JsonConvert.SerializeObject(payload),
                Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(webhookUrl, stringContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
