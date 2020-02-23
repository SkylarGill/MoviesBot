using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Moq;
using MoviesBot.LanguageUnderstanding.Model;
using MoviesBot.LanguageUnderstanding.ResponseStrategy;
using MoviesBot.LanguageUnderstanding.ResponseStrategy.Strategies;
using MoviesBot.MovieDB.Genres;
using MoviesBot.MovieDB.Movies;
using NUnit.Framework;

namespace MoviesBot.Tests.LanguageUnderstanding.ResponseStrategy
{
    [TestFixture]
    public class ResponseStrategySelectorTests
    {
        [Test]
        public void GetResponse_NoResponseStrategies_ThrowsNoMatchingResponseStrategyException()
        {
            var strategies = new List<IResponseStrategy>();
            
            var moviesBotIntent = new MoviesBotIntent()
            {
                Intents = new Dictionary<MoviesBotIntent.Intent, IntentScore>
                {
                    {MoviesBotIntent.Intent.None, new IntentScore {Score = 0.99}},
                    {MoviesBotIntent.Intent.GetMovies, new IntentScore {Score = 0.01}}
                },
                Text = "This is not a recognised sentence",
                Entities = new MoviesBotIntent._Entities()
            };
            
            var responseStrategySelector = new ResponseStrategySelector(strategies);
            
            Assert.Throws<NoMatchingResponseStrategyException>(
                () => responseStrategySelector.GetStrategy(moviesBotIntent));
        }
        
        [Test]
        public void GetResponse_IntentOfNone_ReturnsDefaultResponseStrategy()
        {
            var strategies = new List<IResponseStrategy>()
            {
                new DefaultResponseStrategy(),
                new GetMoviesResponseStrategy(Mock.Of<IMoviesClient>(), Mock.Of<IGenresClient>())
            };
            
            var moviesBotIntent = new MoviesBotIntent()
            {
                Intents = new Dictionary<MoviesBotIntent.Intent, IntentScore>
                {
                    {MoviesBotIntent.Intent.None, new IntentScore {Score = 0.99}},
                    {MoviesBotIntent.Intent.GetMovies, new IntentScore {Score = 0.01}}
                },
                Text = "This is not a recognised sentence",
                Entities = new MoviesBotIntent._Entities()
            };
            
            var responseStrategySelector = new ResponseStrategySelector(strategies);

            var result = responseStrategySelector.GetStrategy(moviesBotIntent);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<DefaultResponseStrategy>());
            Assert.That(result.Intent, Is.EqualTo(MoviesBotIntent.Intent.None));
        }
        
        [Test]
        public void GetResponse_IntentOfGetMovies_ReturnsGetMoviesResponseStrategy()
        {
            var strategies = new List<IResponseStrategy>()
            {
                new DefaultResponseStrategy(),
                new GetMoviesResponseStrategy(Mock.Of<IMoviesClient>(), Mock.Of<IGenresClient>())
            };
            
            var moviesBotIntent = new MoviesBotIntent()
            {
                Intents = new Dictionary<MoviesBotIntent.Intent, IntentScore>
                {
                    {MoviesBotIntent.Intent.None, new IntentScore {Score = 0.01}},
                    {MoviesBotIntent.Intent.GetMovies, new IntentScore {Score = 0.99}}
                },
                Text = "Horror Movies from 1999",
                Entities = new MoviesBotIntent._Entities()
            };
            
            var responseStrategySelector = new ResponseStrategySelector(strategies);

            var result = responseStrategySelector.GetStrategy(moviesBotIntent);
            
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<GetMoviesResponseStrategy>());
            Assert.That(result.Intent, Is.EqualTo(MoviesBotIntent.Intent.GetMovies));
        }
    }
}