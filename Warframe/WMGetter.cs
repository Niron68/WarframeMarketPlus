using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Warframe
{
    public static class WMGetter
    {
        private static string BaseLink = "https://api.warframe.market/v1/items/";

        public async static Task<int> GetPrice(string item)
        {
            int res = 0;
            string link = BaseLink + item + "/orders";
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(link);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                Orders orders = JsonConvert.DeserializeObject<Orders>(content, settings);
                List<Order> ingameOrders =
                    (from el in orders.payload.orders
                    where el.user.status == "ingame"
                    select el).ToList();
                List<Order> sellOrder =
                    (from el in ingameOrders
                     where el.order_type == "sell"
                     select el).ToList();
                Order order = sellOrder.OrderBy(i => i.platinum).ToList().First();
                res = (int) order.platinum;
            }
            return res;
        }

        public async static Task<Dictionary<string, string>> GetAllPrime()
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync("https://api.warframe.market/v1/items");
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                WMItems wMItems = JsonConvert.DeserializeObject<WMItems>(content, settings);
                List<WMItem> prime =
                    (from it in wMItems.payload.items
                     where it.item_name.ToLower().Contains(" prime ") && !it.item_name.ToLower().Contains(" set")
                     select it).ToList();
                res = prime.ToDictionary(i => i.item_name, i => i.url_name);
            }
            return res;
        }

        public async static Task<List<Relique>> GetReliques(Item item)
        {
            List<Relique> res = new List<Relique>();
            string link = BaseLink + item.MarketName;
            using (HttpClient client = new HttpClient())
            {
                string content = await client.GetStringAsync(link);
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    MissingMemberHandling = MissingMemberHandling.Ignore
                };
                WMItemInfo info = JsonConvert.DeserializeObject<WMItemInfo>(content, settings);
                Items_In_Set item_set =
                    (from it in info.payload.item.items_in_set
                     where it.url_name == item.MarketName
                     select it).First();
                foreach(Drop drop in item_set.en.drop)
                {
                    string[] dropInfo = drop.name.Split(' ');
                    Relique relique = new Relique((Era) Enum.Parse(typeof(Era), dropInfo[0]), dropInfo[1]);
                    relique.Add(item, (Rarity)Enum.Parse(typeof(Rarity), dropInfo[2]));
                    res.Add(relique);
                }
            }
            return res;
        }

        public async static Task<string> Test()
        {
            string res = "";
            using(HttpClient client = new HttpClient())
            {
                string link = BaseLink + "nova_prime_chassis/statistics";
                res = await client.GetStringAsync(link);
            }
            return res;
        }

    }
}
