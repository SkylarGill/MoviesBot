using System;
using System.Collections.Generic;

namespace BotFrameworkTest1.MovieDB.Endpoint
{
    public interface IMovieDbEndpoint
    {
        Uri ConstructUri(IDictionary<string, string> queryParams = null);
    }
}