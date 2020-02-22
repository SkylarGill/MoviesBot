using System.Collections.Generic;
using System.Linq;
using MoviesBot.LanguageUnderstanding.Model;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy
{
    public class ResponseStrategySelector : IResponseStrategySelector
    {
        private readonly IEnumerable<IResponseStrategy> _responseStrategies;

        public ResponseStrategySelector(IEnumerable<IResponseStrategy> responseStrategies)
        {
            _responseStrategies = responseStrategies;
        }

        public IResponseStrategy GetStrategy(MoviesBotIntent moviesBotIntent)
        {
            var topIntent = moviesBotIntent.TopIntent().intent;
            var matchingStrategy = _responseStrategies.FirstOrDefault(strategy => strategy.Intent == topIntent);

            if (matchingStrategy == null)
            {
                throw new NoMatchingResponseStrategyException();
            }

            return matchingStrategy;
        }
    }
}