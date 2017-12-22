using System;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace SisVendedoras.Dominio.Entidades
{
    public class Marca
   {
        #region Propriedades

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 IdMarca { get; set; }
        public Int64 IdUsuario { get; set; }
        public String Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        [ForeignKey("IdUsuario")]
        public virtual Usuario Usuario { get; set; }

        #endregion
   }
}


