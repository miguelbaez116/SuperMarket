using MarketOn.DataMarketOn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarketOn.Helpers
{
    public static class Helpers
    {
        public static Usuario GetUsuario(this HttpContextBase ctx)
        {

            if (ctx.Session["Logeado"] != null)
            {
                var userId = Int32.Parse(ctx.Session["Logeado"].ToString());
                var db = new DataMarketOn.MarketOnEntities();
                var usuario = db.Usuario.Where(z => z.UserId == userId).FirstOrDefault();
                return usuario;
            }

            return null;    
        }
    }
}