using System.Threading.Tasks;
using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy.Strategies
{
    public class DefaultResponseStrategy : IResponseStrategy
    {
        private const string DefaultResponseMessage = "Sorry, I don't understand that.\r\nTry asking me about movies from a specific year and/or genre.";
        
        public MoviesBotIntent.Intent Intent => MoviesBotIntent.Intent.None;
        
        public Task<string> GetResponseMessageAsync(MoviesBotIntent moviesBotIntent)
        {
            return Task.FromResult(DefaultResponseMessage);
        }
    }
}