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

        //Listas
        public IEnumerable<Producto> ListaProducto { get; set; }

    }
}