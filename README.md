# Bot Framework Movie DB example

This is an example of a chatbot utilising the [Microsoft Bot Framework](https://dev.botframework.com/ "Microsoft Bot Framework").


## Functionality
The bot currently only has basic functionality.
It can:
- Process user input into queries using Azure Language Understanding Service (LUIS)
- List movies sourced from [The Movie DB API](https://www.themoviedb.org/documentation/api, "The Movie DB API Documentation")
- Filter movies based on genre and year of release input by the user
- Integrated web chat for demonstration purposes

## Prerequisites
- .NET Core 2.1
- The Movie DB account
- Microsoft Azure account

## Setup

Setup a LUIS Application. A guide of how to do this can be found [here.](https://docs.microsoft.com/en-us/azure/cognitive-services/luis/luis-how-to-start-new-app, "LUIS App Setup")
The LUIS Application ID and API Key then need to be added to the `appsettings.json` file as `LuisAppId` and `LuisAPIKey` respectively.

Apply for an API key for The Movie DB. [You can apply on this page.](https://www.themoviedb.org/settings/api)
Add this API key to the `appsettings.json` of the solution under the key `MovieDbApiKey`.

The web chat feature requires the Bot to be published with Azure. Add the registered Bot name and the secret that Azure provides for the 'Web Chat' channel to the `appsettings.json` file under the `BotName` and `WebChatSecret` keys respectively.


```json
{
  "MicrosoftAppId": "",
  "MicrosoftAppPassword": "",
  "LuisAPIHostName":"westeurope.api.cognitive.microsoft.com",
  "LuisAPIKey":"YOUR_LUIS_API_KEY_GOES_HERE",
  "LuisAppId":"YOUR_LUIS_APP_ID_HERE",
  "MovieDbBaseUri":"api.themoviedb.org/3",
  "MovieDbApiKey":"YOUR_MOVIE_DB_API_KEY_HERE",
  "MovieDbMoviesEndpointPath":"/discover/movie",
  "MovieDbGenresEndpointPath":"/genre/movie/list",
  "BotName":"YOUR_AZURE_PUBLISHED_BOT_NAME_HERE",
  "WebChatSecret":"YOUR_AZURE_WEB_CHAT_CHANNEL_SECRET_HERE"
}
```

When launching the app, the `launchsettings.json` is set to use port 3979 for HTTPS and port 3978 for HTTP.
