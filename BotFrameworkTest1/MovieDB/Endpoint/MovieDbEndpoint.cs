using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace BotFrameworkTest1.MovieDB.Endpoint
{
    public class MovieDbEndpoint : IMovieDbEndpoint
    {
        private readonly string _baseUrl;
        private readonly string _endpointPath;
        private readonly string _apiKey;

        public MovieDbEndpoint(string baseUrl, string endpointPath, string apiKey)
        {
            _baseUrl = baseUrl;
            _endpointPath = endpointPath;
            _apiKey = apiKey;
        }

        public Uri ConstructUri(IDictionary<string, string> queryParams = null)
        {
            if (queryParams == null)
            {
                queryParams = new Dictionary<string, string>();
            }
            
            queryParams.Add("api_key", _apiKey);
            var queryString = QueryString.Create(queryParams).Value;
            
            var uriBuilder = new UriBuilder
            {
                Host = _baseUrl, 
                Path = _endpointPath, 
                Query = queryString
            };

            return uriBuilder.Uri;
        }
    }
}