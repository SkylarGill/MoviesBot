using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using MoviesBot.MovieDB.Endpoint;
using MoviesBot.MovieDB.Movies;
using MoviesBot.MovieDB.Movies.Request;
using MoviesBot.MovieDB.Movies.Response;
using MoviesBot.Tests.TestUtilities;
using NUnit.Framework;

namespace MoviesBot.Tests.MovieDB.Movies
{
    [TestFixture]
    public class MoviesClientTests
    {
        [Test]
        public async Task GetMoviesAsync_WithSuccessfulResponse_ReturnsMoviesList()
        {
            var moviesResponse = new MoviesResponse()
            {
                Page = 1,
                TotalPages = 1,
                TotalResults = 1,
                Movies = new List<Movie>()
                {
                    new Movie()
                    {
                        Adult = false,
                        Title = "Some Film: The Movie",
                        Id = 1,
                        Overview = "Events occur on a screen",
                        Popularity = 100,
                        Video = false,
                        BackdropPath = "http://example.com/backdrop",
                        GenreIds = new []{42, 12},
                        OriginalLanguage = "english",
                        OriginalTitle = "Original Title",
                        PosterPath = "http://example.com/poster",
                        ReleaseDate = "2020-02-21",
                        VoteAverage = 10,
                        VoteCount = int.MaxValue
                    },
                    new Movie()
                    {
                        Adult = false,
                        Title = "Some Film 2: The Sequel",
                        Id = 1,
                        Overview = "Events occur on a screen again",
                        Popularity = 100,
                        Video = false,
                        BackdropPath = "http://example.com/backdrop",
                        GenreIds = new []{42, 12},
                        OriginalLanguage = "english",
                        OriginalTitle = "Original Title 2",
                        PosterPath = "http://example.com/poster",
                        ReleaseDate = "2021-09-20",
                        VoteAverage = 10,
                        VoteCount = int.MaxValue
                    }
                }
            };
            
            var mockHttpClientFactory = HttpClientFactoryTestHelper.GetMockHttpClientFactory(moviesResponse);
            var mockGenresEndpoint = GetMockMoviesEndpoint();
            var moviesRequest = new MoviesRequest();
            var moviesClient = new MoviesClient(mockHttpClientFactory, mockGenresEndpoint);

            var actualMovies = await moviesClient.GetMoviesAsync(moviesRequest)
                .ConfigureAwait(false);
            
            Assert.That(actualMovies, Is.EquivalentTo(moviesResponse.Movies).Using<Movie>(AreMoviesEqual));
        }

        private static IMovieDbEndpoint GetMockMoviesEndpoint()
        {
            var mockEndpoint = new Mock<IMovieDbEndpoint>();
            mockEndpoint
                .Setup(endpoint => endpoint.ConstructUri(It.IsAny<IDictionary<string, string>>()))
                .Returns(new Uri("http://www.example.com/moviesendpoint"));

            return mockEndpoint.Object;
        }

        private static bool AreMoviesEqual(Movie movieA, Movie movieB)
        {
            return movieA.Adult == movieB.Adult &&
                   movieA.Video == movieB.Video &&
                   movieA.Id == movieB.Id &&
                   movieA.Overview == movieB.Overview &&
                   movieA.Popularity == movieB.Popularity &&
                   movieA.Title == movieB.Title &&
                   movieA.BackdropPath == movieB.BackdropPath &&
                   movieA.OriginalLanguage == movieB.OriginalLanguage &&
                   movieA.OriginalTitle == movieB.OriginalTitle &&
                   movieA.PosterPath == movieB.PosterPath &&
                   movieA.ReleaseDate == movieB.ReleaseDate &&
                   movieA.VoteAverage == movieB.VoteAverage &&
                   movieA.VoteCount == movieB.VoteCount &&
                   movieA.GenreIds.SequenceEqual(movieB.GenreIds);
        }
    }
}