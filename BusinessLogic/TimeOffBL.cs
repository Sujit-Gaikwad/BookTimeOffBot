using System;
using System.Dynamic;
using System.IO;
using System.Reflection;

namespace BusinessLogic
{
    public class TimeOffBL
    {

        public static string GetPath()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            int index = path.IndexOf("\\bin");
            path = path.Substring(0, index);
            return path;
        }
        public string GetTimeOffbalance()
        {
            //Integration Proxy call goes here
            Random random = new Random();
            int leaves = random.Next(500);
            return $"You have {leaves} hours left";
        }
    }
}
