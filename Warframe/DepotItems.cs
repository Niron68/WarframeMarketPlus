using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warframe
{
    public class DepotItems
    {
        public SQLiteAsyncConnection dbConn;

        public DepotItems(string path)
        {
            dbConn = new SQLiteAsyncConnection(Path.Combine(path, "items.db3"));
            dbConn.CreateTableAsync<Item>();
        }

        public async Task<IEnumerable<Item>> AllItems()
        {
            List<Item> lst = await dbConn.Table<Item>().ToListAsync();
            if(lst.Count == 0)
            {
                List<Item> items = await GetAllPrime();
                await dbConn.InsertAllAsync(items);
                lst.AddRange(items);
            }
            //else
            //{
            //    foreach (var el in lst)
            //    {
            //        await dbConn.DeleteAsync(el);
            //    }
            //}
            //List<Item> lst2 = await dbConn.Table<Item>().ToListAsync();
            lst = lst.OrderByDescending(i => i.MinPrice).ToList();
            return lst;
        }

        private async Task<List<Item>> GetAllPrime()
        {
            List<Item> items = new List<Item>();
            Dictionary<string, string> primes = await WMGetter.GetAllPrime();
            List<Task> tasks = new List<Task>();
            foreach (KeyValuePair<string, string> prime in primes)
            {
                tasks.Add(Task.Run(async () =>
                {
                    items.Add(await Item.Create(prime.Key, prime.Value));
                }));
                //items.Add(await Item.Create(prime.Key, prime.Value));
            }
            Task.WaitAll(tasks.ToArray());
            items = items.OrderByDescending(i => i.MinPrice).ToList();
            return items;
        }

        public async Task<bool> AddItem(Item item)
        {
            bool res = false;
            if(item.Id == 0)
            {
                await dbConn.InsertAsync(item);
                res = true;
            }
            else
            {
                await dbConn.UpdateAsync(item);
            }
            return res;
        }

        public async Task RefreshItem(Item item)
        {
            await dbConn.UpdateAsync(item);
        }
    }
}
