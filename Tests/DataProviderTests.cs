using log4net;
using NUnit.Framework;
using WebApp.StockData;

namespace Tests
{
    [TestFixture]
    public class DataProviderTests
    {
        private ILog log = LogManager.GetLogger(typeof(DataProviderTests));

        [Test]
        public void BinanceOrderBookDataProviderTest()
        {
            IOrderBookDataProvider dataProvider = new BinanceOrderBookDataProvider();
            OrderBook data = dataProvider.provide("BTCEUR", 50);
            log.Info(data);
        }        
        
        [Test]
        public void BitstampOrderBookDataProviderTest()
        {
            IOrderBookDataProvider dataProvider = new BitstampOrderBookDataProvider();
            OrderBook data = dataProvider.provide("BTCEUR", 50);
            log.Info(data);
        }
    }
}