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
    public class UbigeosController : ControllerBase
    {

        private readonly DBEvaContext _context;

        public UbigeosController(DBEvaContext context)
        {
            _context = context;
        }

        // GET: api/<UbigeosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ubigeo>>> Get()
        {
            return await _context.Ubigeo.ToListAsync();
        }

        // GET api/<UbigeosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ubigeo>> Get(int id)
        {
            var ubigeos = await _context.Ubigeo.FindAsync(id);
            if (ubigeos == null)
            {
                return NotFound();
            }
            return ubigeos;
        }

        // POST api/<UbigeosController>
        [HttpPost]
        public async Task<ActionResult<Ubigeo>> Post(Ubigeo ubigeos)
        {
            _context.Ubigeo.Add(ubigeos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = ubigeos.Idubigeo }, ubigeos);
        }

        // PUT api/<UbigeosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Ubigeo ubigeos)
        {
            if (id != ubigeos.Idubigeo)
            {
                return BadRequest();
            }
            _context.Entry(ubigeos).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UbigeosExists(id))
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

        // DELETE api/<UbigeosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Ubigeo>> Delete(int id)
        {
            var ubigeos = await _context.Ubigeo.FindAsync(id);
            if (ubigeos == null)
            {
                return NotFound();
            }

            _context.Ubigeo.Remove(ubigeos);
            await _context.SaveChangesAsync();
            return ubigeos;
        }

        private bool UbigeosExists(int id)
        {
            return _context.Ubigeo.Any(e => e.Idubigeo == id);
        }

        /// <summary>
        /// Obtener todos los ubigeos
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna los datos de todos los ubigeos</returns>
        [HttpPost("GetAllUbigeos")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ResultUbigeo>>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<IEnumerable<ResultUbigeo>>> GetAllPerfils()
        {
            BaseResponse<ResultUbigeo> baseResponse = new BaseResponse<ResultUbigeo>();
            var resultado = new BaseResponse<IEnumerable<ResultUbigeo>>();
            try
            {
                resultado.result = from F in _context.Ubigeo
                                   select new ResultUbigeo
                                   {
                                       Idubigeo = F.Idubigeo,
                                       Codgoubigeo = F.Codigoubigeo,
                                       Departamento = F.Departamento,
                                       Provincia = F.Provincia,
                                       Distrito = F.Distrito,
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
                    resultado.mensaje = "Metodo GetAllUbigeos";
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
                resultado.mensaje = "Error al Cargar Ubigeos:" + ex.Message;
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Obtener informacion del ubigeo
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información del ubigeo</returns>
        [HttpPost("GetOneUbigeo")]
        [ProducesResponseType(typeof(BaseResponse<ResultUbigeo>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<ResultUbigeo>> GetOnePerfil(int idUbigeo)
        {
            BaseResponse<ResultUbigeo> baseResponse = new BaseResponse<ResultUbigeo>
            {
                result = new ResultUbigeo()
            };
            try
            {
                var ubigeo = _context.Ubigeo.FirstOrDefault(x => x.Idubigeo.Equals(idUbigeo));
                if (ubigeo != null)
                {
                    baseResponse.result.Idubigeo = ubigeo.Idubigeo;
                    baseResponse.result.Codgoubigeo = ubigeo.Codigoubigeo;
                    baseResponse.result.Departamento = ubigeo.Departamento;
                    baseResponse.result.Provincia = ubigeo.Provincia;
                    baseResponse.result.Distrito = ubigeo.Distrito;
                    baseResponse.result.Estado = ubigeo.Estado;
                    baseResponse.result.Crea = ubigeo.Crea;
                    baseResponse.result.Fechacrea = ubigeo.Fechacrea;
                    baseResponse.result.Modifica = ubigeo.Modifica;
                    baseResponse.result.Fechamodifica = ubigeo.Fechamodifica;

                    baseResponse.success = true;
                    baseResponse.code = "0000";
                    baseResponse.mensaje = "Metodo GetOneUbigeo";
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
                baseResponse.mensaje = "Error al Cargar Ubigeo:" + ex.Message;
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Insertar Ubigeo
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la insercion del Ubigeo</returns>
        [HttpPost("InsertUbigeo")]
        [ProducesResponseType(typeof(BaseResponse<ResultUbigeoIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultUbigeoIU>>> InsertUbigeo(ResultUbigeo request)
        {
            BaseResponse<ResultUbigeoIU> baseResponse = new BaseResponse<ResultUbigeoIU>
            {
                result = new ResultUbigeoIU()
            };
            try
            {
                Ubigeo ubigeoinsert = new Ubigeo
                {
                    Idubigeo = request.Idubigeo,
                    Codigoubigeo = request.Codgoubigeo,
                    Departamento = request.Departamento,
                    Provincia = request.Provincia,
                    Distrito = request.Distrito,
                    Estado = request.Estado,
                    Crea = request.Crea,
                    Fechacrea = DateTime.Now,
                    Modifica = null,
                    Fechamodifica = null
                };
                //Insertar Registro en Ubigeo
                _context.Ubigeo.Add(ubigeoinsert);
                await _context.SaveChangesAsync();

                baseResponse.result.idUbigeo = ubigeoinsert.Idubigeo;
                baseResponse.result.mensaje = "Se insertó correctamente";

                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo InsertUbigeo";
            }
            catch (Exception ex)
            {
                baseResponse.result.idUbigeo = 0;
                baseResponse.result.mensaje = "No se pudo registrar el Ubigeo. " + ex.ToString();

                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo InsertUbigeo Error";
            }

            return Ok(baseResponse);
        }

        /// <summary>
        /// Actualizar ubigeo
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la actualizacion del ubigeo</returns>
        [HttpPost("UpdateUbigeo")]
        [ProducesResponseType(typeof(BaseResponse<ResultUbigeoIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultUbigeoIU>>> UpdateUbigeo(ResultUbigeo request)
        {
            BaseResponse<ResultUbigeoIU> baseResponse = new BaseResponse<ResultUbigeoIU>
            {
                result = new ResultUbigeoIU()
            };
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Ubigeo ubigeoupdate = new Ubigeo
                        {
                            Idubigeo = request.Idubigeo,
                            Codigoubigeo = request.Codgoubigeo,
                            Departamento = request.Departamento,
                            Provincia = request.Provincia,
                            Distrito = request.Distrito,
                            Estado = request.Estado,
                            Modifica = request.Modifica,
                            Fechamodifica = request.Fechamodifica
                        };

                        _context.Ubigeo.Update(ubigeoupdate);
                        await _context.SaveChangesAsync();
                        transaction.Commit();

                        baseResponse.result.idUbigeo = ubigeoupdate.Idubigeo;
                        baseResponse.result.mensaje = "Se actualizó correctamente";

                        baseResponse.success = true;
                        baseResponse.code = "00000";
                        baseResponse.mensaje = "Metodo UpdateUbigeo";

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        baseResponse.result.idUbigeo = 0;
                        baseResponse.result.mensaje = "No se pudo actualizar el Ubigeo. " + ex.ToString();
                        baseResponse.success = false;
                        baseResponse.code = "-1";
                        baseResponse.mensaje = "Metodo UpudateUbigeo Error";
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.result.idUbigeo = 0;
                baseResponse.result.mensaje = "No se pudo actualizar el Ubigeo. " + ex.ToString();
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo UpudateUbigeo Error";
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Eliminar ubigeo
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la eliminacion del ubigeo</returns>
        [HttpPost("DeleteUbigeo")]
        [ProducesResponseType(typeof(BaseResponse<int?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<int?>>> DeleteUbigeo(int Idubigeo)
        {
            BaseResponse<int?> baseResponse = new BaseResponse<int?>();
            try
            {
                //Eliminar Ubigeo
                var ubigeo = _context.Ubigeo.FirstOrDefault(x => x.Idubigeo.Equals(Idubigeo));
                if (ubigeo == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Ubigeo.Remove(ubigeo);
                    await _context.SaveChangesAsync();
                }
                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo DeleteUbigeo";
            }
            catch (Exception ex)
            {
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo DeleteUbigeo Error. " + ex.ToString();
                return NotFound();
            }
            baseResponse.result = null;
            return Ok(baseResponse);
        }

    }
}