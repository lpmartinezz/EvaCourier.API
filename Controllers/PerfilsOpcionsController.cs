using EvaCourier.API.Models;
using EvaCourier.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EvaCourier.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilsOpcionsController : ControllerBase
    {

        private readonly DBEvaContext _context;

        public PerfilsOpcionsController(DBEvaContext context)
        {
            _context = context;
        }

        // GET: api/<PerfilsOpcionsControllerController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfilopcion>>> Get()
        {
            return await _context.Perfilopcion.ToListAsync();
        }

        // GET api/<PerfilsOpcionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfilopcion>> Get(int id)
        {
            var perfilsopcions = await _context.Perfilopcion.FindAsync(id);
            if (perfilsopcions == null)
            {
                return NotFound();
            }
            return perfilsopcions;
        }

        // POST api/<PerfilsOpcionsController>
        [HttpPost]
        public async Task<ActionResult<Perfilopcion>> Post(Perfilopcion perfilsopcions)
        {
            _context.Perfilopcion.Add(perfilsopcions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = perfilsopcions.Idperfilopcion }, perfilsopcions);
        }

        // PUT api/<PerfilsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Perfilopcion perfilsopcions)
        {
            if (id != perfilsopcions.Idperfilopcion)
            {
                return BadRequest();
            }
            _context.Entry(perfilsopcions).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilsOpcionsExists(id))
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

        // DELETE api/<PerfilsOpcionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Perfilopcion>> Delete(int id)
        {
            var perfilsopcions = await _context.Perfilopcion.FindAsync(id);
            if (perfilsopcions == null)
            {
                return NotFound();
            }
            _context.Perfilopcion.Remove(perfilsopcions);
            await _context.SaveChangesAsync();
            return perfilsopcions;
        }

        /// <summary>
        /// Obtener infor del formulario
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información del formulario</returns>
        [HttpPost("GetOnePerfilOpcion")]
        [ProducesResponseType(typeof(BaseResponse<ResultPerfil>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultPerfil>>> GetOnePerfilOpcion(RequestOnePerfil request)
        {
            BaseResponse<ResultPerfil> baseResponse = new BaseResponse<ResultPerfil>
            {
                result = new ResultPerfil()
            };
            try
            {
                var perfil = _context.Perfil.FirstOrDefault(x => x.Idperfil.Equals(request.idPerfil));
                if (perfil != null)
                {
                    baseResponse.result.Idperfil = perfil.Idperfil;
                    baseResponse.result.Nombreperfil = perfil.Nombreperfil;
                    baseResponse.result.Estado = perfil.Estado;
                    baseResponse.result.Crea = perfil.Crea;
                    baseResponse.result.Fechacrea = perfil.Fechacrea;
                    baseResponse.result.opcions = (IEnumerable<ResultOpcion>)OpcionsByPerfilId(perfil.Idperfil);

                    baseResponse.success = true;
                    baseResponse.code = "0000";
                    baseResponse.mensaje = "Metodo GetPerfilOpciones";
                }
                else
                {
                    baseResponse.success = false;
                    baseResponse.code = "-1";
                    baseResponse.mensaje = "No se encontró registros";
                }
            }
            catch (Exception ex)
            {
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Error al Cargar Formularios:" + ex.Message;
                throw;
            }
            return Ok(baseResponse);
        }

        private bool PerfilsOpcionsExists(int id)
        {
            return _context.Perfil.Any(e => e.Idperfil == id);
        }

        private async Task<IEnumerable<Entities.ResultOpcion>> OpcionsByPerfilId(int idperfil)
        {
            var Vopcion = await _context.Perfilopcion.Where(x => x.Idperfil.Equals(idperfil) && x.Estado.Equals(true)).ToListAsync();
            var opcion = from F in Vopcion
                         join c in _context.Opcion on F.Idopcion equals c.Idopcion into opciones
                         from opc in opciones.DefaultIfEmpty()
                         select new Entities.ResultOpcion
                          {
                              Idopcion = opc.Idopcion,
                              Nombreopcion = opc.Nombreopcion,
                              Estado = opc.Estado
                          };
            return opcion;
        }

    }
}