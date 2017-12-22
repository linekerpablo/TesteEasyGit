using System;
using System.Web.Mvc;
using SisVendedoras.Authentication.Authentication;
using SisVendedoras.Infra.Repositorios;
using SisVendedoras.Servicos.MarcaServico;
using SisVendedoras.Dominio.Entidades;
using System.Linq;

namespace SisVendedoras.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [Route("EditarRegistro/{idMarca}")]
        public ActionResult EditarRegistro(long? idMarca)
        {
            return View();
        }

        public JsonResult CarregarMarca(long idMarca)
        {
            try
            {
                if (AuthenticationUser.UserSession != null)
                {
                    return Json(new { marca = new MarcaServico().GetById(idMarca) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { urlRedirecionar = "/Login/Index" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult CarregarMarcas()
        {
            try
            {
                if (AuthenticationUser.UserSession != null)
                {
                    return Json(new { lstMarcas = new MarcaServico().GetAll().Where(tmp => tmp.IdUsuario.Equals(AuthenticationUser.UserSession.IdUsuario)) }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { urlRedirecionar = "/Login/Index" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json( new { erros = ex.Message });
            }
        }

        public JsonResult Salvar(string descricao)
        {
            try
            {
                if (AuthenticationUser.UserSession != null)
                {
                    new MarcaServico().Salvar(new Marca { IdUsuario = AuthenticationUser.UserSession.IdUsuario, Descricao = descricao });

                    return Json(new { mensagem = "Cadastro realizado com sucesso", urlRedirecionar = "/Marca/Index" });
                }
                else
                {
                    return Json(new { urlRedirecionar = "/Login/Index" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { erros = ex.Message });
            }
        }

        public JsonResult Editar(Marca objMarca)
        {
            try
            {
                if (AuthenticationUser.UserSession != null)
                {
                    new MarcaServico().Editar(objMarca);

                    return Json(new { mensagem = "Editado com sucesso", urlRedirecionar = "/Marca/Index" });
                }
                else
                {
                    return Json(new { urlRedirecionar = "/Login/Index" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { erros = ex.Message });
            }
        }

        public JsonResult Deletar(Marca objMarca)
        {
            try
            {
                if (AuthenticationUser.UserSession != null)
                {
                    new MarcaServico().Deletar(objMarca);

                    return Json(new { mensagem = "Deletado com sucesso", urlRedirecionar = "/Marca/Index" });
                }
                else
                {
                    return Json(new { urlRedirecionar = "/Login/Index" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { erros = ex.Message });
            }
        }
    }
}