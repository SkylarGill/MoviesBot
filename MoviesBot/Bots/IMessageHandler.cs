using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.Bots
{
    public interface IMessageHandler
    {
        Task<string> GetMessageResponse(ITurnContext turnContext, CancellationToken cancellationToken);
    }
}