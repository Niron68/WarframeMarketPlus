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


    public class WMItemInfo
    {
        public ItemInfoPayload payload { get; set; }
    }

    public class ItemInfoPayload
    {
        public ItemInfo item { get; set; }
    }

    public class ItemInfo
    {
        public string id { get; set; }
        public Items_In_Set[] items_in_set { get; set; }
    }

    public class Items_In_Set
    {
        public int ducats { get; set; }
        public string id { get; set; }
        public bool set_root { get; set; }
        public int mastery_level { get; set; }
        public string icon { get; set; }
        public Pt pt { get; set; }
        public string sub_icon { get; set; }
        public Ko ko { get; set; }
        public int trading_tax { get; set; }
        public Ru ru { get; set; }
        public Fr fr { get; set; }
        public En en { get; set; }
        public string icon_format { get; set; }
        public Zh zh { get; set; }
        public string url_name { get; set; }
        public string[] tags { get; set; }
        public Sv sv { get; set; }
        public De de { get; set; }
        public string thumb { get; set; }
    }

    public class Pt
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

    public class Drop
    {
        public string name { get; set; }
        public object link { get; set; }
    }

    public class Ko
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

    public class Ru
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

    public class Fr
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

    public class En
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

    public class Zh
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

    public class Sv
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

    public class De
    {
        public string item_name { get; set; }
        public string description { get; set; }
        public string wiki_link { get; set; }
        public Drop[] drop { get; set; }
    }

}
