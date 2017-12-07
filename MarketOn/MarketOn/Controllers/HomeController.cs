using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketOn.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new DataMarketOn.MarketOnEntities();

            var lista = context.Producto.ToList();

            return View();
        }
    }
}