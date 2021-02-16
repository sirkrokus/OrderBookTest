namespace WebApp.StockData
{
    public interface IOrderBookDataProvider
    {
        OrderBook provide(string symbol, int depth);
    }
}