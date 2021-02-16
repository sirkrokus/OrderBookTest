using System;
using NUnit.Framework;
using WebApp.Service;
using WebApp.StockData;

namespace Tests
{
    [TestFixture]
    public class OrderBookTest
    {
        private OrderBookService orderBookService = new OrderBookService();

        [Test]
        [TestCase("BINANCE", "BTCEUR", true)]
        [TestCase("BINANCE", "BTCUSD", true)]
        [TestCase("BITSTAMP", "BTCEUR", true)]
        [TestCase("BITSTAMP", "BTCUSD", true)]
        [TestCase("POLONIEX", "BTCETH", false)]
        public void OrderBookServiceTest(string providerName, string symbol, bool valid)
        {
            try
            {
                orderBookService.GetOrderBook(providerName, symbol);
                Assert.IsTrue(valid, "Valid result is expected");
            }
            catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
                Assert.IsFalse(valid, "Invalid result is expected");
            }
        }

        [Test]
        public void OrderBookCache()
        {
            Cache cache = new Cache(10);
            for (int i = 0; i < 20; i++)
            {
                OrderBook book = new OrderBook()
                {
                    TimeSnapshot = i
                };
                cache.Add(book);
                Console.WriteLine($"{i}={cache.Size()}. Last entry:{cache.FindLast()}");
            }
        }
        
    }
}