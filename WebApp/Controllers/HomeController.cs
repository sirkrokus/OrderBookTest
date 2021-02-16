using System.Web.Mvc;
using log4net;
using WebApp.Models;
using WebApp.Service;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ILog log = LogManager.GetLogger(typeof(HomeController));

        public ActionResult Index(OrderBookForm form)
        {
            CookieHelper.SetUserCookie(Request, Response);

            form.UseDefaultProviderIfEmpty(OrderBookService.BINANCE);
            form.UseDefaultSymbolIfEmpty("BTCEUR");
            form.SymbolTitle = form.Symbol.Substring(0, 3) + "/" + form.Symbol.Substring(3, 3);
            
            return View(form);
        }

        public ActionResult Audit()
        {
            ViewBag.AuditTable = AuditService.Instance.GetAll();
            return View();
        }

    }
}