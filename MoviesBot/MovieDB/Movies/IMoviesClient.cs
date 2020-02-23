using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesBot.MovieDB.Movies.Request;
using MoviesBot.MovieDB.Movies.Response;

namespace MoviesBot.MovieDB.Movies
{
    public interface IMoviesClient
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(MoviesRequest moviesRequest);
    }
}