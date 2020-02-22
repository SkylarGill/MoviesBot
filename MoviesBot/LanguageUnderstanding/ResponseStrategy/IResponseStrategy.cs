using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy
{
    public interface IResponseStrategy
    {
        MoviesBotIntent.Intent Intent { get; }

        string GetResponseMessage(MoviesBotIntent moviesBotIntent);
    }
}