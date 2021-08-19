using Fundacion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundacion.Comandos
{
    public class InsertarProyecto
    {
        public int? IdArea { get; set; } //puede ser nulo
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

        public List<int> ListaPersonal { get; set; }
        public List<int> ListaAreas { get; set; }



    }
}
