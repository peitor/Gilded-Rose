using System;
using System.Collections.Generic;

namespace GildedRose.Business
{
    public class Processor
    {
        public const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        public const string BackstagePasses = "Backstage passes to a TAFKAL80ETC concert";
        public const string AgedBrie = "Aged Brie";
        public const string Conjured = "Conjured";

        readonly Dictionary<string, Action<Item>> _qualityActions = new Dictionary<string, Action<Item>>
        {
            {
                BackstagePasses, item =>
                                    {
                                        TryIncreaseQuality(item);

                                        if (item.SellIn < 11)
                                        {
                                            TryIncreaseQuality(
                                                item);
                                        }

                                        if (item.SellIn < 6)
                                        {
                                            TryIncreaseQuality(
                                                item);
                                        }
                                    }
                },
            {
                AgedBrie, item =>
                            {
                                if (item.Quality < 50)
                                {
                                    item.Quality =
                                        item.Quality + 1;
                                }
                            }
                },
            {
                Conjured, item =>
                            {
                                TryDecreaseQuality(item);
                                TryDecreaseQuality(item);
                            }
                },
            {Sulfuras, item => { }}
        };

        readonly Dictionary<string, Action<Item>> _sellActions = new Dictionary<string, Action<Item>>
        {
            { BackstagePasses, item => { item.Quality = 0; }},
            { AgedBrie, TryIncreaseQuality},
            { Sulfuras, item => { }}
        };



        public void UpdateQuality(IEnumerable<Item> items)
        {
            if (items != null)
            {
                foreach (Item item in items)
                {
                    UpdateQuality(item);
                }
            }
        }

        void UpdateQuality(Item item)
        {
            ProcessQuality(item);

            if (item.Name != Sulfuras)
            {
                item.SellIn = item.SellIn - 1;
            }

            ProcessSellIn(item);
        }

        void ProcessSellIn(Item item)
        {
            if (item.SellIn < 0)
            {
                if (!string.IsNullOrEmpty(item.Name) && _sellActions.ContainsKey(item.Name))
                {
                    _sellActions[item.Name].Invoke(item);
                }
                else
                {
                    TryDecreaseQuality(item);
                }
            }
        }

        void ProcessQuality(Item item)
        {
            if (!string.IsNullOrEmpty(item.Name) && _qualityActions.ContainsKey(item.Name))
            {
                _qualityActions[item.Name].Invoke(item);
            }
            else
            {
                TryDecreaseQuality(item);
            }
        }


        static void TryIncreaseQuality(Item item)
        {
            if (item.Quality < 50)
            {
                item.Quality = item.Quality + 1;
            }
        }


        static void TryDecreaseQuality(Item item)
        {
            if (item.Quality > 0)
            {
                item.Quality = item.Quality - 1;
            }

        }
    }
}
