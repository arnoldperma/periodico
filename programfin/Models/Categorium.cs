using System;
using System.Collections.Generic;

namespace programfin.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Noticia = new HashSet<Noticium>();
        }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Noticium> Noticia { get; set; }
    }
}
