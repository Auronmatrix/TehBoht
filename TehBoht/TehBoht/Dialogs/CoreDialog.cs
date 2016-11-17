using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TehBoht.Dialogs
{
    using System.Threading.Tasks;

    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using Microsoft.Bot.Connector;

    using TehBoht.Messages;

    [LuisModel("b79fe466-5608-44af-ab46-29035e977d3b", "f91ac2dff7454de3a6edbd36210c6cdc")]
    [Serializable]
    public class CoreDialog: LuisDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(this.MessageReceived);
        }
        
        [LuisIntent("")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            var message = $"Sorry I did not understand: " + string.Join(", ", result.Intents.Select(i => i.Intent));
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Greeting")]
        public async Task Greeting(IDialogContext context, LuisResult result)
        {
            var message = $"Servus! Im TehBoht. How can I help?";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }

        [LuisIntent("Help")]
        public async Task Help(IDialogContext context, LuisResult result)
        {
            var message = $"I'm programmed to try and understand the following intents from your speach: {string.Join("\n", result.Intents.Select(i => i.Intent))}. \n Try asking me something along those lines";
            await context.PostAsync(message);
            context.Wait(MessageReceived);
        }
        
        [LuisIntent("GetContactDetails")]
        public async Task GetContactDetails(IDialogContext context, LuisResult result)
        {
            var contactMessage = MessageFactory.CreateContactMessage(context);
            await context.PostAsync(contactMessage);
            context.Wait(MessageReceived);
        }

        [LuisIntent("StartMediaDialog")]
        public async Task StartMediaDialog(IDialogContext context, LuisResult result)
        {
            var form = Chain.From(() => FormDialog.FromForm(MediaCompassDialog.BuildForm, FormOptions.PromptInStart));
            context.Call(form, ResumeAfterMediaCompass);
        }

        private async Task ResumeAfterMediaCompass(IDialogContext context, IAwaitable<MediaCompassDialog> result)
        {
            await context.PostAsync($"Be cool, stay in school");
            context.Wait(MessageReceived);
        }
    }
}