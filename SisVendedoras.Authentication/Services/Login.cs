using SisVendedoras.Dominio.Entidades;

namespace SisVendedoras.Authentication
{
    public class Login
    {
        public SessionModel ConvertUsuarioParaSessionModel(Usuario objUsuario)
        {
            SessionModel objSessionModel = new SessionModel
            {
                IdUsuario = objUsuario.IdUsuario,
                Email = objUsuario.Email,
                Nome = objUsuario.Nome
            };

            return objSessionModel;
        }
    }
}
