using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MoviesBot.MovieDB.Endpoint;
using NUnit.Framework;

namespace MoviesBot.Tests.MovieDB.Endpoint
{
    [TestFixture]
    public class MovieDbEndpointTests
    {
        [Test]
        public void ConstructUri_NoQueryParams_ReturnsRelevantUri()
        {
            const string baseUrl = "www.example.com";
            const string endpointPath = "/example/endpoint";
            const string apiKey = "apikey";
            var expectedUriString = $"http://{baseUrl}{endpointPath}?api_key={apiKey}";

            var endpoint = new MovieDbEndpoint(baseUrl, endpointPath, apiKey);

            var outputUri = endpoint.ConstructUri();

            Assert.That(outputUri.ToString(), Is.EqualTo(expectedUriString));
        }
        
        [Test]
        public void ConstructUri_WithQueryParams_ReturnsRelevantUriWithQueryParams()
        {
            const string baseUrl = "www.example.com";
            const string endpointPath = "/example/endpoint";
            const string apiKey = "apikey";
            
            var queryParams = new Dictionary<string, string>()
            {
                {"key_one", "value_one"},
                {"key_two", "value_two"},
            };
            
            var expectedUriString = $"http://{baseUrl}{endpointPath}{GetAsQueryString(queryParams)}&api_key={apiKey}";

            var endpoint = new MovieDbEndpoint(baseUrl, endpointPath, apiKey);

            var outputUri = endpoint.ConstructUri(queryParams);

            Assert.That(outputUri.ToString(), Is.EqualTo(expectedUriString));
        }

        private static string GetAsQueryString(Dictionary<string, string> queryParams)
        {
            return QueryString.Create(queryParams).Value;
        }
    }
}