using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy.Strategies
{
    public class GetMoviesResponseStrategy : IResponseStrategy
    {
        public MoviesBotIntent.Intent Intent => MoviesBotIntent.Intent.GetMovies;
        public string GetResponseMessage(MoviesBotIntent moviesBotIntent)
        {
            throw new System.NotImplementedException();
        }
    }
}