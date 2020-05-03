using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Warframe
{
    public class DepotReliques
    {

        private DepotItems depotItems;

        private SQLiteAsyncConnection dbConn
        {
            get
            {
                return depotItems.dbConn;
            }
        }

        public DepotReliques(DepotItems depotItems)
        {
            this.depotItems = depotItems;
            dbConn.CreateTableAsync<Relique>();
        }

        public async Task<IEnumerable<Relique>> AllReliques()
        {
            List<Relique> lst = await dbConn.Table<Relique>().ToListAsync();
            if (lst.Count == 0)
            {
                List<Relique> reliques = await GetAllReliques();
                await dbConn.InsertAllAsync(reliques);
                lst.AddRange(reliques);
            }
            return lst;
        }

        public async Task<List<Relique>> GetAllReliques()
        {
            IEnumerable<Item> primes = await depotItems.AllItems();
            List<Relique> res = new List<Relique>();
            foreach(Item item in primes)
            {
                List<Relique> reliques = await WMGetter.GetReliques(item);
                foreach(Relique relique in reliques)
                {
                    if (res.Contains(relique))
                    {
                        res[res.IndexOf(relique)].Add(relique);
                    }
                    else
                    {
                        res.Add(relique);
                    }
                }
            }
            res = res.OrderByDescending(i => i.AveragePrice).ToList();
            return res;
        }
    }
}
