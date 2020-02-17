using System;
using System.Collections.Generic;

namespace MoviesBot.MovieDB.Endpoint
{
    public interface IMovieDbEndpoint
    {
        Uri ConstructUri(IDictionary<string, string> queryParams = null);
    }
}