using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesBot.MovieDB.Movies.Response
{
    public class MoviesResponse
    {
        public int Page { get; set; }
        
        [JsonProperty("total_results")]
        public int TotalResults { get; set; }
        
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
        
        [JsonProperty("results")]
        public IEnumerable<Movie> Movies { get; set; }
    }
}