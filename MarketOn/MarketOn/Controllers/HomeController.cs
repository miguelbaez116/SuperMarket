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
    public class HomeController : Controller
    {
        [MiguelAuthorize]
        [HttpGet]
        public ActionResult Index()
        {
            var usuario = HttpContext.GetUsuario();

            var db = new DataMarketOn.MarketOnEntities();

            var MOM = new MothesOfModels
            {
                ListaProductosInventarios = (from I in db.Inventario
                                 join P in db.Producto on I.ProductoId equals P.ProductoId
                                 where (I.UserId == usuario.UserId) || (I.FamiliaId == usuario.FamiliaId)
                                 select new ListaProducto() { ProductoNombre = P.ProductoNombre,
                                                              Cantidad = I.Cantidad,
                                                              InventarioId = I.InventarioId}).ToList(),

                Usuario = usuario,
                ListaCategorias = db.Categoria.ToList(),
                ListaProductos = db.Producto.ToList()
    
            };
            return View(MOM);
        }
        public ActionResult CrearProducto(string nombreProducto, string categoriaId)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var producto = new Producto();

            var idCategoria = Int32.Parse(categoriaId);
            var validacion = true;

            producto.ProductoNombre = nombreProducto;
            producto.CategoriaId = idCategoria;
            producto.Estado = "N/A";
            producto.Precio = 0;
            producto.Stock = 0;

            db.Producto.Add(producto);
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });

        }
        public ActionResult AgregarProducto(string productoId, string Cantidad)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var inventario = new Inventario();

            var idProducto = Int32.Parse(productoId);
            var cantidad = Decimal.Parse(Cantidad);
            var validacion = true;
            var usuario = HttpContext.GetUsuario();

            inventario.ProductoId = idProducto;
            inventario.FamiliaId = usuario.FamiliaId;
            inventario.UserId = usuario.UserId;
            inventario.Cantidad = cantidad;

            db.Inventario.Add(inventario);
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });
        }
        public ActionResult EliminarProducto(string inventarioId)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var inventario = new Inventario();

            var idInventario = Int32.Parse(inventarioId);
            var validacion = true;

            var productoInventario = db.Inventario.Find(idInventario);

            db.Inventario.Remove(productoInventario);
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });
        }
        public ActionResult ModificarProducto(string InventarioId, string Cantidad)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var inventario = new Inventario();

            var idInventario = Int32.Parse(InventarioId);
            var cantidad = Decimal.Parse(Cantidad);
            var validacion = true;

            var productoInventario = db.Inventario.Find(idInventario);
            productoInventario.Cantidad = cantidad;

            db.Entry(productoInventario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });
        }

    }
}