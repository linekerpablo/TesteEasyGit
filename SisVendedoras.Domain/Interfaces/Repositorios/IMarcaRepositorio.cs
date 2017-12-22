using SisVendedoras.Dominio.Entidades;
using System;
using SisVendedoras.Dominio.Interfaces.Repositorios;
using System.Collections.Generic;

namespace SisVendedoras.Dominio.Interfaces
{
    public interface IMarcaRepositorio : IRepositorioBase<Marca>
    {
        #region Métodos

        Marca BuscarPorIdMarca(Int64 idmarca);
        IEnumerable<Marca> BuscarPorIdUsuario(Int64 idusuario);
        Marca BuscarPorDescricao(String descricao);
        Marca BuscarPorDescricaoEUsuario(String descricao, long idUsuario);

        bool ValidarCamposObrigatorios();

        #endregion
    }
}


