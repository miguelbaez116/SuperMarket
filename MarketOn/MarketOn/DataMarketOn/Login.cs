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
    
    public partial class Login
    {
        public Login()
        {
            this.Login_Detail = new HashSet<Login_Detail>();
            this.Usuario = new HashSet<Usuario>();
        }
    
        public int LoginId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    
        public virtual ICollection<Login_Detail> Login_Detail { get; set; }
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
