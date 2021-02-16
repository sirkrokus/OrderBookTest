using Newtonsoft.Json;
using RestSharp;

namespace WebApp.StockData
{
    public class BaseOrderBookDataProvider : BaseJsonDataProvider
    {
        public BaseOrderBookDataProvider(string apiUrl) : base(apiUrl)
        {
        }
        
        protected OrderBook GetOrderBook(string symbol, IRestRequest request)
        {
            IRestResponse response = Invoke(request);
            OrderBook orderBook = JsonConvert.DeserializeObject<OrderBook>(response.Content);
            orderBook.Symbol = symbol;
            return orderBook;
        }
        
    }
}