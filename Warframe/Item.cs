using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Warframe
{
    [Table("item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string MarketName { get; set; }

        public int MinPrice { get; set; }

        public int Ducats { get; set; }
        
        [TextBlob(nameof(ReliquesBlobbed))]
        public List<string> Reliques { get; set; }

        private string ReliquesBlobbed;

        [Ignore]
        public string Price
        {
            get
            {
                return "Price : " + MinPrice;
            }

        }

        public Item()
        {

        }

        public Item(string name, string url)
        {
            Name = name;
            MarketName = url;
            Reliques = new List<string>();
            //InitializePrice();
        }

        public static async Task<Item> Create(string name, string url)
        {
            Item item = new Item(name, url);
            item.MinPrice = await WMGetter.GetPrice(item.MarketName);
            item.Ducats = await WMGetter.GetDucats(item.MarketName);
            var list = await WMGetter.GetReliques(item);
            foreach (var it in list)
            {
                item.Reliques.Add(it.Ere.ToString() + " " + it.Name + " " + it.Loot.First().Value.ToString());
            }
            return item;
        }

        public async void InitializePrice()
        {
            MinPrice = await WMGetter.GetPrice(MarketName);
        }

        public override bool Equals(object obj)
        {
            bool res = false;
            if(obj is Item it)
            {
                res = it.MarketName == this.MarketName;
            }
            return res;
        }

    }
}
