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
                                             where (I.UserId == usuario.UserId) || ((I.FamiliaId == usuario.FamiliaId) && (usuario.FamiliaId != null))
                                             select new ListaProducto()
                                             {
                                                 ProductoNombre = P.ProductoNombre,
                                                 Cantidad = I.Cantidad,
                                                 InventarioId = I.InventarioId
                                             }).ToList(),
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
            var validacion = true;

            var producto = new Producto
            {
                ProductoNombre = nombreProducto,
                CategoriaId = idCategoria
            };


            db.Producto.Add(producto);
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });

        }
        public ActionResult AgregarProducto(string productoId, string Cantidad)
        {
            var db = new DataMarketOn.MarketOnEntities();

            var idProducto = Int32.Parse(productoId);
            var cantidad = Decimal.Parse(Cantidad);
            var validacion = true;
            var usuario = HttpContext.GetUsuario();

            var inventario = new Inventario
            {
                ProductoId = idProducto,
                FamiliaId = usuario.FamiliaId,
                UserId = usuario.UserId,
                Cantidad = cantidad
            };

            db.Inventario.Add(inventario);
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });
        }
        public ActionResult EliminarProducto(string inventarioId)
        {
            var db = new DataMarketOn.MarketOnEntities();

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

            var idInventario = Int32.Parse(InventarioId);
            var cantidad = Decimal.Parse(Cantidad);
            var validacion = true;

            var productoInventario = db.Inventario.Find(idInventario);
            productoInventario.Cantidad = cantidad;

            db.Entry(productoInventario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(new { validacion, url = Url.Action("Index", "Home") });
        }

        [MiguelAuthorize]
        [HttpGet]
        public ActionResult InfoUsuario()
        {
            var usuario = HttpContext.GetUsuario();

            var db = new DataMarketOn.MarketOnEntities();

            var MOM = new MothesOfModels
            {
                Usuario = usuario
            };
            return View(MOM);
        }
        public ActionResult ModificarUsuario(string nombre, string apellido, string familiaId)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var usuario = HttpContext.GetUsuario();
            var validacion = false;

            var idFamiliar = 0;
            if (familiaId != "") { idFamiliar = Int32.Parse(familiaId); }

            if (idFamiliar != 0)
            { 
                var familiaCode = db.FamiliaCode.Find(idFamiliar);

                if (familiaCode == null) { validacion = false; }
                else
                {
                    var usuarioModifcar = db.Usuario.Find(usuario.UserId);
                    usuarioModifcar.Nombre = nombre;
                    usuarioModifcar.Apellido = apellido;
                    usuarioModifcar.FamiliaId = idFamiliar;

                    db.Entry(usuarioModifcar).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
            
                    validacion = true;
                }
            }
            if (familiaId == "") { validacion = true; }
            return Json(new { validacion, url = Url.Action("Index", "Home") });
        }
        public ActionResult CrearFamilia(string nombreFamilia)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var validacion = true;

            var FamiliaCode = new FamiliaCode
            {
                FamiliaNombre = nombreFamilia,
                FamiliaCodigo = nombreFamilia
            };

            db.FamiliaCode.Add(FamiliaCode);
            db.SaveChanges();

            var codigoFamiliar = (from c in db.FamiliaCode 
                                  orderby c.FamiliaId descending
                                  select c).FirstOrDefault();

            return Json(new { validacion, url = Url.Action("InfoUsuario", "Home"), codigo = codigoFamiliar.FamiliaId });
        }
        public ActionResult ValidarPassword(string PassActual)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var validacion = false;

            var usuario = HttpContext.GetUsuario();

            if (usuario.Password == PassActual)
            {
             validacion = true;
            }

            return Json(new { validacion });
        }
        public ActionResult ModififcarPassword(string PassNuevo)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var usuario = HttpContext.GetUsuario();
            var validacion = false;

            if (PassNuevo != "")
            {
                var usuarioModifcar = db.Usuario.Find(usuario.UserId);
                usuarioModifcar.Password = PassNuevo;

                db.Entry(usuarioModifcar).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                validacion = true;
            }

            return Json(new { validacion, url = Url.Action("InfoUsuario", "Home") });
        }

    }
}