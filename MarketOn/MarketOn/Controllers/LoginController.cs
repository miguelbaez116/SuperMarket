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
            var loginDetailsInfo = new Login_Detail();

            var validation = false;

            var result = db.Login.Where(x => (x.Username == username && x.Password == password) || (x.Email == username && x.Password == password)).FirstOrDefault();

            if (result != null)
            {
                validation = true;

                //loginDetailsInfo.LoginId = result.LoginId;
                //loginDetailsInfo.Fecha = System.DateTime.Today;
                //db.Login_Detail.Add(loginDetailsInfo);

                //db.SaveChanges();

                var usuario = db.Usuario.Where(z => z.Username == result.Username).FirstOrDefault();
                Session.Add("Logeado", usuario.UserId);          
           }

            return Json(new { validation, url = Url.Action("Index", "Home") });
        }

        public ActionResult UserValidator(string username)
        {
            var db = new DataMarketOn.MarketOnEntities();

            var validation = false;

            var result = db.Login.Where(x => (x.Username == username)).FirstOrDefault();

            if (result == null)
            {
                validation = true;          
            }

            return Json(validation);
        }

        [HttpPost]
        public PartialViewResult Registro(Usuario usuario)
        {
            var db = new DataMarketOn.MarketOnEntities();
            var loginInfo = new Login();

            db.Usuario.Add(usuario);

            loginInfo.Username = usuario.Username;
            loginInfo.Password = usuario.Password;
            loginInfo.Email = usuario.Email;
            db.Login.Add(loginInfo);

            db.SaveChanges();          

            return PartialView("Login");
        }
        [HttpGet]
        public PartialViewResult Registro()
        {
            return PartialView();
        }
    }
}