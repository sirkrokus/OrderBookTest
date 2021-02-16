using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebApp.StockData
{
    public class OrderBook : IComparable<OrderBook>
    {
        [JsonProperty("timeSnapshot")]
        public long TimeSnapshot { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("bids")]
        public List<decimal[]> Bids { get; set; }

        [JsonProperty("asks")]
        public List<decimal[]> Asks { get; set; }

        public int CompareTo(OrderBook other)
        {
            return other == null || other.TimeSnapshot == TimeSnapshot ? 0 : (other.TimeSnapshot < TimeSnapshot ? 1 : -1);
        }

        public override string ToString()
        {
            string result = $"timeSnapshot:{TimeSnapshot}, symbol:{Symbol}. BIDS: ";
            if (Bids == null)
            {
                result += "[]";
            }
            else
            {
                foreach (var bid in Bids)
                {
                    result += $"[{bid[0]}:{bid[1]}], ";
                }
            }

            result += " ASKS: ";
            if (Asks == null)
            {
                result += "[]";
            }
            else
            {
                foreach (var ask in Asks)
                {
                    result += $"[{ask[0]}:{ask[1]}], ";
                }
            }

            return result;
        }

        public void ConvertWith(decimal rate)
        {
            foreach (var ask in Asks)
            {
                ask[0] = ask[0] * rate;
            }
            foreach (var bid in Bids)
            {
                bid[0] = bid[0] * rate;
            }
        }

        public void Reduce(int depth)
        {
            Asks.RemoveRange(depth, Asks.Count - depth);
            Bids.RemoveRange(depth, Bids.Count - depth);
        }
    }
    
}