using CongnetiveServiceLibrary;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserPropertiesLibrary;
using BookTimeOffBot.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using BusinessEntities;
using System.IO;
using Microsoft.Bot.Schema;
using AdaptiveCards;
using BusinessLogic;

namespace BookTimeOffBot.Dialogs
{
    public class MainDialog : ComponentDialog
    {
        IStatePropertyAccessor<UserData> statePropertyAccessor;
        LuisData luisData = new LuisData();
        ILogger logger;
        public MainDialog(UserState user,ConversationState conversationState,ILogger<MainDialog> logger) : base(nameof(MainDialog))
        {

            AddDialog(new TimeOfBalanceDialog(user));
            AddDialog(new BookTimeOffDialog(user));
            statePropertyAccessor = user.CreateProperty<UserData>(nameof(UserData));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog),new WaterfallStep[]
                {
                ActionAsync
            }));

            InitialDialogId = nameof(WaterfallDialog);
        }
    
        private async Task<DialogTurnResult> ActionAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        
        {
            var activity = stepContext.Context.Activity;

            if (activity.Value != null)
            {
                var Value = JsonConvert.DeserializeObject<JObject>(activity.Value.ToString());
                var ActionValue = Value["Action"];
                if (ActionValue != null)
                {
                    var state = await stepContext.ContinueDialogAsync();
                }
                else
                {
                    ImBackData imBackData = JsonConvert.DeserializeObject<ImBackData>(activity.Value.ToString());
                    activity.Text = imBackData.msteams.value;
                }
            }

            var topScoringintent = LuisData.LuisResult(activity.Text);

            switch (topScoringintent)
            {
                case "Cancel":
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text("Cancelling All dialogs please restart the flow"));
                    return await stepContext.CancelAllDialogsAsync(cancellationToken);
                case IntentToken.PTOApply:
                    return await stepContext.ReplaceDialogAsync(nameof(BookTimeOffDialog));
                case IntentToken.PTOBalance:
                    return await stepContext.ReplaceDialogAsync(nameof(TimeOfBalanceDialog));
                default:
                    string path = TimeOffBL.GetPath();
                    string WelcomeCard = File.ReadAllText(path + "\\Cards\\NoneIntent.json");
                    await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(SendCards.SendAdaptiveCard(WelcomeCard)), cancellationToken);
                    return await stepContext.CancelAllDialogsAsync(cancellationToken);
            }

        }

    }
}
