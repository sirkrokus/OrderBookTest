using System;
using System.Web.Http;
using log4net;
using WebApp.Service;
using WebApp.StockData;

namespace WebApp.Controllers
{
    public class OrderBookController : ApiController
    {
        private ILog log = LogManager.GetLogger(typeof(OrderBookController));

        private OrderBookService orderBookService = new OrderBookService();
        
        [Route("api/orderbook")]
        [HttpGet]
        public OrderBook GetOrderBook(string providerCode, string symbol, string user)
        {
            try
            {
                var book = orderBookService.GetOrderBook(providerCode, symbol);
                AuditService.Instance.AddToLog(user, book);
                return book;
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
                throw;
            }
        }

    }
}