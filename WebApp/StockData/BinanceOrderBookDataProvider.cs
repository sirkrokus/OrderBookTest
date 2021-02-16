using RestSharp;
using WebApp.Service;

namespace WebApp.StockData
{
    public class BinanceOrderBookDataProvider : BaseOrderBookDataProvider, IOrderBookDataProvider
    {
        private static readonly double EURUSD_RATE = 1.2129;
        
        public BinanceOrderBookDataProvider() : base("https://api.binance.com")
        {
        }

        public OrderBook provide(string symbol, int depth)
        {
            symbol = symbol == Symbols.BTCUSD ? "BTCUSDT" : symbol;
            // https://api.binance.com/api/v3/depth?symbol=BTCEUR&limit=50
            IRestRequest request = new RestRequest("api/v3/depth", Method.GET, DataFormat.Json)
                .AddParameter("symbol", symbol)
                .AddParameter("limit", depth);
            return GetOrderBook(symbol, request);
        }

    }
}