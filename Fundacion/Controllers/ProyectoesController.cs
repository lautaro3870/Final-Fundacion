using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundacion.Models;
using Fundacion.Resultados;
using Fundacion.Comandos;

namespace Fundacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoesController : ControllerBase
    {
        private readonly db_Fundacion_FinalContext _context = new db_Fundacion_FinalContext();

        //public ProyectoesController(db_Fundacion_FinalContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Proyectoes
        [HttpGet]
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
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyecto(int id, Proyecto proyecto)
        {
            if (id != proyecto.Id)
            {
                return BadRequest();
            }

            _context.Entry(proyecto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Proyectoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proyecto>> PostProyecto(InsertarProyecto comando)
        {
            //_context.Proyectos.Add(proyecto);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetProyecto", new { id = proyecto.Id }, proyecto);

            var resultado = new ResultadosApi();

            var proyecto = new Proyecto
            {
                IdArea = comando.IdArea,
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
                foreach(var id in comando.ListaPersonal)
                {
                    var equipoXproyecto = new EquipoXproyecto
                    {
                        IdPersonal = id,
                        IdProyecto = proyecto.Id
                    };
                    _context.EquipoXproyectos.Add(equipoXproyecto);
                }
            }

            //if (comando.ListaAreas != null)
            //{
            //    foreach (var id in comando.ListaAreas)
            //    {
            //        var areaXproyecto = new AreasxProyecto
            //        {
            //            IdArea = id,
            //            IdProyecto = proyecto.Id
            //        };
            //        _context.AreasxProyectos.Add(areaXproyecto);
            //    }
            //}

            await _context.SaveChangesAsync();
            

            return proyecto;


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
    }
}
