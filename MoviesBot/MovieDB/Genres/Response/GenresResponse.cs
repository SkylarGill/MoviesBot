using System.Collections.Generic;

namespace MoviesBot.MovieDB.Genres.Response
{
    public class GenresResponse
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}