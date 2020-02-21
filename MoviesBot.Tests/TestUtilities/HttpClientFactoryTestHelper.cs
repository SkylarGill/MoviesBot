using System.Net;
using System.Net.Http;
using Moq;

namespace MoviesBot.Tests.TestUtilities
{
    public static class HttpClientFactoryTestHelper
    {
        public static IHttpClientFactory GetMockHttpClientFactory(object response)
        {
            var testHttpMessageHandler = new TestHttpMessageHandler(HttpStatusCode.OK, response);
            return GetMockHttpClientFactory(testHttpMessageHandler);
        }
        
        public static IHttpClientFactory GetMockHttpClientFactory(HttpMessageHandler messageHandler)
        {
            var httpClient = new HttpClient(messageHandler);
            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            mockHttpClientFactory
                .Setup(factory => factory.CreateClient(It.IsAny<string>()))
                .Returns(httpClient);

            return mockHttpClientFactory.Object;
        }
    }
}