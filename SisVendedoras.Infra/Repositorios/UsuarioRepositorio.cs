using SisVendedoras.Dominio.Entidades;
using System.Collections.Generic;
using SisVendedoras.Dominio.Interfaces;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System;
using SisVendedoras.Infra.Data.Repositories;

namespace SisVendedoras.Infra.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
   {
        #region Métodos

        public Usuario BuscarPorIdUsuario(Int64 idusuario)
      {
            return Db.Usuarios.Where(p => p.IdUsuario.Equals(idusuario)).FirstOrDefault();
      }

        public Usuario BuscarPorNome(String nome)
      {
            return Db.Usuarios.Where(p => p.Nome.Equals(nome)).FirstOrDefault();
      }

        public Usuario BuscarPorEmail(String email)
      {
            return Db.Usuarios.Where(p => p.Email.Equals(email)).FirstOrDefault();
      }

        public Usuario BuscarPorSenha(String senha)
      {
            return Db.Usuarios.Where(p => p.Senha.Equals(senha)).FirstOrDefault();
      }

        public Usuario BuscarPorDataCadastro(String datacadastro)
      {
            return Db.Usuarios.Where(p => p.DataCadastro.Equals(datacadastro)).FirstOrDefault();
      }

        public bool ValidarCamposObrigatorios()
      {
            return false;
      }

        #endregion
   }
}


