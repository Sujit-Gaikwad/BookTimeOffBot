using BusinessLogic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserPropertiesLibrary;

namespace BookTimeOffBot.Dialogs
{
    public class BookTimeOffDialog : ComponentDialog
    {
        IStatePropertyAccessor<UserData> statePropertyAccessor;
        ILogger logger;
        TimeOffBL timeoffBL = new TimeOffBL();

       public  BookTimeOffDialog(UserState userState) : base(nameof(BookTimeOffDialog))
        {
                statePropertyAccessor = userState.CreateProperty<UserData>(nameof(UserData));
                AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
                    {
                SendDateTimeCard,
                ValidateDateTime,
                Summary
                }));
                InitialDialogId = nameof(WaterfallDialog);
            }

        private async Task<DialogTurnResult> SendDateTimeCard(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string path = TimeOffBL.GetPath();
            string DateTimeCard = File.ReadAllText(path + "\\Cards\\PTODateTimeCard.json");
            await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(SendCards.SendAdaptiveCard(DateTimeCard)), cancellationToken);
            return EndOfTurn;
        }
        private async Task<DialogTurnResult> ValidateDateTime(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var activity = stepContext.Context.Activity;
            if (activity.Value != null)
            {
                var value = JsonConvert.DeserializeObject<JObject>(activity.Value.ToString());
                var Action = value["Action"].ToString();
                if(Action.Equals("validate&ApplyPTO"))
                {
                    var card = AdaptiveRuntimeValidation.DateCardValidation(stepContext.Context.Activity.Value.ToString()) ;
                    if(string.IsNullOrEmpty(card))
                    {
                        var userData = await statePropertyAccessor.GetAsync(stepContext.Context,() => new UserData());
                        var Data = JsonConvert.DeserializeObject<JObject>(activity.Value.ToString());
                        string startDate = Data["StartDate"].ToString();
                        string endDate = Data["EndDate"].ToString();
                        string ptoReason = Data["Reason"].ToString();
                        userData.PTOData = new PTOData { EndDate = endDate, StartDate = startDate, Reason = ptoReason };
                        await statePropertyAccessor.SetAsync(stepContext.Context, userData);
                        return await stepContext.NextAsync();
                    }
                    else
                    {
                        
                        await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(SendCards.SendAdaptiveCard(card)), cancellationToken);
                        stepContext.ActiveDialog.State["stepIndex"] = (int)stepContext.ActiveDialog.State["stepIndex"] - 1;
                        return EndOfTurn; 
                    }
                }
                //string DateTimeCard = File.ReadAllText(path + "\\Cards\\PTODateTimeCard.json");
                //await stepContext.Context.SendActivityAsync(MessageFactory.Attachment(SendCards.SendAdaptiveCard(DateTimeCard)), cancellationToken);
            }
            await stepContext.Context.SendActivityAsync(MessageFactory.Text("I don't understand this input cancelling this flow please restart again"), cancellationToken);
            return await stepContext.CancelAllDialogsAsync();
        }

        private async  Task<DialogTurnResult> Summary(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var userData = await statePropertyAccessor.GetAsync(stepContext.Context, () => new UserData());
            await stepContext.Context.SendActivityAsync(MessageFactory.Text($"Hi I Got you inputs PTO Start Date:{userData.PTOData.StartDate}, PTO end Date:{userData.PTOData.EndDate}, And Reason Of PTO {userData.PTOData.Reason}"), cancellationToken);
            await stepContext.EndDialogAsync();
            return await stepContext.CancelAllDialogsAsync();
        
        }


    }
}
