using Fundacion.Ado;
using Fundacion.Comandos;
using Fundacion.Models;
using Fundacion.Resultados;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Prog3")]
    public class ProyectoesController : ControllerBase
    {
        private readonly db_Fundacion_FinalContext _context = new db_Fundacion_FinalContext();

        //public ProyectoesController(db_Fundacion_FinalContext context)
        //{
        //    _context = context;
        //}

        //GET: api/Proyectoes
        [HttpGet("GetProyectos")]
        public async Task<ActionResult<IEnumerable<Proyecto>>> GetProyectos()
        {

            return await _context.Proyectos.ToListAsync();
        }

        // GET: api/Proyectoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyecto>> GetProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);

            if (proyecto == null)
            {
                return NotFound();
            }

            return proyecto;
        }

        // PUT: api/Proyectoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProyecto(int id, Proyecto proyecto)
        //{
        //    if (id != proyecto.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(proyecto).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProyectoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}


        //Revisar el put: al querer actualizar un proyecto con el mismo idArea que ya tiene, salta un error 500

        [HttpPut]
        public async Task<ActionResult<Proyecto>> Put2(UpdateProyecto comando)
        {
            var proyecto = await _context.Proyectos.FindAsync(comando.Id);

            if (proyecto == null)
            {
                return NotFound("Proyecto no encontrado");
            }


            proyecto.Titulo = comando.Titulo;
            proyecto.PaisRegion = comando.PaisRegion;
            proyecto.Contratante = comando.Contratante;

            if (comando.ListaPersonal != null)
            {
                var personal = _context.EquipoXproyectos.Where(x => x.IdProyecto == comando.Id);

                foreach (var x in personal)
                {
                    _context.EquipoXproyectos.Remove(x);
                }

                foreach (var id in comando.ListaPersonal)
                {
                    var equipoXproyecto = new EquipoXproyecto
                    {
                        IdPersonal = id,
                        IdProyecto = comando.Id
                    };
                    _context.EquipoXproyectos.Add(equipoXproyecto);
                }
            }

            if (comando.ListaAreas != null)
            {
                var aereas = _context.AreasxProyectos.Where(x => x.IdProyecto == comando.Id);

                foreach (var x in aereas)
                {
                    _context.AreasxProyectos.Remove(x);
                }

                foreach (var id in comando.ListaAreas)
                {
                    var areaXproyecto = new AreasxProyecto
                    {
                        IdArea = id,
                        IdProyecto = comando.Id
                    };
                    _context.AreasxProyectos.Add(areaXproyecto);
                }
            }

            //_context.Proyectos.Update(proyecto);
            _context.SaveChanges();

            return proyecto;
        }


        [HttpGet("GetAreas")]
        public ActionResult<ResultadosApi> GetAreas()
        {
            var resultado = new ResultadosApi();

            resultado.Ok = true;
            resultado.Return = _context.Areas.ToList();
            return resultado;
        }

        [HttpGet("GetDeptos")]
        public ActionResult<ResultadosApi> GetDeptos()
        {
            var resultado = new ResultadosApi();

            resultado.Ok = true;
            resultado.Return = _context.Departamentos.ToList();
            return resultado;
        }

        // POST: api/Proyectoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ResultadosApi>> PostProyecto(InsertarProyecto comando)
        {
            //_context.Proyectos.Add(proyecto);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetProyecto", new { id = proyecto.Id }, proyecto);

            var resultado = new ResultadosApi();

            try
            {
                var proyecto = new Proyecto
                {
                    //IdArea = comando.IdArea,
                    Titulo = comando.Titulo,
                    PaisRegion = comando.PaisRegion,
                    Contratante = comando.Contratante,
                    Dirección = comando.Dirección,
                    MontoContrato = comando.MontoContrato,
                    NroContrato = comando.NroContrato,
                    MesInicio = comando.MesInicio,
                    AnioInicio = comando.AnioInicio,
                    MesFinalizacion = comando.MesFinalizacion,
                    AnioFinalizacion = comando.AnioFinalizacion,
                    ConsultoresAsoc = comando.ConsultoresAsoc,
                    Descripcion = comando.Descripcion,
                    Resultados = comando.Resultados,
                    FichaLista = comando.FichaLista,
                    EnCurso = comando.EnCurso,
                    Departamento = comando.Departamento,
                    Moneda = comando.Moneda,
                    CertConformidad = comando.CertConformidad,
                    CertificadoPor = comando.CertificadoPor



                };

                _context.Proyectos.Add(proyecto);

                await _context.SaveChangesAsync();


                if (comando.ListaPersonal != null)
                {
                    foreach (var id in comando.ListaPersonal)
                    {
                        var equipoXproyecto = new EquipoXproyecto
                        {
                            IdPersonal = id,
                            IdProyecto = proyecto.Id
                        };
                        _context.EquipoXproyectos.Add(equipoXproyecto);
                    }
                }

                if (comando.ListaAreas != null)
                {
                    foreach (var id in comando.ListaAreas)
                    {
                        var areaXproyecto = new AreasxProyecto
                        {
                            IdArea = id,
                            IdProyecto = proyecto.Id
                        };
                        _context.AreasxProyectos.Add(areaXproyecto);
                    }
                }

                await _context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.Return = proyecto;
                return resultado;

            }
            catch (Exception e)
            {
                resultado.Ok = false;
                resultado.CodigoError = 4;
                return resultado;

            }


        }


        // DELETE: api/Proyectoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var proyecto = await _context.Proyectos.FindAsync(id);
            if (proyecto == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyecto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProyectoExists(int id)
        {
            return _context.Proyectos.Any(e => e.Id == id);
        }

        //[HttpGet]
        //public async Task<ActionResult<List<Proyecto>>> GetConsulta()
        //{

        //    //Holaa
        //    var proyecto = await _context.Proyectos.Include(x => x.AreasxProyectos).Include(x => x.EquipoXproyectos).ToListAsync();

        //    return proyecto;

        //}

        [HttpGet]
        public ActionResult<ResultadosApi> GetConsulta()
        {

            var resultado = new ResultadosApi();
            //var proyecto = await _context.Proyectos.Include(x => x.AreasxProyectos).Include(x => x.EquipoXproyectos).ToListAsync();
            //var proyecto = await _context.Proyectos.Include(x => x.AreasxProyectos).ThenInclude(x => x.IdAreaNavigation).ToListAsync();
            //var proyecto =
            //       from proy in _context.Proyectos
            //       join ap in _context.AreasxProyectos on proy.Id equals ap.IdProyecto
            //       join are in _context.Areas on ap.IdArea equals are.Id
            //       select new { Proy = proy, Ap = ap, Are = are  };
            //var proyecto = await _context.Database.ExecuteSqlRawAsync("select p.Id, p.Titulo,p.[Pais-region],p.Contratante, a.Area from Proyectos p join AreasxProyecto ap on p.Id = ap.IdProyecto join Areas a on a.Id = ap.IdArea");


            //LAUTARO: SI VES EL EXPLORADOR DE SOLUCIONES CREE UNA CARPETA LLAMADA ADO (si, recurri a esa vieja tecnica milenaria)
            // BUENO AHI ME CREE UNA CLASE CONEXION Y LUEGO UN METODO PARA LEER LA CONSULTA QUE HICE, EN ESTA CLASE AQUI DEBAJO
            // HICE LA LOGICA PARA PASAR ESOS DATSO EN UNA LISTA Y ASI PODER DEVOLVER ESA LISTA Y MANIPULARLA EN EL FRONT
            // HA Y CREE UNA CLASE EN COMANDOS LLAMADA CONSULTAPROYECTOS PARA PODER CREAR UN OBJETO CON LOS DATOS
            // QUE ME TRAJO ESA CONSULTA Y METERLO A ESA LISTA.

            Conexion conexion = new Conexion();
            ConsultaProyectos p = null;


            List<object> lista = new List<object>();
            conexion.Leer();
            lista.Clear();
            while (conexion.Dr.Read())
            {
                p = new ConsultaProyectos();
                if (!conexion.Dr.IsDBNull(0))
                {
                    p.Id = conexion.Dr.GetInt32(0);
                }
                if (!conexion.Dr.IsDBNull(1))
                {
                    p.Titulo = conexion.Dr.GetString(1);
                }
                if (!conexion.Dr.IsDBNull(2))
                {
                    p.PaisRegion = conexion.Dr.GetString(2);
                }
                if (!conexion.Dr.IsDBNull(3))
                {
                    p.Contratante = conexion.Dr.GetString(3);
                }
                if (!conexion.Dr.IsDBNull(4))
                {
                    p.Area = conexion.Dr.GetString(4);
                }
                lista.Add(p);
            }
            conexion.CerrarConexion();
            resultado.Ok = true;
            resultado.Return = lista;
            return resultado;



        }

    }
}
