// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesBot.Bots;
using MoviesBot.LanguageUnderstanding;
using MoviesBot.LanguageUnderstanding.ResponseStrategy;
using MoviesBot.LanguageUnderstanding.ResponseStrategy.Strategies;
using MoviesBot.MovieDB.Endpoint;
using MoviesBot.MovieDB.Genres;
using MoviesBot.MovieDB.Movies;

namespace MoviesBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();
            
            services.AddSingleton<IMessageHandler, MessageHandler>();
            services.AddSingleton<IRecognizer, MoviesRecognizer>();
            services.AddSingleton<IResponseStrategySelector, ResponseStrategySelector>();
            services.AddSingleton<IResponseStrategy, DefaultResponseStrategy>();
            services.AddSingleton<IResponseStrategy, GetMoviesResponseStrategy>();
            services.AddSingleton<IMoviesClient, MoviesClient>(GetMoviesClient);
            services.AddSingleton<IGenresClient, GenresClient>(GetGenresClient);

            services.AddHttpClient();

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, Bots.MoviesBot>();
        }

        private MoviesClient GetMoviesClient(IServiceProvider provider)
        {
            var configuration = provider.GetService<IConfiguration>();
            var movieDbEndpoint = new MovieDbEndpoint(
                configuration["MovieDbBaseUri"],
                configuration["MovieDbMoviesEndpointPath"],
                configuration["MovieDbApiKey"]);
            var httpClientFactory = provider.GetService<IHttpClientFactory>();
            
            return new MoviesClient(httpClientFactory, movieDbEndpoint);
        }
        
        private GenresClient GetGenresClient(IServiceProvider provider)
        {
            var configuration = provider.GetService<IConfiguration>();
            var movieDbEndpoint = new MovieDbEndpoint(
                configuration["MovieDbBaseUri"],
                configuration["MovieDbGenresEndpointPath"],
                configuration["MovieDbApiKey"]);
            var httpClientFactory = provider.GetService<IHttpClientFactory>();
            
            return new GenresClient(movieDbEndpoint, httpClientFactory);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();
            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
