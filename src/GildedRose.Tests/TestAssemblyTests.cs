using GildedRose.Business;
using NUnit.Framework;

namespace GildedRose.Tests
{
    [TestFixture]
    public class UpdateQualityTests
    {
       
        [Test]
        public void UpdateQuality_HandlesNull()
        {
            new Processor().UpdateQuality(null);
        }

        [Test]
        public void UpdateQuality_DecreasSellIn()
        {
            Item item = new Item { SellIn = 1 };

            H.RunUpdateQuality(item);

            Assert.That(item.SellIn == 0);
        }

        [Test]
        public void UpdateQuality_DecreaseQuality()
        {
            Item item = new Item { Quality = 1 };

            H.RunUpdateQuality(item);

            Assert.That(item.Quality == 0);
        }

        [Test]
        public void UpdateQuality_SoldItem_QualityTwiceAsFast()
        {
            Item item = new Item { Quality = 2, SellIn = 0 };

            H.RunUpdateQuality(item);

            Assert.That(item.Quality == 0);
        }

        [Test]
        public void UpdateQuality_TheQualityofanitemisnevernegative()
        {
            Item item = new Item { Quality = 0 };

            H.RunUpdateQuality(item);

            Assert.That(item.Quality == 0);
        }

        [Test]
        public void UpdateQuality_AgedBrie_ActuallyincreasesinQualitytheolderitgets()
        {
            Item item = new Item { Name = Processor.AgedBrie, Quality = 0 };

            H.RunUpdateQuality(item);

            Assert.That(item.Quality == 2);
        }

        [Test]
        public void UpdateQuality_QualityNeverOver50_AgedBrie_ActuallyincreasesinQualitytheolderitgets()
        {
            Item item = new Item { Name = Processor.AgedBrie, Quality = 50 };

            H.RunUpdateQuality(item);

            Assert.That(item.Quality == 50);
        }

        [Test]
        public void UpdateQuality_Sulfuras_QualityNoChange()
        {
            Item item = new Item { Name = Processor.Sulfuras, Quality = 1 };

            H.RunUpdateQuality(item);

            Assert.That(item.Quality == 1);
        }

        [Test]
        public void UpdateQuality_Sulfuras_SellNoChange()
        {
            Item item = new Item { Name = Processor.Sulfuras, SellIn = 1 };

            H.RunUpdateQuality(item);

            Assert.That(item.SellIn == 1);
        }

        [Test]
        public void UpdateQuality_BackstagePasses_Qualitydropsto0aftertheconcert()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 0, 10, 0);
        }

        [Test]
        public void UpdateQuality_BackstagePasses_Qualityincreasesby3whenthereare5daysorless()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 5, 0, 3);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 4, 0, 3);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 3, 0, 3);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 2, 0, 3);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 1, 0, 3);
        }

        [Test]
        public void UpdateQuality_BackstagePasses_Qualityincreasesby2whenthereare10daysorless()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 9, 0, 2);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 8, 0, 2);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 7, 0, 2);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 6, 0, 2);
        }

        [Test]
        public void UpdateQuality_BackstagePasses_MoreThan10()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 10, 10, 12);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 11, 10, 11);
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 12, 10, 11);
        }

        [Test]
        public void UpdateQuality_BackstagePasses_Is49()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 10, 49, 50);
        }

        [Test]
        public void UpdateQuality_BackstagePasses_Is50()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 10, 50, 50);
        }

        [Test]
        public void UpdateQuality_BackstagePasses_Is51()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.BackstagePasses, 10, 51, 51);
        }

        [Test]
        public void UpdateQuality_DecreaseSell_CanBeNegative()
        {
            Item item = new Item { SellIn = 0 };

            H.RunUpdateQuality(item);

            Assert.AreEqual(-1, item.SellIn);
        }
       

        [Test]
        public void UpdateQuality_Conjured_SellIn1Day_QualityDropsTwice()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.Conjured, sellIn: 1, initialQuality: 1, expectedQuality: 0);
            H.RunUpdateQuality_AssertOnQuality(Processor.Conjured, sellIn: 1, initialQuality: 2, expectedQuality: 0);
        }

        [Test]
        public void UpdateQuality_Conjured_SellIn2Day_QualityDropsTwice()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.Conjured, sellIn: 2, initialQuality: 1, expectedQuality: 0);
            H.RunUpdateQuality_AssertOnQuality(Processor.Conjured, sellIn: 2, initialQuality: 2, expectedQuality: 0);
            H.RunUpdateQuality_AssertOnQuality(Processor.Conjured, sellIn: 2, initialQuality: 3, expectedQuality: 1);
        }

        [Test]
        public void UpdateQuality_Conjured_SellIn0Day_QualityDropsTwice()
        {
            H.RunUpdateQuality_AssertOnQuality(Processor.Conjured, sellIn: 0, initialQuality: 2, expectedQuality: 0);
        }
          

    }
}