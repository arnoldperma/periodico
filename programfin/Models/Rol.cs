using System;
using System.Collections.Generic;

namespace programfin.Models
{
    public partial class Rol
    {
        public Rol()
        {
            IdUsuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Usuario> IdUsuarios { get; set; }
    }
}
