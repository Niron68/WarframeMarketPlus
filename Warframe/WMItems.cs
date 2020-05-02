using System;
using System.Collections.Generic;
using System.Text;

namespace Warframe
{

    public class WMItems
    {
        public ItemPayload payload { get; set; }
    }

    public class ItemPayload
    {
        public WMItem[] items { get; set; }
    }

    public class WMItem
    {
        public string item_name { get; set; }
        public string url_name { get; set; }
        public string id { get; set; }
        public string thumb { get; set; }
    }

}
