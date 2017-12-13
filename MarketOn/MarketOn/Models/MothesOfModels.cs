using MarketOn.DataMarketOn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketOn.Models
{
    public class MothesOfModels
    {
        public Usuario Usuario { get; set; }
        public Producto Producto { get; set; }
        public Categoria Categoria { get; set; }
        public FamiliaCode FamiliaCode { get; set; }
        public ListaCompra ListaCompra { get; set; }

        //Listas
        public IEnumerable<ListaProducto> ListaProductosInventarios { get; set; }
        public IEnumerable<Producto> ListaProducto { get; set; }
        public IEnumerable<Producto> ListaProductos { get; set; }
        public IEnumerable<ListaCompra> ListaCompraPersonal { get; set; }
        public IEnumerable<ListaCompra> ListaCompraFamiliar { get; set; }
        public IEnumerable<Categoria> ListaCategorias { get; set; }

    }

    public class ListaProducto
    {
        public string ProductoNombre { get; set; }
        public decimal Cantidad { get; set; }
        public int InventarioId { get; set; }

    }
}