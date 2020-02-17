using System.Collections.Generic;

namespace MoviesBot.MovieDB.Genres
{
    public class GenresResponse
    {
        public IEnumerable<Genre> Genres { get; set; }
    }
}