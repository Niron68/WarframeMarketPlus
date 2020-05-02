using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Warframe
{
    public static class PriceGetter
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

    }
}
