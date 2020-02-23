using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
using LuisRecognizer = Microsoft.Bot.Builder.AI.Luis.LuisRecognizer;
using LuisRecognizerOptions = Microsoft.Bot.Builder.AI.Luis.LuisRecognizerOptions;

namespace MoviesBot.LanguageUnderstanding
{
    public class MoviesRecognizer : IRecognizer
    {
        private readonly ITelemetryRecognizer _luisRecognizer;

        public MoviesRecognizer(IConfiguration configuration)
        {
            var luisApplication = new LuisApplication(configuration["LuisAppId"],
                configuration["LuisAPIKey"],
                "https://" + configuration["LuisAPIHostName"]);
            
            _luisRecognizer = new LuisRecognizer(luisApplication);
        }

        public async Task<RecognizerResult> RecognizeAsync(ITurnContext turnContext, CancellationToken cancellationToken) =>
            await _luisRecognizer
                .RecognizeAsync(turnContext, cancellationToken)
                .ConfigureAwait(false);

        public async Task<T> RecognizeAsync<T>(ITurnContext turnContext, CancellationToken cancellationToken)
            where T : IRecognizerConvert, new() =>
                await _luisRecognizer
                    .RecognizeAsync<T>(turnContext, cancellationToken)
                    .ConfigureAwait(false);
    }
}