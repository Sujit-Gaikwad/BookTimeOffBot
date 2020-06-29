using System;
using System.Collections.Generic;
using System.Text;

namespace CongnetiveServiceLibrary
{
   class LuisScoreData
    {
        public TopScoreingIntent topScoringIntent { get; set; }
        public List<IntentData> intents { get; set; }
        public List<Entity> entities { get; set; }
    }

    class TopScoreingIntent
    {
        public string intent { get; set; }
        public double score { get; set; }

    }

    class IntentData
    {
        public string intent { get; set; }
        public string score { get; set; }
    }

    class Entity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public Resolution resolution { get; set; }
    }

    class Resolution
    {
      public  string[] values;
    }
}
