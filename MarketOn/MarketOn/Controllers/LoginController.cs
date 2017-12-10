using MarketOn.DataMarketOn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketOn.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login(Usuario usuario)
        {
            return View();
        }

        public ActionResult LoginValidator( string username, string password) {
            var db = new DataMarketOn.MarketOnEntities();
            var validation = false;

            var result = db.Login.Where(x => (x.Username == username && x.Password == password) || (x.Email == username && x.Password == password)).FirstOrDefault();

            if (result != null){ validation = true;}

            return Json(validation);
        }

        [HttpPost]
        public PartialViewResult Registro(Usuario usuario)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var loginInfo = new Login();
            var loginDetailsInfo = new Login_Detail();

            db.Usuario.Add(usuario);

            loginInfo.Username = usuario.Username;
            loginInfo.Password = usuario.Password;
            loginInfo.Email = usuario.Email;
            db.Login.Add(loginInfo);

            db.SaveChanges();

            var idLogin = (from x in db.Login orderby x.LoginId descending select x).First();
            loginDetailsInfo.LoginId = idLogin.LoginId;
            loginDetailsInfo.Fecha = System.DateTime.Today;
            db.Login_Detail.Add(loginDetailsInfo);

            db.SaveChanges();

            return PartialView("~/Views/Login/Login.cshtml");
        }
        [HttpGet]
        public PartialViewResult Registro()
        {
            return PartialView();
        }
    }
}