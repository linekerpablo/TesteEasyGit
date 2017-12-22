using SisVendedoras.Authentication.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SisVendedoras.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (AuthenticationUser.UserSession != null)
            {
                return View();
            }
            else
            {
                RedirectToAction("Index", "Login");
            }

            return View();
        }
    }
}