using BusinessLogic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserPropertiesLibrary;

namespace BookTimeOffBot.Dialogs
{
    public class TimeOfBalanceDialog : ComponentDialog
    {
        IStatePropertyAccessor<UserData> statePropertyAccessor;
        ILogger logger;
        TimeOffBL timeoffBL = new TimeOffBL();
        public TimeOfBalanceDialog(UserState userState) : base(nameof(TimeOfBalanceDialog))
        {
            statePropertyAccessor = userState.CreateProperty<UserData>(nameof(UserData));
            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
                {
                GetTimeOffBalance
            }));
            InitialDialogId = nameof(WaterfallDialog);
        }

        private async Task<DialogTurnResult> GetTimeOffBalance(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var resposne  = timeoffBL.GetTimeOffbalance();
            await stepContext.Context.SendActivityAsync(MessageFactory.Text(resposne), cancellationToken);
            return await stepContext.CancelAllDialogsAsync();
            
        }
    }
}
