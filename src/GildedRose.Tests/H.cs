using System.Collections.Generic;
using GildedRose.Business;
using NUnit.Framework;

namespace GildedRose.Tests
{
    public static class H
    {
        public static void RunUpdateQuality_AssertOnQuality(string name, int sellIn, int initialQuality, int expectedQuality)
        {
            Item item = new Item { Name = name, SellIn = sellIn, Quality = initialQuality };

            RunUpdateQuality(item);

            Assert.AreEqual(expectedQuality, item.Quality);
        }

        public static void RunUpdateQuality(Item item)
        {
            List<Item> list = new List<Item> { item };
            (new Processor()).UpdateQuality(list);
        }

    }
}