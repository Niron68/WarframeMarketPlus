﻿using SQLite;
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
        private SQLiteAsyncConnection dbConn;

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
                List<Item> items = new List<Item>();
                Dictionary<string, string> primes = await PriceGetter.GetAllPrime();
                foreach (KeyValuePair<string, string> item in primes)
                {
                    items.Add(await Item.Create(item.Key, item.Value));
                }
                items = items.OrderByDescending(i => i.MinPrice).ToList();
                await dbConn.InsertAllAsync(items);
                lst.AddRange(items);
            }
            //else
            //{
            //    foreach(var el in lst)
            //    {
            //        await dbConn.DeleteAsync(el);
            //    }
            //}
            lst = lst.OrderByDescending(i => i.MinPrice).ToList();
            return lst;
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
            await AddItem(item);
        }
    }
}
