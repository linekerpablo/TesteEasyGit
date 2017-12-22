using System;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations; 

namespace SisVendedoras.Dominio.Entidades
{
    public class Usuario
   {
        #region Propriedades

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 IdUsuario { get; set; }
        public String Nome { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public String DataCadastro { get; set; }

        #endregion
   }
}


