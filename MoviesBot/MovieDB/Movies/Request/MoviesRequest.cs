using System.Collections.Generic;

namespace MoviesBot.MovieDB.Movies.Request
{
    public class MoviesRequest
    {
        private const string ReleaseDateCountryIsoCode = "GB";
        private const string SortBy = "popularity.desc";
        
        private readonly string _genreId;
        private readonly string _year;

        public MoviesRequest(string genreId = null, string year = null)
        {
            _genreId = genreId;
            _year = year;
        }

        public IDictionary<string, string> GetQueryParamsDictionary()
        {
            var queryParams = new Dictionary<string, string>()
            {
                {"sort_by", SortBy},
                {"region", ReleaseDateCountryIsoCode},
            };

            if (!string.IsNullOrEmpty(_genreId))
            {
                queryParams.Add("with_genres", _genreId);
            }
            
            if (!string.IsNullOrEmpty(_year))
            {
                queryParams.Add("year", _year);
            }

            return queryParams;
        }
    }
}