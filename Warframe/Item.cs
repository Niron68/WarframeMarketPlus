using SQLite;
using System;
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
            //InitializePrice();
        }

        public static async Task<Item> Create(string name, string url)
        {
            Item item = new Item(name, url);
            item.MinPrice = await PriceGetter.GetPrice(item.MarketName);
            return item;
        }

        public async void InitializePrice()
        {
            MinPrice = await PriceGetter.GetPrice(MarketName);
        }
    }
}
