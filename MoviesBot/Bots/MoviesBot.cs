// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.6.2

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;

namespace MoviesBot.Bots
{
    public class MoviesBot : ActivityHandler
    {
        private readonly IMessageHandler _messageHandler;

        public MoviesBot(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            var replyText = _messageHandler.GetMessageResponse(turnContext, cancellationToken);
            await turnContext.SendActivitiesAsync(
                new IActivity[]
                {
                    new Activity(ActivityTypes.Typing),
                    MessageFactory.Text(await replyText.ConfigureAwait(false), await replyText.ConfigureAwait(false)),
                }, cancellationToken);
        }

        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            const string welcomeText = "Hello, ask me about movies.";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }
    }
}
