using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MoviesBot.Controllers
{
    [ApiController]
    public class WebChatController : Controller
    {
        private const string StylesheetResourceName = "MoviesBot.Content.Chat.css";

        private readonly string _botName;
        private readonly string _webChatSecret;
        private readonly string _stylesheetContent;

        public WebChatController(IConfiguration configuration)
        {
            _stylesheetContent = GetFileContentFromResource(StylesheetResourceName);

            _botName = configuration["BotName"];
            _webChatSecret = configuration["WebChatSecret"];
        }

        [Route("/webchat/stylesheet")]
        [HttpGet]
        public ContentResult GetStylesheet()
        {
            return new ContentResult()
            {
                Content = _stylesheetContent,
                ContentType = "text/css",
            };
        }
        
        [Route("/webchat/chatframe")]
        [HttpGet]
        public async Task GetChatFrame()
        {
            var token = await GetChatToken().ConfigureAwait(false);
            var redirectUrl = $"https://webchat.botframework.com/embed//{_botName}?t={token}";
            Response.Redirect(redirectUrl);
        }

        private async Task<string> GetChatToken()
        {
            const string tokenUrl = "https://webchat.botframework.com/api/tokens";
            var httpClient = new HttpClient();
            var tokenRequest = new HttpRequestMessage(HttpMethod.Get, tokenUrl);
            tokenRequest.Headers.Authorization = new AuthenticationHeaderValue("BotConnector", _webChatSecret);

            var tokenResponse = await httpClient.SendAsync(tokenRequest).ConfigureAwait(false);
            tokenResponse.EnsureSuccessStatusCode();
            
            var responseContent = await tokenResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
            return RemoveQuotesFromTokenResponse(responseContent);
        }

        private static string RemoveQuotesFromTokenResponse(string responseContent)
        {
            return responseContent.Substring(1, responseContent.Length - 2);
        }
        
        private static string GetFileContentFromResource(string htmlResourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var htmlStream = assembly.GetManifestResourceStream(htmlResourceName);
            
            if (htmlStream == null)
            {
                throw new FileNotFoundException($"HTML resource {htmlResourceName} not found");
            }
            
            using var htmlStreamReader = new StreamReader(htmlStream);
            return htmlStreamReader.ReadToEnd();
        }
    }
}