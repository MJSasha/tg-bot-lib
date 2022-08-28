using System;
using System.Net;
using System.Net.Http;

namespace TgBotLib.Services
{
    public class BaseService
    {
        protected readonly HttpClient httpClient;
        protected Uri Root { get; set; }

        public BaseService(string entityRoot)
        {
            Root = new Uri(BaseBotSettings.BackRoot + entityRoot);

            HttpClientHandler handler = new()
            {
                CookieContainer = new CookieContainer()
            };
            handler.CookieContainer.Add(Root, new Cookie("token", BaseBotSettings.ApiToken));
            httpClient = new HttpClient(handler);
        }
    }
}
