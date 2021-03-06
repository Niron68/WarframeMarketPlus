﻿using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Warframe
{

    public enum Era { Lith, Meso, Neo, Axi, Requiem }

    public enum Rarity { Common = 17, Uncommon = 20, Rare = 10, Erreur = 0 }

    [Table("relique")]
    public class Relique
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }

        [NotNull]
        public Era Ere { get; set; }

        public string Name { get; set; }

        public Dictionary<Item, Rarity> Loot { get; set; }

        public bool Ducats { get; set; }

        [Ignore]
        public float AverageDucats
        {
            get
            {
                float res = 0;
                foreach(KeyValuePair<Item, Rarity> item in Loot)
                {
                    res += item.Key.Ducats * (int) item.Value;
                }
                return res / 101;
            }
        }

        [Ignore]
        public float AveragePrice
        {
            get
            {
                float res = 0;
                foreach(KeyValuePair<Item, Rarity> item in Loot)
                {
                    res += item.Key.MinPrice * (int) item.Value;
                }
                return res / 101;
            }
        }

        public Relique(Era era, string name)
        {
            Ere = era;
            Name = name;
            Ducats = false;
            Loot = new Dictionary<Item, Rarity>();
        }

        public Relique()
        {
            Ere = Era.Lith;
            Name = "";
            Loot = new Dictionary<Item, Rarity>();
        }

        public void Add(Item item, Rarity rarity)
        {
            if (!Loot.ContainsKey(item))
            {
                Loot.Add(item, rarity);
            }
        }

        public void Add(Relique relique)
        {
            if (this.Equals(relique))
            {
                foreach(KeyValuePair<Item, Rarity> loot in relique.Loot)
                {
                    if (!Loot.ContainsKey(loot.Key))
                    {
                        Loot.Add(loot.Key, loot.Value);
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            bool res = false;
            if(obj is Relique relique)
            {
                res = this.Ere == relique.Ere && this.Name == relique.Name;
            }
            return res;
        }
    }
}
