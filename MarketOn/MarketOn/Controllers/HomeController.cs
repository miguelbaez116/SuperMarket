using MarketOn.DataMarketOn;
using MarketOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketOn.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index() //Usuario usuario
        {
            var usuario = new Usuario();
            usuario.UserId = 3;
            usuario.FamiliaId = 1;

            var db = new DataMarketOn.MarketOnEntities();
            var MOM = new MothesOfModels
            {
                ListaProducto = (from I in db.Inventario
                                 join U in db.Usuario on I.UserId equals U.UserId
                                 join F in db.FamiliaCode on I.FamiliaId equals F.FamiliaId
                                 join P in db.Producto on I.ProductoId equals P.ProductoId
                                 where (U.UserId == usuario.UserId) || (F.FamiliaId == usuario.FamiliaId)
                                 select P).ToList(),

                Usuario = usuario,
    
            };
            return View(MOM);
        }

        [HttpPost]
        public ActionResult Index(string nose)
        {          
            return View();
        }
    }
}