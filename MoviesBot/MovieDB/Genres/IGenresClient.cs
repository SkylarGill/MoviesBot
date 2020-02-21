using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesBot.MovieDB.Genres.Response;

namespace MoviesBot.MovieDB.Genres
{
    public interface IGenresClient
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
    }
}