using System;
using System.Collections.Generic;

#nullable disable

namespace Fundacion.Models
{
    public partial class EquipoXproyecto
    {
        public int IdProyecto { get; set; }
        public int IdPersonal { get; set; }
        public string Texto { get; set; }
        public string FuncionTarea { get; set; }
        public bool? Coordinador { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual Personal IdPersonalNavigation { get; set; }
        public virtual Proyecto IdProyectoNavigation { get; set; }
    }
}
