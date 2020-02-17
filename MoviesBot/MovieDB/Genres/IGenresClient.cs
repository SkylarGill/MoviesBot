using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesBot.MovieDB.Genres
{
    public interface IGenresClient
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
    }
}