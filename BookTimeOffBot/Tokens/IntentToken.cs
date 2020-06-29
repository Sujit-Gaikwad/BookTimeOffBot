using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BookTimeOffBot.Tokens
{

    public class IntentToken
    {
        public List<string> Intents = new List<string>();

        public IntentToken()
        {
            Intents.Add("PTOBalance");
            Intents.Add("PTOApply");
            Intents.Add("None");
       }

        public const string PTOBalance = "PTOBalance";
        public const string PTOApply = "PTOApply";
        public const string None = "None";
    }
}
