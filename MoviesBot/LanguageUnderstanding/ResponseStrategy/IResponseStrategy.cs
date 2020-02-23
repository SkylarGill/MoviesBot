using System.Threading.Tasks;
using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy
{
    public interface IResponseStrategy
    {
        MoviesBotIntent.Intent Intent { get; }

        Task<string> GetResponseMessageAsync(MoviesBotIntent moviesBotIntent);
    }
}