using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.StockData;

namespace WebApp.Service
{
    public class OrderBookService
    {
        public static readonly string BINANCE = "BINANCE";
        public static readonly string BITSTAMP = "BITSTAMP";

        private static readonly int DEFAULT_DEPTH = 50;
        private static readonly long MAX_PERIOD = 15; // in sec
        
        private Dictionary<string, IOrderBookDataProvider> dataProviders;
        private Cache cache = new Cache(480); // 2h * 60min * 4; // 60 min per hour * every 15 sec (4 per minute)

        public OrderBookService()
        {
            dataProviders = new Dictionary<string, IOrderBookDataProvider>();
            dataProviders.Add(BINANCE, new BinanceOrderBookDataProvider());
            dataProviders.Add(BITSTAMP, new BitstampOrderBookDataProvider());
        }
        
        public OrderBook GetOrderBook(string providerName, string symbol)
        {
            if (!dataProviders.ContainsKey(providerName))
            {
                throw new ArgumentException($"{providerName} data provider is not supported");
            }

            var now = DateTime.Now.Ticks;
            OrderBook book = cache.FindLast();
            
            // if a last record is too old then obtain newest data from a stock exchange
            if (book == null || TimeSpan.FromTicks(now - book.TimeSnapshot).Seconds > MAX_PERIOD)
            {
                book = dataProviders[providerName].provide(symbol, DEFAULT_DEPTH);
                book.TimeSnapshot = now;
                cache.Add(book);
            }

            return book;
        }
        
    }

    public class Cache
    {
        private int maxSize;
        private SortedList<long, OrderBook> books = new SortedList<long, OrderBook>();

        public Cache(int maxSize)
        {
            this.maxSize = maxSize;
        }

        public void Add(OrderBook book)
        {
            books.Add(book.TimeSnapshot, book);
            if (Size() > maxSize)
            {
                books.RemoveAt(0);
            }
        }
        
        public OrderBook FindLast()
        {
            return Size() > 0 ? books.Last().Value : null;
        }

        public int Size()
        {
            return books.Count;
        }
    }
    
}