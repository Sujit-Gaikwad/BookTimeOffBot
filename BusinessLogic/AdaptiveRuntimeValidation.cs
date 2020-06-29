using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using BusinessEntities;
using System.IO;

namespace BusinessLogic
{
    public class AdaptiveRuntimeValidation
    {
        public static string DateCardValidation(string value)
        {
            var Data = JsonConvert.DeserializeObject<JObject>(value);
             string startDate = Data["StartDate"].ToString();
             string endDate = Data["EndDate"].ToString();
             string ptoReason = Data["Reason"].ToString();
             var path = TimeOffBL.GetPath();
             string cardData = File.ReadAllText(path + "\\Cards\\PTODateTimeCard.json");
             PTODateTimeCard pTODateTimeCard = JsonConvert.DeserializeObject<PTODateTimeCard>(cardData);
             if(!string.IsNullOrEmpty(startDate)&&!string.IsNullOrEmpty(endDate)&&!string.IsNullOrEmpty(ptoReason))
                {
                    DateTime startdateTime = Convert.ToDateTime(startDate).Date;
                    DateTime enddateTime = Convert.ToDateTime(endDate).Date;
                    if(startdateTime < enddateTime)
                    {
                        if(startdateTime<DateTime.Now.Date)
                        {
                            pTODateTimeCard.body[8].isVisible = true;
                            pTODateTimeCard.body[2].columns[0].items[0].value = endDate;
                            pTODateTimeCard.body[4].value = ptoReason;
                            cardData = JsonConvert.SerializeObject(pTODateTimeCard);
                            return cardData;
                        }
                        else 
                        {
                            return null;
                        }
                    }
                    else
                    {
                        pTODateTimeCard.body[7].isVisible = true;
                        cardData = JsonConvert.SerializeObject(pTODateTimeCard);
                        pTODateTimeCard.body[1].columns[0].items[0].value = startDate;
                        pTODateTimeCard.body[4].value = ptoReason;
                        return cardData;
                    }
                }
                else if((string.IsNullOrEmpty(startDate) || string.IsNullOrEmpty(endDate)) && !string.IsNullOrEmpty(ptoReason))
                {
                    pTODateTimeCard.body[6].isVisible = true;
                    pTODateTimeCard.body[4].value = ptoReason;
                    cardData = JsonConvert.SerializeObject(pTODateTimeCard);
                    return cardData;
                }
                else
                {
                    pTODateTimeCard.body[5].isVisible = true;
                    cardData = JsonConvert.SerializeObject(pTODateTimeCard);
                    return cardData;
                }
            }

        }
    }

