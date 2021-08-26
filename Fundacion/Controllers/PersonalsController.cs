using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundacion.Models;
using Fundacion.Comandos;
using Fundacion.Resultados;
using Microsoft.AspNetCore.Cors;

namespace Fundacion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("Prog3")]
    public class PersonalsController : ControllerBase
    {
        private readonly db_Fundacion_FinalContext _context = new db_Fundacion_FinalContext();

        //public PersonalsController(db_Fundacion_FinalContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Personals
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Personal>>> GetPersonals()
        //{

               
        //    return await _context.Personals.ToListAsync();
        //}

        [HttpGet]
        public ActionResult<ResultadosApi> GetPersonals()
        {
            var r = new ResultadosApi();
            var personas = _context.Personals.ToList();
            r.Ok = true;
            r.Return = personas;
            return r;

        }

        // GET: api/Personals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personal>> GetPersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);

            if (personal == null)
            {
                return NotFound();
            }

            return personal;
        }

        // PUT: api/Personals/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonal(int id, Personal personal)
        {
            if (id != personal.Id)
            {
                return BadRequest();
            }

            _context.Entry(personal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalExists(id))
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

        // POST: api/Personals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("Login")]
        public async Task<ActionResult<ResultadosApi>> Login([FromBody]InsertarPersonal cmd)
        {
            //_context.Personals.Add(personal);
            //await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPersonal", new { id = personal.Id }, personal);

            var resultado = new ResultadosApi();

            var email = cmd.Email.Trim();
            var password = cmd.Password;

            try
            {
                var personal = await _context.Personals.FirstOrDefaultAsync(x => x.Email.Equals(email) && x.Password.Equals(password));
                if (personal != null)
                {
                    resultado.Ok = true;
                    resultado.Return = personal;
                    if (personal.Activo == false)
                    {
                        resultado.Error = "Usuario bloqueado";
                        return resultado;
                    }
                    
                }
                else
                {
                    resultado.Ok = false;
                    resultado.Error = "Usuario o contraseña incorrectos";
                }
                return resultado;

            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Usuario no encontrado";
                resultado.CodigoError = 1;
                return resultado;
            }
            

        }

        // DELETE: api/Personals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonal(int id)
        {
            var personal = await _context.Personals.FindAsync(id);
            if (personal == null)
            {
                return NotFound();
            }

            _context.Personals.Remove(personal);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonalExists(int id)
        {
            return _context.Personals.Any(e => e.Id == id);
        }
    }
}
