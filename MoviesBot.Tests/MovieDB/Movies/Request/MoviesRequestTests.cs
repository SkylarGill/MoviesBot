using MoviesBot.MovieDB.Movies.Request;
using NUnit.Framework;

namespace MoviesBot.Tests.MovieDB.Movies.Request
{
    [TestFixture]
    public class MoviesRequestTests
    {
        [Test]
        public void GetQueryParamsDictionary_WithGenre_ContainsKeyAndValue()
        {
            var moviesRequest = new MoviesRequest("123");

            var queryParams = moviesRequest.GetQueryParamsDictionary();
            
            Assert.That(queryParams, Contains.Key("with_genres"));
            Assert.That(queryParams, Contains.Value("123"));
        }
        
        [Test]
        public void GetQueryParamsDictionary_WithYear_ContainsKeyAndValue()
        {
            var moviesRequest = new MoviesRequest(null, "1999");

            var queryParams = moviesRequest.GetQueryParamsDictionary();
            
            Assert.That(queryParams, Contains.Key("year"));
            Assert.That(queryParams, Contains.Value("1999"));
        }
        
        [Test]
        public void GetQueryParamsDictionary_WithNoGenreOrYear_ContainsExpectedKeysAndValues()
        {
            var moviesRequest = new MoviesRequest();

            var queryParams = moviesRequest.GetQueryParamsDictionary();
            
            Assert.That(queryParams, Contains.Key("sort_by"));
            Assert.That(queryParams, Contains.Value("popularity.desc"));
            
            Assert.That(queryParams, Contains.Key("region"));
            Assert.That(queryParams, Contains.Value("GB"));
        }
    }
}