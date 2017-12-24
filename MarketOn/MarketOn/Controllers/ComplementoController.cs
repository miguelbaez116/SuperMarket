using MarketOn.DataMarketOn;
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
                ListaCompraFamiliar = db.ListaCompra.Where(c => c.FamiliaId == usuario.FamiliaId && c.FamiliaId != null).ToList(),
                ListaCompraPersonal = db.ListaCompra.Where(z => z.UserId == usuario.UserId).ToList(),
                Usuario = usuario,
            };
            return View(MOM);
        }

        [MiguelAuthorize]
        [HttpGet]
        public ActionResult DetalleLista(int id)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var usuario = HttpContext.GetUsuario();

            var MOM = new MothesOfModels
            {
                ListaProductoCompra = (from P in db.Producto
                                       join DC in db.DetalleListaCompra on P.ProductoId equals DC.ProductoId
                                       join LC in db.ListaCompra on DC.ListaCompraId equals LC.ListaCompraId
                                       where LC.ListaCompraId == id
                                       select new ListaProductoCompra()
                                       {
                                           ProductoNombre = P.ProductoNombre,
                                           Cantidad = DC.Cantidad,
                                           Estado = DC.Estado,
                                           DetalleListaCompraId = DC.DetalleListaCompraId
                                       }).ToList(),
                ListaCompra = db.ListaCompra.Where(l => l.ListaCompraId == id).FirstOrDefault(),
                Usuario = usuario
            };
            return View(MOM);
        }
        public ActionResult CrearLista(string nombreLista, string tipoLista)
        {
            var db = new DataMarketOn.MarketOnEntities();

            var validation = true;
            var usuario = HttpContext.GetUsuario();

            var listaCompra = new ListaCompra
            {
                ListaCompraNombre = nombreLista,
                Fecha = System.DateTime.Today
            };

            if (tipoLista == "Personal")
            {
                listaCompra.UserId = usuario.UserId;
            }
            if (tipoLista == "Familiar")
            {
                listaCompra.FamiliaId = usuario.FamiliaId;
            }

            db.ListaCompra.Add(listaCompra);
            db.SaveChanges();

            return Json(validation);
        }



        [MiguelAuthorize]
        [HttpGet]
        public ActionResult ListaInfo(int id)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var usuario = HttpContext.GetUsuario();

            var MOM = new MothesOfModels
            {
                ListaProductos = db.Producto.ToList(),
                ListaCompra = db.ListaCompra.Where(l => l.ListaCompraId == id).FirstOrDefault(),
                Usuario = usuario
            };
            return View(MOM);
        }

        public ActionResult AgregarProductos(IEnumerable<DetalleListaCompra> listaProductos)
        {
            var db = new DataMarketOn.MarketOnEntities();

            var validacion = false;

            if (listaProductos != null)
            {
                db.DetalleListaCompra.AddRange(listaProductos);
                db.SaveChanges();
                validacion = true;
            }

            return Json(new { validacion });
        }

        public void CambioEstado(string idProducto, string valueChk)
        {
            var db = new DataMarketOn.MarketOnEntities();

            var ProductoId = Int32.Parse(idProducto);        

            var productoDetalleCompra = db.DetalleListaCompra.Find(ProductoId);
            productoDetalleCompra.Estado = valueChk;

            db.Entry(productoDetalleCompra).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

        }
    }
}