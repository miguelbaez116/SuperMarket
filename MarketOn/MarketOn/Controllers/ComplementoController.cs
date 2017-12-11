using MarketOn.Helpers;
using MarketOn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketOn.Controllers
{
    public class ComplementoController : Controller
    {

     

        [MiguelAuthorize]
        [HttpGet]
        public ActionResult ConsultarLista()
        {
            var usuario = HttpContext.GetUsuario();

            var db = new DataMarketOn.MarketOnEntities();
            var MOM = new MothesOfModels
            {
               ListaCompraFamiliar = db.ListaCompra.Where(c => c.FamiliaId == usuario.FamiliaId).ToList() ,
               ListaCompraPersonal = db.ListaCompra.Where(z => z.UserId == usuario.UserId).ToList(),
               Usuario = usuario,
            };
            return View(MOM);
        }

        [MiguelAuthorize]
        [HttpGet]
        public ActionResult DetalleLista(string ListaCompraId)
        {
            var c = ListaCompraId;
            return View();
        }
	}
}