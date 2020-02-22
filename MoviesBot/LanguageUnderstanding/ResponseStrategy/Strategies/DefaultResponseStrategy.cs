using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy.Strategies
{
    public class DefaultResponseStrategy : IResponseStrategy
    {
        public MoviesBotIntent.Intent Intent => MoviesBotIntent.Intent.None;
        public string GetResponseMessage(MoviesBotIntent moviesBotIntent)
        {
            throw new System.NotImplementedException();
        }
    }
}