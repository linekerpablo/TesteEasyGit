using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SisVendedoras.Authentication.Authentication;

namespace SisVendedoras.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Login(string email, string senha)
        {
            try
            {
                bool logado = new AuthenticationUser().Logon(email, senha, true);

                if (logado)
                {
                    RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("E-mail ou senha inválidos");
                }
            }
            catch (Exception ex)
            {
                return Json(new { erros = ex.Message });
            }

            return Json(new { });
        }
    }
}