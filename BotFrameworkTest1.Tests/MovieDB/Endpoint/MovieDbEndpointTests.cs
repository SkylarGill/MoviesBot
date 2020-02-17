using BotFrameworkTest1.MovieDB.Endpoint;
using NUnit.Framework;

namespace BotFrameworkTest1.Tests.MovieDB.Endpoint
{
    [TestFixture]
    public class MovieDbEndpointTests
    {
        [Test]
        public void ConstructUri_NoQueryParams_ReturnsRelevantUri()
        {
            const string baseUrl = "http://www.example.com";
            const string endpointPath = "/example/endpoint";
            const string apiKey = "apikey";
            var expectedUriString = $"{baseUrl}{endpointPath}?api_key={apiKey}";

            var endpoint = new MovieDbEndpoint(baseUrl, endpointPath, apiKey);

            var outputUri = endpoint.ConstructUri();

            Assert.That(outputUri, Is.EqualTo(expectedUriString));
        }
    }
}