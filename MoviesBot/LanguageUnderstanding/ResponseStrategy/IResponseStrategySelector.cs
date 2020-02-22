using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy
{
    public interface IResponseStrategySelector
    {
        IResponseStrategy GetStrategy(MoviesBotIntent moviesBotIntent);
    }
}