using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MoviesBot.MovieDB.Endpoint;
using MoviesBot.MovieDB.Movies.Request;
using MoviesBot.MovieDB.Movies.Response;
using Newtonsoft.Json;

namespace MoviesBot.MovieDB.Movies
{
    public class MoviesClient : IMoviesClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMovieDbEndpoint _moviesEndpoint;

        public MoviesClient(IHttpClientFactory httpClientFactory, IMovieDbEndpoint moviesEndpoint)
        {
            _httpClientFactory = httpClientFactory;
            _moviesEndpoint = moviesEndpoint;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(MoviesRequest moviesRequest)
        {
            var moviesResponse = await GetMoviesResponse(moviesRequest).ConfigureAwait(false);
            return moviesResponse?.Movies;
        }

        private async Task<MoviesResponse> GetMoviesResponse(MoviesRequest moviesRequest)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var queryParams = moviesRequest.GetQueryParamsDictionary();
            var moviesUri = _moviesEndpoint.ConstructUri(queryParams);

            var response = await httpClient.GetAsync(moviesUri)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var responseBodyString = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            var parsedResponse = JsonConvert.DeserializeObject<MoviesResponse>(responseBodyString);
            return parsedResponse;
        }
    }
}