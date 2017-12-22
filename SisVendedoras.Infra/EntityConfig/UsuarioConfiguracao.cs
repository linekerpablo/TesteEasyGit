using SisVendedoras.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisVendedoras.Infra.EntityConfig
{
    public class UsuarioConfiguracao : EntityTypeConfiguration<Usuario>
   {
      public UsuarioConfiguracao()
      {
          HasKey(p => p.IdUsuario);

          Property(p => p.Nome);

          Property(p => p.Email);

          Property(p => p.Senha);

          Property(p => p.DataCadastro);

      }
   }
}


