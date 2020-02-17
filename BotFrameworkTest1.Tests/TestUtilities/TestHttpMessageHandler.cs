using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BotFrameworkTest1.Tests.TestUtilities
{
    public class TestHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _message;
        
        
        public TestHttpMessageHandler(HttpStatusCode statusCode, object responseObject)
        {
            var contentString = JsonConvert.SerializeObject(responseObject);
            
            _message = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(contentString)
            };
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_message);
        }
    }
}