using System;
using System.Collections.Generic;

#nullable disable

namespace Fundacion.Models
{
    public partial class PublicacionesXproyecto
    {
        public int IdPublicacion { get; set; }
        public int? IdProyecto { get; set; }
        public string Publicacion { get; set; }
        public string Año { get; set; }
        public string Medio { get; set; }
        public string CodigoBcs { get; set; }

        public virtual Proyecto IdProyectoNavigation { get; set; }
    }
}
