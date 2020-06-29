using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CongnetiveServiceLibrary
{
    class LuisEndpointOperations
    {
      internal  static async Task<LuisScoreData> GetLuisEndpointScore(string utterances)
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(String.Empty);
            
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", AppKeys.subscriptionId);

            LuisScoreData luisScoreData = null;

             queryString["q"] = utterances;
             queryString["timezoneOffset"] = "0";
             queryString["verbose"] = "true";
             queryString["spellCheck"] = "false";
             queryString["staging"] = "false";

               var endPoint = "https://westus.api.cognitive.microsoft.com/luis/v2.0/apps/" + AppKeys.appId + "?" + queryString;
        
                try
                  {
                      var response = await client.GetAsync(endPoint);
                     if (response.StatusCode == System.Net.HttpStatusCode.OK)
                     {
                       var strResponseContent = await response.Content.ReadAsStringAsync();
                       luisScoreData = JsonConvert.DeserializeObject<LuisScoreData>(strResponseContent);
                     }
                     else
                     {
                        throw new HttpRequestException();
                     }
                  }
                catch (Exception e)
                 {
                     Console.WriteLine(e.Message);
                 }
            return luisScoreData;
        }
    }
}
