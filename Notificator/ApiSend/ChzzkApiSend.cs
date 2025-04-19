using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using Notificator.Models;
using static Notificator.ConstantKey;
using static Notificator.Models.ChzzkModel;

namespace Notificator.ApiSend;

public class ChzzkApiSend
{
    public readonly IConfiguration _config;

    public ChzzkApiSend(IConfiguration config)
    {
        _config = config;
    }

    public async Task getAccessToken(AccessTokenRq accessToken)
    {
        string url = $"{_config["Chzzk:host"]}/auth/v1/token";
        string jsonRq = JsonConvert.SerializeObject(accessToken);
        StringContent content = new StringContent(jsonRq, Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            AccessTokenRs accessTokenRs = JsonConvert.DeserializeObject<AccessTokenRs>(json)!;
            ChzzkConstant.accessToken = accessTokenRs.content.accessToken;
            ChzzkConstant.refreshToken = accessTokenRs.content.refreshToken;
        }
    }

    public async Task<UserInfoRs?> getUserInfo()
    {
        string url = $"{_config["Chzzk:openApiHost"]}/open/v1/users/me";
        using (var httpClient = new HttpClient())
        {
            addAccessTokenAuth(httpClient);
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            UserInfoRs? userInfo = JsonConvert.DeserializeObject<UserInfoRs?>(json);
            return userInfo;
        }
    }
    public async Task<LiveListRs?> getLiveList(string? next)
    {
        var queryParams = new Dictionary<string, string>
        {
            { "size", "8" }
        };

        if (!string.IsNullOrEmpty(next))
        {
            queryParams.Add("next", next);
        }

        string url = QueryHelpers.AddQueryString($"{_config["Chzzk:openApiHost"]}/open/v1/lives", queryParams);

        using (var httpClient = new HttpClient())
        {
            addClientAuth(httpClient);
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            LiveListRs? liveList = JsonConvert.DeserializeObject<LiveListRs?>(json);
            return liveList;
        }
    }

    public async Task<SearchResultRs> chzzkSearch(string keyword, int offset, int size)
    {
        string url = $"https://api.chzzk.naver.com/service/v1/search/channels?keyword={keyword}&offset={offset}&size={size}";
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            SearchResultRs searchResultRs = JsonConvert.DeserializeObject<SearchResultRs>(json);
            return searchResultRs;
        }

    }

    public async Task<LiveInfoRs?> getLiveInfo(string channelId)
    {
        string url = $"https://api.chzzk.naver.com/service/v1/channels/{channelId}";
        using (var httpClient = new HttpClient())
        {
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string json = await response.Content.ReadAsStringAsync();
            LiveInfoRs? liveInfo = JsonConvert.DeserializeObject<LiveInfoRs?>(json);
            return liveInfo;
        }
    }

    private void addAccessTokenAuth(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ChzzkConstant.accessToken}");
    }

    private void addClientAuth(HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Add("Client-Id", ChzzkConstant.clientId);
        httpClient.DefaultRequestHeaders.Add("Client-Secret", ChzzkConstant.clientSecret);
    }
}