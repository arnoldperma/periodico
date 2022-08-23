using System;
using System.Collections.Generic;

namespace programfin.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Noticia = new HashSet<Noticium>();
            IdRols = new HashSet<Rol>();
        }

        public int IdUsuario { get; set; }
        public string Usuario1 { get; set; } = null!;
        public string Contrasenia { get; set; } = null!;
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<Noticium> Noticia { get; set; }

        public virtual ICollection<Rol> IdRols { get; set; }
    }
}
