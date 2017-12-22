using SisVendedoras.Infra.Repositorios;
using System;
using SisVendedoras.Dominio.Entidades;
using SisVendedoras.Authentication.Authentication;
using System.Collections.Generic;

namespace SisVendedoras.Servicos.MarcaServico
{
    public class MarcaServico
    {
        private readonly MarcaRepositorio _marcaRepositorio = new MarcaRepositorio();

        public Marca GetById(long idMarca)
        {
            return _marcaRepositorio.GetById(idMarca);
        }

        public IEnumerable<Marca> GetAll()
        {
            return _marcaRepositorio.GetAll();
        }

        public void Salvar(Marca objMarca)
        {
            Validacoes(objMarca);

            _marcaRepositorio.Add(new Marca { Descricao = objMarca.Descricao, IdUsuario = objMarca.IdUsuario });
        }

        public void Editar(Marca objMarca)
        {
            Validacoes(objMarca, true);

            _marcaRepositorio.Update(objMarca);
        }

        public void Deletar(Marca objMarca)
        {
            objMarca = _marcaRepositorio.GetById(objMarca.IdMarca);
            _marcaRepositorio.Remove(objMarca);
        }

        private void Validacoes(Marca objMarca, bool editar = false)
        {
            if (string.IsNullOrEmpty(objMarca.Descricao))
            {
                throw new Exception("Descriçao deve ser informada");
            }

            if (!editar)
            {
                Marca objMarcaExistente = _marcaRepositorio.BuscarPorDescricaoEUsuario(objMarca.Descricao, objMarca.IdUsuario);

                if (objMarcaExistente != null)
                {
                    throw new Exception("Descriçao já cadastrada");
                }
            }
        }
    }
}