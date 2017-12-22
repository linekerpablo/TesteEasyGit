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
    public class MarcaRepositorio : RepositorioBase<Marca>, IMarcaRepositorio
    {
        #region Métodos

        public Marca BuscarPorIdMarca(Int64 idmarca)
        {
            return Db.Marcas.Where(p => p.IdMarca.Equals(idmarca)).FirstOrDefault();
        }

        public IEnumerable<Marca> BuscarPorIdUsuario(Int64 idusuario)
        {
            return Db.Marcas.Where(p => p.IdUsuario.Equals(idusuario));
        }

        public Marca BuscarPorDescricao(String descricao)
        {
            return Db.Marcas.Where(p => p.Descricao.Equals(descricao)).FirstOrDefault();
        }

        public Marca BuscarPorDescricaoEUsuario(String descricao, long idUsuario)
        {
            return Db.Marcas.Where(p => p.Descricao.Equals(descricao) && p.IdUsuario.Equals(idUsuario)).FirstOrDefault();
        }

        public bool ValidarCamposObrigatorios()
        {
            return false;
        }

        #endregion
    }
}


