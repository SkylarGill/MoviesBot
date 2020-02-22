using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using MoviesBot.LanguageUnderstanding.Model;
using MoviesBot.LanguageUnderstanding.ResponseStrategy;

namespace MoviesBot.Bots
{
    public class MessageHandler : IMessageHandler
    {
        private readonly IRecognizer _recognizer;
        private readonly IResponseStrategySelector _responseStrategySelector;

        public MessageHandler(IRecognizer recognizer, IResponseStrategySelector responseStrategySelector)
        {
            _recognizer = recognizer;
            _responseStrategySelector = responseStrategySelector;
        }
      
        public async Task<string> GetMessageResponse(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            var intent = await _recognizer.RecognizeAsync<MoviesBotIntent>(turnContext, cancellationToken).ConfigureAwait(false);
            var responseStrategy = _responseStrategySelector.GetStrategy(intent);
            return await responseStrategy.GetResponseMessageAsync(intent).ConfigureAwait(false);
        }
    }
}