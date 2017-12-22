using SisVendedoras.Dominio.Entidades;
using System.Web;
using System.Web.Security;
using System;
using Newtonsoft.Json;
using SisVendedoras.Infra.Repositorios;
using SisVendedoras.Dominio.Utilidades;

namespace SisVendedoras.Authentication.Authentication
{
    public class AuthenticationUser
    {
        private readonly UsuarioRepositorio _usuarioRepository = new UsuarioRepositorio();

        #region Propriedades

        public static SessionModel UserSession
        {
            get
            {
                return new AuthenticationUser().UserSessionPrivate;
            }
        }

        private SessionModel userSessionPrivate;
        public SessionModel UserSessionPrivate
        {
            get
            {
                try
                {
                    if (userSessionPrivate == null)
                    {
                        SessionModel objUserSession = null;

                        HttpCookie cookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];

                        if (cookie == null)
                        {
                            //TODO: possible cookie null?
                        }
                        else
                        {
                            //Decrypt the ticket
                            FormsAuthenticationTicket decTicket = null;

                            if (!string.IsNullOrWhiteSpace(cookie.Value))
                            {
                                decTicket = FormsAuthentication.Decrypt(cookie.Value);
                            }

                            if (decTicket == null)
                            {
                                //TODO: possible decTicket null?
                            }
                            else
                            {
                                objUserSession = JsonConvert.DeserializeObject<SessionModel>(decTicket.UserData);
                            }
                        }

                        userSessionPrivate = objUserSession;
                    }
                }
                catch (Exception)
                {
                    HttpContext.Current.Response.Redirect("/Home/Index");
                }

                return userSessionPrivate;
            }
        }

        #endregion

        #region Methods

        public bool Logon(string email, string senha, bool lembrarMe)
        {
            Usuario objUsuario = _usuarioRepository.BuscarPorEmail(email);

            if (objUsuario != null && new Encrypt().Compara(senha, objUsuario.Senha))
            {
                try
                {
                    SessionModel objSessionModel = new Login().ConvertUsuarioParaSessionModel(objUsuario);

                    string sessionSerialized = JsonConvert.SerializeObject(objSessionModel);

                    DateTime lembrarMeDate = DateTime.Now;

                    if (lembrarMe)
                    {
                        lembrarMeDate = lembrarMeDate.AddMonths(1);
                    }
                    else
                    {
                        lembrarMeDate = lembrarMeDate.AddMinutes(30);
                    }

                    FormsAuthenticationTicket objFormsAuthenticationTicket = new
                    FormsAuthenticationTicket
                      (1,
                       email,
                       DateTime.Now,
                       lembrarMeDate,
                       false,
                       sessionSerialized,
                       FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(objFormsAuthenticationTicket);
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { HttpOnly = true });

                    return true;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return false;
        }

        public bool LogonFacebook(string email)
        {
            Usuario objUsuario = _usuarioRepository.BuscarPorEmail(email);

            if (objUsuario != null)
            {
                try
                {
                    SessionModel objSessionModel = new Login().ConvertUsuarioParaSessionModel(objUsuario);

                    string sessionSerialized = JsonConvert.SerializeObject(objSessionModel);

                    DateTime lembrarMeDate = DateTime.Now;

                    lembrarMeDate = lembrarMeDate.AddMonths(1);

                    FormsAuthenticationTicket objFormsAuthenticationTicket = new
                    FormsAuthenticationTicket
                      (1,
                       email,
                       DateTime.Now,
                       lembrarMeDate,
                       false,
                       sessionSerialized,
                       FormsAuthentication.FormsCookiePath);

                    string encTicket = FormsAuthentication.Encrypt(objFormsAuthenticationTicket);
                    HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket) { HttpOnly = true });

                    return true;
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return false;
        }

        public void Logout()
        {
            if (HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.UtcNow.AddDays(-1);
                FormsAuthentication.SignOut();
            }
        }

        #endregion
    }
}
