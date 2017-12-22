using SisVendedoras.Dominio.Entidades;
using System; 
using SisVendedoras.Dominio.Interfaces.Repositorios;

namespace SisVendedoras.Dominio.Interfaces
{
    public interface IUsuarioRepositorio : IRepositorioBase<Usuario>
   {
        #region Métodos

        Usuario BuscarPorIdUsuario(Int64 idusuario);
        Usuario BuscarPorNome(String nome);
        Usuario BuscarPorEmail(String email);
        Usuario BuscarPorSenha(String senha);
        Usuario BuscarPorDataCadastro(String datacadastro);

        bool ValidarCamposObrigatorios();

        #endregion
   }
}


