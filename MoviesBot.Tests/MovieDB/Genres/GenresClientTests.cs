using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using MoviesBot.MovieDB.Endpoint;
using MoviesBot.MovieDB.Genres;
using MoviesBot.MovieDB.Genres.Response;
using MoviesBot.Tests.TestUtilities;
using NUnit.Framework;

namespace MoviesBot.Tests.MovieDB.Genres
{
    [TestFixture]
    public class GenresClientTests
    {
        private readonly Func<Genre, Genre, bool> _genreComparison = (genreA, genreB) => genreA.Id == genreB.Id && genreA.Name == genreB.Name;
        
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
            
            var mockHttpClientFactory = HttpClientFactoryTestHelper.GetMockHttpClientFactory(genresResponse);
            var mockGenresEndpoint = GetMockGenresEndpoint();

            var genresClient = new GenresClient(mockGenresEndpoint, mockHttpClientFactory);

            var actualGenres = await genresClient.GetGenresAsync()
                .ConfigureAwait(false);
            
            Assert.That(actualGenres, Is.EquivalentTo(genresResponse.Genres).Using<Genre>(_genreComparison));
        }

        private IMovieDbEndpoint GetMockGenresEndpoint()
        {
            var mockEndpoint = new Mock<IMovieDbEndpoint>();
            mockEndpoint
                .Setup(endpoint => endpoint.ConstructUri(null))
                .Returns(new Uri("http://www.example.com/genresendpoint"));

            return mockEndpoint.Object;
        }
    }
}   