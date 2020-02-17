using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BotFrameworkTest1.MovieDB.Endpoint;
using BotFrameworkTest1.MovieDB.Genres;
using BotFrameworkTest1.Tests.TestUtilities;
using Moq;
using NUnit.Framework;

namespace BotFrameworkTest1.Tests.MovieDB.Genres
{
    [TestFixture]
    public class GenresClientTests
    {
        [Test]
        public async Task GetGenresAsync_WithSuccessfulResponse_ReturnsGenreList()
        {
            var genresResponse = new GenresResponse
            {
                Genres = new List<Genre>
                {
                    new Genre {Id = 1, Name = "action"},
                    new Genre {Id = 2, Name = "comedy"},
                    new Genre {Id = 3, Name = "horror"},
                }
            };
            
            var mockHttpClientFactory = GetMockHttpClientFactory(genresResponse);
            var mockGenresEndpoint = GetMockGenresEndpoint();

            var genresClient = new GenresClient(mockGenresEndpoint, mockHttpClientFactory);

            var actualGenres = await genresClient.GetGenresAsync()
                .ConfigureAwait(false);
            
            Assert.That(actualGenres, Is.EquivalentTo(genresResponse.Genres));
        }

        private IMovieDbEndpoint GetMockGenresEndpoint()
        {
            var mockEndpoint = new Mock<IMovieDbEndpoint>();
            mockEndpoint
                .Setup(endpoint => endpoint.ConstructUri(null))
                .Returns(new Uri("example.com/genresendpoint"));

            return mockEndpoint.Object;
        }

        private static IHttpClientFactory GetMockHttpClientFactory(object response)
        {
            var testHttpMessageHandler = new TestHttpMessageHandler(HttpStatusCode.OK, response);
            var httpClient = new HttpClient(testHttpMessageHandler);
            // var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            // mockHttpClientFactory
            //     .Setup(factory => factory.CreateClient())
            //     .Returns(httpClient);
            
            var mockHttpClientFactory = testHttpMessageHandler.CreateClientFactory()

            return mockHttpClientFactory.Object;
        }
    }
}   