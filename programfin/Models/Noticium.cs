using System;
using System.Collections.Generic;

namespace programfin.Models
{
    public partial class Noticium
    {
        public int IdNoticia { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public byte[] FotoPortada { get; set; } = null!;
        public string Cuerpo { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }
        public int IdUsuario { get; set; }
        public int IdCategoria { get; set; }

        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
