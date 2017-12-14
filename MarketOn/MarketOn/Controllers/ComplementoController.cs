﻿using MarketOn.DataMarketOn;
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
                ListaProducto = (from P in db.Producto
                                 join DC in db.DetalleListaCompra on P.ProductoId equals DC.ProductoId
                                 join LC in db.ListaCompra on DC.ListaCompraId equals LC.ListaCompraId
                                 where LC.ListaCompraId == id
                                 select P).ToList(),
                ListaCompra = db.ListaCompra.Where(l => l.ListaCompraId == id).FirstOrDefault(),
                Usuario = usuario
            };
            return View(MOM);
        }
        public ActionResult CrearLista(string nombreLista, string tipoLista )
        {
            var db = new DataMarketOn.MarketOnEntities();

            var validation = true;
            var usuario = HttpContext.GetUsuario();

            var listaCompra = new ListaCompra();

            listaCompra.SupermercadoNombre = nombreLista;
            listaCompra.Fecha = System.DateTime.Today;
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
    }
}