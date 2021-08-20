using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundacion.Comandos
{
    public class UpdateProyecto
    {

        public int Id { get; set; } 
        public string Titulo { get; set; }
        public string PaisRegion { get; set; }
        public string Contratante { get; set; }

        public List<int> ListaPersonal { get; set; }
        public List<int> ListaAreas { get; set; }


    }
}
