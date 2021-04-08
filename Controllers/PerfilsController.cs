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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilsController : ControllerBase
    {

        private readonly DBEvaContext _context;

        public PerfilsController(DBEvaContext context)
        {
            _context = context;
        }

        // GET: api/<PerfilsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Perfil>>> Get()
        {
            return await _context.Perfil.ToListAsync();
        }

        // GET api/<PerfilsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Perfil>> Get(int id)
        {
            var perfils = await _context.Perfil.FindAsync(id);
            if (perfils == null)
            {
                return NotFound();
            }
            return perfils;
        }

        // POST api/<PerfilssController>
        [HttpPost]
        public async Task<ActionResult<Perfil>> Post(Perfil perfils)
        {
            _context.Perfil.Add(perfils);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = perfils.Idperfil }, perfils);
        }

        // PUT api/<PerfilsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Perfil perfils)
        {
            if (id != perfils.Idperfil)
            {
                return BadRequest();
            }
            _context.Entry(perfils).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerfilsExists(id))
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

        // DELETE api/<PerfilsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Perfil>> Delete(int id)
        {
            var perfils = await _context.Perfil.FindAsync(id);
            if (perfils == null)
            {
                return NotFound();
            }

            _context.Perfil.Remove(perfils);
            await _context.SaveChangesAsync();
            return perfils;
        }

        private bool PerfilsExists(int id)
        {
            return _context.Perfil.Any(e => e.Idperfil == id);
        }

        /// <summary>
        /// Obtener todos los perfiles
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna los datos de todos los perfiles</returns>
        [HttpPost("GetAllPerfils")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ResultPerfil>>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<IEnumerable<ResultPerfil>>> GetAllPerfils()
        {
            BaseResponse<ResultPerfil> baseResponse = new BaseResponse<ResultPerfil>();
            var resultado = new BaseResponse<IEnumerable<ResultPerfil>>();
            try
            {
                resultado.result = from F in _context.Perfil
                                   select new ResultPerfil
                                   {
                                       Idperfil = F.Idperfil,
                                       Nombreperfil = F.Nombreperfil,
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
                    resultado.mensaje = "Metodo GetAllPerfils";
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
                resultado.mensaje = "Error al Cargar Perfiles:" + ex.Message;
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Obtener informacion del perfil
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información del perfil</returns>
        [HttpPost("GetOnePerfil")]
        [ProducesResponseType(typeof(BaseResponse<ResultOpcion>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<ResultPerfil>> GetOnePerfil(int idPerfil)
        {
            BaseResponse<ResultPerfil> baseResponse = new BaseResponse<ResultPerfil>
            {
                result = new ResultPerfil()
            };
            try
            {
                var perfil = _context.Perfil.FirstOrDefault(x => x.Idperfil.Equals(idPerfil));
                if (perfil != null)
                {
                    baseResponse.result.Idperfil = perfil.Idperfil;
                    baseResponse.result.Nombreperfil = perfil.Nombreperfil;
                    baseResponse.result.Estado = perfil.Estado;
                    baseResponse.result.Crea = perfil.Crea;
                    baseResponse.result.Fechacrea = perfil.Fechacrea;
                    baseResponse.result.Modifica = perfil.Modifica;
                    baseResponse.result.Fechamodifica = perfil.Fechamodifica;

                    baseResponse.success = true;
                    baseResponse.code = "0000";
                    baseResponse.mensaje = "Metodo GetOnePerfil";
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
                baseResponse.mensaje = "Error al Cargar Perfil:" + ex.Message;
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Insertar Perfil
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la insercion del Perfil</returns>
        [HttpPost("InsertPerfil")]
        [ProducesResponseType(typeof(BaseResponse<ResultPerfilIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultPerfilIU>>> InsertPerfil(ResultPerfil request)
        {
            BaseResponse<ResultPerfilIU> baseResponse = new BaseResponse<ResultPerfilIU>
            {
                result = new ResultPerfilIU()
            };
            try
            {
                Perfil perfilinsert = new Perfil
                {
                    //Idperfil = request.Idperfil,
                    Nombreperfil = request.Nombreperfil,
                    Estado = request.Estado,
                    Crea = request.Crea,
                    Fechacrea = DateTime.Now,
                    Modifica = null,
                    Fechamodifica = null
                };
                //Insertar Registro en Perfil
                _context.Perfil.Add(perfilinsert);
                await _context.SaveChangesAsync();

                baseResponse.result.idPerfil = Convert.ToInt32(perfilinsert.Idperfil);
                baseResponse.result.mensaje = "Se insertó correctamente";

                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo InsertPerfil";
            }
            catch (Exception ex)
            {
                baseResponse.result.idPerfil = 0;
                baseResponse.result.mensaje = "No se pudo registrar el Perfil. " + ex.ToString();

                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo InsertPerfil Error";
            }

            return Ok(baseResponse);
        }

        /// <summary>
        /// Actualizar perfil
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la actualizacion del perfil</returns>
        [HttpPost("UpdatePerfil")]
        [ProducesResponseType(typeof(BaseResponse<ResultPerfilIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultPerfilIU>>> UpdatePerfil(ResultPerfil request)
        {
            //int formularioversion;

            BaseResponse<ResultPerfilIU> baseResponse = new BaseResponse<ResultPerfilIU>
            {
                result = new ResultPerfilIU()
            };
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        //Actualizar Registro en Perfil
                        Perfil perfilupdate = new Perfil
                        {
                            Idperfil = request.Idperfil,
                            Nombreperfil = request.Nombreperfil,
                            Estado = request.Estado,
                            Modifica = request.Modifica,
                            Fechamodifica = request.Fechamodifica
                        };

                        _context.Perfil.Update(perfilupdate);
                        await _context.SaveChangesAsync();
                        transaction.Commit();

                        baseResponse.result.idPerfil = Convert.ToInt32(perfilupdate.Idperfil);
                        baseResponse.result.mensaje = "Se insertó correctamente";

                        baseResponse.success = true;
                        baseResponse.code = "00000";
                        baseResponse.mensaje = "Metodo UpdatePerfil";

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        baseResponse.result.idPerfil = 0;
                        baseResponse.result.mensaje = "No se pudo actualizar el Perfil. " + ex.ToString();
                        baseResponse.success = false;
                        baseResponse.code = "-1";
                        baseResponse.mensaje = "Metodo UpudatePerfil Error";
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.result.idPerfil = 0;
                baseResponse.result.mensaje = "No se pudo actualizar el Perfil. " + ex.ToString();
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo UpudatePerfil Error";
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Eliminar perfil
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la eliminacion del perfil</returns>
        [HttpPost("DeletePerfil")]
        [ProducesResponseType(typeof(BaseResponse<int?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<int?>>> DeletePerfil(RequestOnePerfil request)
        {
            BaseResponse<int?> baseResponse = new BaseResponse<int?>();
            try
            {
                //Eliminar Perfil
                var perfil = _context.Perfil.FirstOrDefault(x => x.Idperfil.Equals(request.idPerfil));
                if (perfil == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Perfil.Remove(perfil);
                    await _context.SaveChangesAsync();
                }
                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo DeletePerfil";
            }
            catch (Exception ex)
            {
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo DeletePerfil Error. " + ex.ToString();
                return NotFound();
            }
            baseResponse.result = null;
            return Ok(baseResponse);
        }

    }
}