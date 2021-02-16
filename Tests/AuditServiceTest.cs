using System;
using System.Collections.Generic;
using NUnit.Framework;
using WebApp.Service;
using WebApp.StockData;

namespace Tests
{
    [TestFixture]
    public class AuditServiceTest
    {
        
        [Test]
        public void AddToLogTest()
        {
            for (int i = 1; i <= 5; i++)
            {
                AuditService.Instance.AddToLog("u1", CreateOrderBook(i));    
                AuditService.Instance.AddToLog("u2", CreateOrderBook(10+i));    
            }
            
            SortedSet<OrderBook> set = AuditService.Instance.GetAllForUser("u1");
            Console.WriteLine($@"*** User: u1 ***");
            foreach (var book in set)
            {
                Console.WriteLine(book);
            }
            
            Dictionary<string, SortedSet<OrderBook>> all = AuditService.Instance.GetAll();
            foreach (KeyValuePair<string,SortedSet<OrderBook>> kv in all)
            {
                Console.WriteLine($@"### User: {kv.Key} ###");
                set = all[kv.Key];
                foreach (var book in set)
                {
                    Console.WriteLine(book);
                }
            }
        }

        private OrderBook CreateOrderBook(long id)
        {
            List<decimal[]> asks = new List<decimal[]>();
            asks.Add(new decimal[] { id,id });
            asks.Add(new decimal[] { 2,2 });
            
            List<decimal[]> bids = new List<decimal[]>();
            bids.Add(new decimal[] { 3,3 });
            bids.Add(new decimal[] { 4,4 });
            return new OrderBook()
            {
                TimeSnapshot = id,
                Symbol = "BTCEUR",
                Asks = asks,
                Bids = bids
            };
        }
        
        
    }
}