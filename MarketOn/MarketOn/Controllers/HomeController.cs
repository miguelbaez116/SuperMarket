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
                ListaProducto = (from I in db.Inventario
                                 join U in db.Usuario on I.UserId equals U.UserId
                                 join F in db.FamiliaCode on I.FamiliaId equals F.FamiliaId
                                 join P in db.Producto on I.ProductoId equals P.ProductoId
                                 where (U.UserId == usuario.UserId) || (F.FamiliaId == usuario.FamiliaId)
                                 select P).ToList(),

                Usuario = usuario,
                ListaCategorias = db.Categoria.ToList(),
                ListaProductos = db.Producto.ToList()
    
            };
            return View(MOM);
        }

        public ActionResult CrearProducto(string nombreProducto, string categoriaId)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var idCategoria = Int32.Parse(categoriaId);
            var producto = new Producto();
            var validacion = true;

            producto.ProductoNombre = nombreProducto;
            producto.CategoriaId = idCategoria;
            producto.Estado = "N/A";
            producto.Precio = 0;
            producto.Stock = 4;

            db.Producto.Add(producto);
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });

        }

        public ActionResult AgregarProducto(string productoId)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var idProducto = Int32.Parse(productoId);

            var inventario = new Inventario();
            var validacion = true;
            var usuario = HttpContext.GetUsuario();

            inventario.ProductoId = idProducto;
            inventario.FamiliaId = usuario.FamiliaId;
            inventario.UserId = usuario.UserId;
            inventario.Cantidad = 2;

            db.Inventario.Add(inventario);
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });

        }

        [HttpPost]
        public ActionResult Index(string nose)
        {          
            return View();
        }
    }
}