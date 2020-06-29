using System;

namespace CongnetiveServiceLibrary
{
    public class LuisData
    {
        

        public static string LuisResult(string userInput)
        {
            var data = LuisEndpointOperations.GetLuisEndpointScore(userInput).GetAwaiter().GetResult();
           string  entityValue = string.Empty;
            string topIntentName = string.Empty;


                //if (data.entities.Count > 0)
                //{
                //        foreach (var entity in data.entities)
                //        {
                //          if(entity.type == "builtin.personName" && data.topScoringIntent.intent == "GetUserName")
                //            {
                //                entityValue = entity.entity;
                //                break;
                //            }
                //          else if (entity.type=="UserMoves" && data.topScoringIntent.intent=="UserChoice")
                //            {
                //                entityValue = entity.resolution.values[0];
                //                break;
                //            }
                //          else
                //            {
                //                entityValue = entity.entity;
                //            }
                           
                //        }
                //}

          topIntentName =   data.topScoringIntent.score < 0.35 ? "None" : data.topScoringIntent.intent;
            return topIntentName;
        }

    }
}
