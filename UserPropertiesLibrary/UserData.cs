using System;
using System.Globalization;

namespace UserPropertiesLibrary
{
    public class UserData
    {
        public string UserName { get; set; }
        public PTOData PTOData { get; set; }
        public string userID { get; set; }
    }
    public class PTOData
    {
        public string StartDate { get; set; }
        public string userLeaveBalance { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }

    }
}
