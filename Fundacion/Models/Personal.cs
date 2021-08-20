using System;
using System.Collections.Generic;

#nullable disable

namespace Fundacion.Models
{
    public partial class Personal
    {
        public Personal()
        {
            EquipoXproyectos = new HashSet<EquipoXproyecto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Sector { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<EquipoXproyecto> EquipoXproyectos { get; set; }
    }
}
