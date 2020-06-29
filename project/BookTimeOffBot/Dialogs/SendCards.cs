using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookTimeOffBot.Dialogs
{
    public class SendCards
    {
        public static Attachment SendAdaptiveCard(string cardJson)
        {
            Attachment attachment = new Attachment()
            {
                ContentType = AdaptiveCard.ContentType,
                Content = JObject.FromObject(JsonConvert.DeserializeObject(cardJson))
            };
            return attachment;
        }
    }
}
