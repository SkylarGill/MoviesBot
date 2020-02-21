using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MoviesBot.MovieDB.Endpoint;
using MoviesBot.MovieDB.Genres.Response;
using Newtonsoft.Json;

namespace MoviesBot.MovieDB.Genres
{
    public class GenresClient : IGenresClient
    {
        private readonly IMovieDbEndpoint _genresEndpoint;
        private readonly IHttpClientFactory _httpClientFactory;
        
        private IEnumerable<Genre> _genres;

        public GenresClient(IMovieDbEndpoint genresEndpoint, IHttpClientFactory httpClientFactory)
        {
            _genresEndpoint = genresEndpoint;
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<IEnumerable<Genre>> GetGenresAsync()
        {
            if (_genres == null)
            {
                var genresResponse = await GetGenresResponse();
                _genres = genresResponse.Genres;
            }

            return _genres;
        }

        private async Task<GenresResponse> GetGenresResponse()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var genresUri = _genresEndpoint.ConstructUri();
            
            var response = await httpClient.GetAsync(genresUri)
                .ConfigureAwait(false);
            
            response.EnsureSuccessStatusCode();

            var responseBodyString = await response.Content.ReadAsStringAsync()
                .ConfigureAwait(false);

            var parsedResponse = JsonConvert.DeserializeObject<GenresResponse>(responseBodyString);
            return parsedResponse;
        }
    }
}