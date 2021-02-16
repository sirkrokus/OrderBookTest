using System;
using System.Collections.Generic;
using WebApp.StockData;

namespace WebApp.Service
{
    public class AuditService
    {
        private Dictionary<string, SortedSet<OrderBook>> log = new Dictionary<string, SortedSet<OrderBook>>();

        private static readonly AuditService instance = new AuditService();
        public static AuditService Instance => instance;

        private AuditService()
        {
        }

        public void AddToLog(string userId, OrderBook book)
        {
            SortedSet<OrderBook> set;
            if (!log.ContainsKey(userId))
            {
                set = new SortedSet<OrderBook>();
                log.Add(userId, set);
            }
            else
            {
                set = log[userId];
            }

            set.Add(book);
        }

        public SortedSet<OrderBook> GetAllForUser(string userId)
        {
            if (!log.ContainsKey(userId))
            {
                throw new Exception($"User {userId} is not exist");
            }
            return log[userId];
        }
        
        public Dictionary<string, SortedSet<OrderBook>> GetAll()
        {
            return log;
        }
        
    }
}