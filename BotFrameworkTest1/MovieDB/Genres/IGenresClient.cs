using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotFrameworkTest1.MovieDB.Genres
{
    public interface IGenresClient
    {
        Task<IEnumerable<Genre>> GetGenresAsync();
    }
}