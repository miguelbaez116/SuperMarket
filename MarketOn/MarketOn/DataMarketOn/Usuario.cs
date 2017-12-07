//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MarketOn.DataMarketOn
{
    using System;
    using System.Collections.Generic;
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.Inventario = new HashSet<Inventario>();
            this.ListaCompra = new HashSet<ListaCompra>();
        }
    
        public int UserId { get; set; }
        public Nullable<int> LoginId { get; set; }
        public Nullable<int> FamiliaId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
    
        public virtual FamiliaCode FamiliaCode { get; set; }
        public virtual ICollection<Inventario> Inventario { get; set; }
        public virtual ICollection<ListaCompra> ListaCompra { get; set; }
        public virtual Login Login { get; set; }
    }
}
