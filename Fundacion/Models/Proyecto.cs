using System;
using System.Collections.Generic;

#nullable disable

namespace Fundacion.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            AreasxProyectos = new HashSet<AreasxProyecto>();
            EquipoXproyectos = new HashSet<EquipoXproyecto>();
            PublicacionesXproyectos = new HashSet<PublicacionesXproyecto>();
        }

        public int Id { get; set; }
        public int? IdArea { get; set; }
        public string Titulo { get; set; }
        public string PaisRegion { get; set; }
        public string Contratante { get; set; }
        public string Dirección { get; set; }
        public string MontoContrato { get; set; }
        public string NroContrato { get; set; }
        public int? MesInicio { get; set; }
        public int? AnioInicio { get; set; }
        public int? MesFinalizacion { get; set; }
        public int? AnioFinalizacion { get; set; }
        public string ConsultoresAsoc { get; set; }
        public string Descripcion { get; set; }
        public string Resultados { get; set; }
        public bool? FichaLista { get; set; }
        public bool? EnCurso { get; set; }
        public string Departamento { get; set; }
        public string Moneda { get; set; }
        public bool? CertConformidad { get; set; }
        public int? CertificadoPor { get; set; }
        public byte[] SsmaTimeStamp { get; set; }

        public virtual ICollection<AreasxProyecto> AreasxProyectos { get; set; }
        public virtual ICollection<EquipoXproyecto> EquipoXproyectos { get; set; }
        public virtual ICollection<PublicacionesXproyecto> PublicacionesXproyectos { get; set; }
    }
}
