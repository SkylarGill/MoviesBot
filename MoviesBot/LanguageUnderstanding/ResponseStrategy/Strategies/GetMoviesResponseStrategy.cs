using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MoviesBot.LanguageUnderstanding.Model;
using MoviesBot.MovieDB.Genres;
using MoviesBot.MovieDB.Genres.Response;
using MoviesBot.MovieDB.Movies;
using MoviesBot.MovieDB.Movies.Request;
using MoviesBot.MovieDB.Movies.Response;

namespace MoviesBot.LanguageUnderstanding.ResponseStrategy.Strategies
{
    public class GetMoviesResponseStrategy : IResponseStrategy
    {
        private readonly IMoviesClient _moviesClient;
        private readonly IGenresClient _genresClient;
        
        public MoviesBotIntent.Intent Intent => MoviesBotIntent.Intent.GetMovies;

        public GetMoviesResponseStrategy(IMoviesClient moviesClient, IGenresClient genresClient)
        {
            _moviesClient = moviesClient;
            _genresClient = genresClient;
        }
        
        public async Task<string> GetResponseMessageAsync(MoviesBotIntent moviesBotIntent)
        {
            var genre = await GetGenre(moviesBotIntent).ConfigureAwait(false);
            var year = GetYear(moviesBotIntent);
            var movies = await _moviesClient.GetMoviesAsync(new MoviesRequest(genre?.Id.ToString(), year)).ConfigureAwait(false);
            return await GetFormattedMessage(genre, year, movies).ConfigureAwait(false);
        }

        private async Task<string> GetFormattedMessage(Genre genre, string year, IEnumerable<Movie> movies)
        {
            var stringBuilder = new StringBuilder("Here are movies");
            
            if (!string.IsNullOrEmpty(genre?.Name))
            {
                stringBuilder.Append($" with genre {genre.Name}");
            }
            
            if (!string.IsNullOrEmpty(year))
            {
                stringBuilder.Append($" from year {year}");
            }

            stringBuilder.Append(":\r\n");

            foreach (var movie in movies)
            {
                var movieGenres = await GetMovieGenres(movie).ConfigureAwait(false);

                var genresString = string.Join(", ", movieGenres);
                
                stringBuilder.Append($"{movie.Title}");
                stringBuilder.Append($" - Genres: {genresString}");
                stringBuilder.Append($" - Rating: {movie.VoteAverage}/10");
                stringBuilder.Append("\r\n");
            }

            return stringBuilder.ToString();
        }

        private async Task<List<string>> GetMovieGenres(Movie movie)
        {
            var movieGenres = new List<string>();
            foreach (var genreId in movie.GenreIds)
            {
                var genreName = await GetGenreNameFromId(genreId).ConfigureAwait(false);
                movieGenres.Add(genreName);
            }

            return movieGenres;
        }

        private static string GetYear(MoviesBotIntent moviesBotIntent)
        {
            var yearRegex = new Regex("[0-9]{4}");
            var yearValue = GetYearFromIntent(moviesBotIntent) ?? string.Empty;
            var matches = yearRegex.Matches(yearValue);
            return matches.FirstOrDefault()?.Value;
        }

        private async Task<Genre> GetGenre(MoviesBotIntent moviesBotIntent)
        {
            if (!GenreIsPopulated(moviesBotIntent))
            {
                return null;
            }

            var genreNameFromIntent = GetGenreFromIntent(moviesBotIntent);
            var genres = await _genresClient.GetGenresAsync().ConfigureAwait(false);
            var genreMatch = genres.FirstOrDefault(genre =>
                string.Equals(genre.Name, genreNameFromIntent, StringComparison.InvariantCultureIgnoreCase));
            return genreMatch;
        }

        private async Task<string> GetGenreNameFromId(int genreId)
        {
            var genres = await _genresClient.GetGenresAsync().ConfigureAwait(false);
            return genres.FirstOrDefault(genre => genre.Id == genreId)?.Name;
        }
         
        private static string GetGenreFromIntent(MoviesBotIntent moviesBotIntent) => 
            moviesBotIntent.Entities.genre.FirstOrDefault();
        
        private static string GetYearFromIntent(MoviesBotIntent moviesBotIntent) => 
            moviesBotIntent.Entities.datetime?.FirstOrDefault()?.ToString();

        private static bool GenreIsPopulated(MoviesBotIntent moviesBotIntent) => 
            !string.IsNullOrEmpty(moviesBotIntent.Entities.genre?.FirstOrDefault());
    }
}