using RestSharp;

namespace WebApp.StockData
{
    public class BitstampOrderBookDataProvider : BaseOrderBookDataProvider, IOrderBookDataProvider
    {
 
        public BitstampOrderBookDataProvider() : base("https://www.bitstamp.net")
        {
        }

        public OrderBook provide(string symbol, int depth)
        {
            // https://www.bitstamp.net/api/v2/order_book/btceur
            IRestRequest request = new RestRequest($"api/v2/order_book/{symbol.ToLower()}", Method.GET, DataFormat.Json);
            OrderBook book = GetOrderBook(symbol, request);
            book.Reduce(depth);
            return book;
        }
        
    }
    
}