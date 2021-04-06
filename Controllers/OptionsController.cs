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
    public class OptionsController : ControllerBase
    {

        private readonly DBEvaContext _context;

        public OptionsController(DBEvaContext context)
        {
            _context = context;
        }

        // GET: api/<OptionsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Opcion>>> Get()
        {
            return await _context.Opcion.ToListAsync();
        }

        // GET api/<OptionsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Opcion>> Get(int id)
        {
            var opcions = await _context.Opcion.FindAsync(id);
            if (opcions == null)
            {
                return NotFound();
            }
            return opcions;
        }

        // POST api/<OptionsController>
        [HttpPost]
        public async Task<ActionResult<Opcion>> Post(Opcion opcions)
        {
            _context.Opcion.Add(opcions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = opcions.Idopcion }, opcions);
        }

        // PUT api/<OptionsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Opcion opcions)
        {
            if (id != opcions.Idopcion)
            {
                return BadRequest();
            }
            _context.Entry(opcions).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpcionsExists(id))
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

        // DELETE api/<OptionsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Opcion>> Delete(int id)
        {
            var opcions = await _context.Opcion.FindAsync(id);
            if (opcions == null)
            {
                return NotFound();
            }

            _context.Opcion.Remove(opcions);
            await _context.SaveChangesAsync();
            return opcions;
        }

        private bool OpcionsExists(int id)
        {
            return _context.Opcion.Any(e => e.Idopcion == id);
        }

        /// <summary>
        /// Obtener todos las opciones
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna los datos de todos las opciones</returns>
        [HttpPost("GetAllOpcions")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ResultOpcion>>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<IEnumerable<ResultOpcion>>> GetAllOpcions()
        {
            BaseResponse<ResultOpcion> baseResponse = new BaseResponse<ResultOpcion>();
            var resultado = new BaseResponse<IEnumerable<ResultOpcion>>();
            try
            {
                resultado.result = from F in _context.Opcion
                                   select new ResultOpcion
                                   {
                                       Idopcion = F.Idopcion,
                                       Nombreopcion = F.Nombreopcion,
                                       Rutaopcion = F.Rutaopcion,
                                       Estado = F.Estado,
                                       Crea = F.Crea,
                                       Fechacrea = F.Fechacrea,
                                       Modifica = F.Modifica,
                                       Fechamodifica = F.Fechamodifica
                                   };
                if (resultado.result != null)
                {
                    resultado.success = true;
                    resultado.code = "0000";
                    resultado.mensaje = "Metodo GetAllOpcions";
                }
                else
                {
                    resultado.success = false;
                    resultado.code = "-1";
                    resultado.mensaje = "No se encontró registros";
                }
            }
            catch (Exception ex)
            {
                resultado.success = false;
                resultado.code = "-1";
                resultado.mensaje = "Error al Cargar Opciones:" + ex.Message;
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Obtener informacion de la opcion
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la opcion</returns>
        [HttpPost("GetOneOpcion")]
        [ProducesResponseType(typeof(BaseResponse<ResultOpcion>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<ResultOpcion>> GetOneOpcion(int idOpcion)
        {
            BaseResponse<ResultOpcion> baseResponse = new BaseResponse<ResultOpcion>
            {
                result = new ResultOpcion()
            };
            try
            {
                var opcion = _context.Opcion.FirstOrDefault(x => x.Idopcion.Equals(idOpcion));
                if (opcion != null)
                {
                    baseResponse.result.Idopcion = opcion.Idopcion;
                    baseResponse.result.Nombreopcion = opcion.Nombreopcion;
                    baseResponse.result.Rutaopcion = opcion.Rutaopcion;
                    baseResponse.result.Estado = opcion.Estado;
                    baseResponse.result.Crea = opcion.Crea;
                    baseResponse.result.Fechacrea = opcion.Fechacrea;
                    baseResponse.result.Modifica = opcion.Modifica;
                    baseResponse.result.Fechamodifica = opcion.Fechamodifica;

                    baseResponse.success = true;
                    baseResponse.code = "0000";
                    baseResponse.mensaje = "Metodo GetOneOpcion";
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
                baseResponse.mensaje = "Error al Cargar Opciones:" + ex.Message;
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Insertar Opcion
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la insercion de la Opcion</returns>
        [HttpPost("InsertOpcion")]
        [ProducesResponseType(typeof(BaseResponse<ResultOpcionIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultOpcionIU>>> InsertOpcion(ResultOpcion request)
        {
            BaseResponse<ResultOpcionIU> baseResponse = new BaseResponse<ResultOpcionIU>
            {
                result = new ResultOpcionIU()
            };
            try
            {
                Opcion opcioninsert = new Opcion
                {
                    Idopcion = request.Idopcion,
                    Nombreopcion = request.Nombreopcion,
                    Rutaopcion = request.Rutaopcion,
                    Estado = request.Estado,
                    Crea = request.Crea,
                    Fechacrea = DateTime.Now,
                    Modifica = null,
                    Fechamodifica = null
                };
                //Insertar Registro en Opcion
                _context.Opcion.Add(opcioninsert);
                await _context.SaveChangesAsync();

                baseResponse.result.idOpcion = Convert.ToInt32(opcioninsert.Idopcion);
                baseResponse.result.mensaje = "Se insertó correctamente";

                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo InsertOpcion";
            }
            catch (Exception ex)
            {
                baseResponse.result.idOpcion = 0;
                baseResponse.result.mensaje = "No se pudo registrar la opción. " + ex.ToString();

                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo InsertOpcion Error";
            }

            return Ok(baseResponse);
        }

        /// <summary>
        /// Actualizar opcion
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la actualizacion de la opcion</returns>
        [HttpPost("UpdateOpcion")]
        [ProducesResponseType(typeof(BaseResponse<ResultOpcionIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultOpcionIU>>> UpdateOpcion(ResultOpcion request)
        {
            //int formularioversion;

            BaseResponse<ResultOpcionIU> baseResponse = new BaseResponse<ResultOpcionIU>
            {
                result = new ResultOpcionIU()
            };
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //Actualizar Registro en Opcion
                        Opcion opcionupdate = new Opcion
                        {
                            Idopcion = request.Idopcion,
                            Nombreopcion = request.Nombreopcion,
                            Rutaopcion = request.Rutaopcion,
                            Estado = request.Estado,
                            Modifica = request.Modifica,
                            Fechamodifica = request.Fechamodifica
                        };

                        _context.Opcion.Update(opcionupdate);
                        await _context.SaveChangesAsync();
                        transaction.Commit();

                        baseResponse.result.idOpcion = Convert.ToInt32(opcionupdate.Idopcion);
                        baseResponse.result.mensaje = "Se actualizó correctamente";

                        baseResponse.success = true;
                        baseResponse.code = "00000";
                        baseResponse.mensaje = "Metodo UpdateOpcion";

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        baseResponse.result.idOpcion = 0;
                        baseResponse.result.mensaje = "No se pudo actualizar la opción. " + ex.ToString();
                        baseResponse.success = false;
                        baseResponse.code = "-1";
                        baseResponse.mensaje = "Metodo UpudateOpcion Error";

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.result.idOpcion = 0;
                baseResponse.result.mensaje = "No se pudo actualizar la Opción. " + ex.ToString();
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo UpudateOpcion Error";
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Eliminar opcion
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la eliminacion de la opcion</returns>
        [HttpPost("DeleteOpcion")]
        [ProducesResponseType(typeof(BaseResponse<int?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<int?>>> DeleteOpcion(int idOpcion)
        {
            BaseResponse<int?> baseResponse = new BaseResponse<int?>();
            try
            {
                //Eliminar Opcion
                var opcion = _context.Opcion.FirstOrDefault(x => x.Idopcion.Equals(idOpcion));
                if (opcion == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Opcion.Remove(opcion);
                    await _context.SaveChangesAsync();
                }
                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo DeleteOpcion";
            }
            catch (Exception ex)
            {
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo DeleteOpcion Error. " + ex.ToString();
                return NotFound();
            }
            baseResponse.result = null;
            return Ok(baseResponse);
        }

    }
}