using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
   public class PTODateTimeCard
    {
              public string type { get; set; }
              
          public string version { get; set; }
          public List<Body> body { get; set; }
          public List<Action> actions { get; set; }
      
    }
    public class Item
    {
        public string type { get; set; }
        public string text { get; set; }
        public string weight { get; set; }
        public string id { get; set; }
        public string value { get; set; }

    }

    public class Column
    {
        public string type { get; set; }
        public string width { get; set; }
        public List<Item> items { get; set; }

    }

    public class Body
    {
        public string type { get; set; }
        public string text { get; set; }
        public string fontType { get; set; }
        public string size { get; set; }
        public string weight { get; set; }
        public List<Column> columns { get; set; }
        public string placeholder { get; set; }
        public string id { get; set; }
        public string value { get; set; }
        public bool? isVisible { get; set; }

    }

    public class Data
    {
        public string Action { get; set; }

    }

    public class Action
    {
        public string type { get; set; }
        public string title { get; set; }
        public string id { get; set; }
        public Data data { get; set; }

    }

}
