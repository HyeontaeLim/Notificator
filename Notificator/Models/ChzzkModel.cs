using System.Drawing;

namespace Notificator.Models;

public class ChzzkModel
{
    public class AccessTokenRq
    {
        public string grantType { get; set; }
        public string clientId { get; set; }
        public string clientSecret { get; set; }
        public string code { get; set; }
        public string state { get; set; }
    }

    public class AccessTokenRs
    {
        public int code { get; set; }
        public string message { get; set; }
        public AccessToken content { get; set; }
    }
    public class AccessToken
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string tokenType { get; set; }
        public string expiresIn { get; set; }
    }


    public class UserInfoRs
    {
        public int code { get; set; }
        public string message { get; set; }
        public UserInfo content { get; set; }

        public class UserInfo
        {
            public string channelId { get; set; }
            public string channelName { get; set; }
        }
    }

    public class LiveListRs
    {
        public int code { get; set; }
        public string message { get; set; }
        public LiveList content { get; set; }

        public class LiveList
        {
            public Data[] data { get; set; }
            public Page page { get; set; }

            public class Data
            {
                public int liveId { get; set; }
                public string liveTitle { get; set; }
                public string liveThumbnailImageUrl { get; set; }
                public int concurrentUserCount { get; set; }
                public string openDate { get; set; }
                public bool adult { get; set; }
                public string[] tags { get; set; }
                public string categoryType { get; set; }
                public string liveCategory { get; set; }
                public string liveCategoryValue { get; set; }
                public string channelId { get; set; }
                public string channelName { get; set; }
                public string channelImageUrl { get; set; }
            }
            public class Page
            {
                public string next { get; set; }
            }
        }

    }
    public class SearchResultRs
    {
        public int code { get; set; }
        public string message { get; set; }
        public SearchResult content { get; set; }

        public class SearchResult
        {
            public int size { get; set; }
            public Page page { get; set; }
            public ChannelData[] data { get; set; }

            public class Page
            {
                public Next next { get; set; }

                public class Next
                {
                    public int offset { get; set; }
                }
            }

            public class ChannelData
            {
                public Channel channel { get; set; }
                public class Channel
                {
                    public string channelId { get; set; }
                    public string channelName { get; set; }
                    public string channelImageUrl { get; set; }
                    public bool verifiedMark { get; set; }
                    public string channelDescription { get; set; }
                    public int followerCount { get; set; }
                    public bool openLive { get; set; }
                    public string[] activatedChannelBadgeIds { get; set; }
                }

            }
        }
    }

    public class LiveInfoRs
    {
        public int code { get; set; }
        public string message { get; set; }
        public LiveInfoContent content { get; set; }

        public class LiveInfoContent
        {
            public string channelId { get; set; }
            public string channelName { get; set; }
            public string channelImageUrl { get; set; }
            public bool verifiedMark { get; set; }
            public string channelType { get; set; }
            public string channelDescription { get; set; }
            public int followerCount { get; set; }
            public bool openLive { get; set; }
            public bool subscriptionAvailability { get; set; }
            public SubscriptionPaymentAvailability subscriptionPaymentAvailability { get; set; }
            public bool adMonetizationAvailability { get; set; }
            public List<string> activatedChannelBadgeIds { get; set; }
            public bool paidProductSaleAllowed { get; set; }
        }

        public class SubscriptionPaymentAvailability
        {
            public bool iapAvailability { get; set; }
            public bool iabAvailability { get; set; }
        }
    }
}