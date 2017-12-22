using SisVendedoras.Dominio.Entidades;
using System.Data.Entity.ModelConfiguration;
using System;
using System.Data.Entity.Infrastructure.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SisVendedoras.Infra.EntityConfig
{
    public class MarcaConfiguracao : EntityTypeConfiguration<Marca>
    {
        public MarcaConfiguracao()
        {
            HasKey(p => p.IdMarca);

            Property(p => p.IdUsuario);

            Property(p => p.Descricao);

            HasRequired(p => p.Usuario)
                .WithMany()
                .HasForeignKey(p => p.IdUsuario);
        }
    }
}


