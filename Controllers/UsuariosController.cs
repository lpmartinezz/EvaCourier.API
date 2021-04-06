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
    public class UsuariosController : ControllerBase
    {

        private readonly DBEvaContext _context;

        public UsuariosController(DBEvaContext context)
        {
            _context = context;
        }

        // GET: api/<UsuariosController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            return await _context.Usuario.ToListAsync();
        }

        // GET api/<UsuariosController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> Get(int id)
        {
            var usuarios = await _context.Usuario.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return usuarios;
        }

        // POST api/<UsuariosController>
        [HttpPost]
        public async Task<ActionResult<Usuario>> Post(Usuario usuarios)
        {
            _context.Usuario.Add(usuarios);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = usuarios.Idusuario }, usuarios);
        }

        // PUT api/<UsuariosController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Usuario usuarios)
        {
            if (id != usuarios.Idusuario)
            {
                return BadRequest();
            }
            _context.Entry(usuarios).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuariosExists(id))
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

        // DELETE api/<UsuariosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Usuario>> Delete(int id)
        {
            var usuarios = await _context.Usuario.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuarios);
            await _context.SaveChangesAsync();
            return usuarios;
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuario.Any(e => e.Idusuario == id);
        }

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna los datos de todos los usuarios</returns>
        [HttpPost("GetAllUsuarios")]
        [ProducesResponseType(typeof(BaseResponse<IEnumerable<ResultUsuario>>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<IEnumerable<ResultUsuario>>> GetAllUsuarios()
        {
            BaseResponse<ResultUsuario> baseResponse = new BaseResponse<ResultUsuario>();
            var resultado = new BaseResponse<IEnumerable<ResultUsuario>>();
            try
            {
                resultado.result = from u in _context.Usuario
                                   join p in _context.Perfil on u.Idperfil equals p.Idperfil
                                   join ub in _context.Ubigeo on u.Idubigeo equals ub.Idubigeo
                                   select new ResultUsuario
                                   {
                                       Idusuario = u.Idusuario,
                                       Idperfil = u.Idperfil,
                                       Nombreperfil = p.Nombreperfil,
                                       Departamento = ub.Departamento,
                                       Provincia = ub.Provincia,
                                       Distrito = ub.Distrito,
                                       Nombres = u.Nombres,
                                       Apellidos = u.Apellidos,
                                       Direccion01 = u.Direccion01,
                                       Direccion02 = u.Direccion02,
                                       Telefono01 = u.Telefono01,
                                       Telefono02 = u.Telefono02,
                                       Celular01 = u.Celular01,
                                       Celular02 = u.Celular02,
                                       Ubicacion01 = u.Ubicacion01,
                                       Ubicacion02 = u.Ubicacion02,
                                       Clave = u.Clave,
                                       Intentos = u.Intentos,
                                       Bloqueado = u.Bloqueado,
                                       Estado = u.Estado,
                                       Crea = u.Crea,
                                       Fechacrea = u.Fechacrea,
                                       Modifica = u.Modifica,
                                       Fechamodifica = u.Fechamodifica
                                   };
                if (resultado.result != null)
                {
                    resultado.success = true;
                    resultado.code = "0000";
                    resultado.mensaje = "Metodo GetAllUsuarios";
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
                resultado.mensaje = "Error al Cargar Usuarios:" + ex.Message;
            }
            return Ok(resultado);
        }

        /// <summary>
        /// Obtener informacion del usuario
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información del usuario</returns>
        [HttpPost("GetOneUsuario")]
        [ProducesResponseType(typeof(BaseResponse<ResultUsuario>), StatusCodes.Status200OK)]
        public ActionResult<BaseResponse<ResultUsuario>> GetOneUsuario(int idUsuario)
        {
            BaseResponse<ResultUsuario> baseResponse = new BaseResponse<ResultUsuario>
            {
                result = new ResultUsuario()
            };
            try
            {
                var usuario = _context.Usuario.FirstOrDefault(x => x.Idusuario.Equals(idUsuario));
                if (usuario != null)
                {
                    baseResponse.result.Idusuario = usuario.Idusuario;
                    baseResponse.result.Idperfil = usuario.Idperfil;
                    baseResponse.result.Idubigeo = usuario.Idubigeo;
                    baseResponse.result.Nombres = usuario.Nombres;
                    baseResponse.result.Apellidos = usuario.Apellidos;
                    baseResponse.result.Direccion01 = usuario.Direccion01;
                    baseResponse.result.Direccion02 = usuario.Direccion02;
                    baseResponse.result.Telefono01 = usuario.Telefono01;
                    baseResponse.result.Telefono02 = usuario.Telefono02;
                    baseResponse.result.Celular01 = usuario.Celular01;
                    baseResponse.result.Celular02 = usuario.Celular02;
                    baseResponse.result.Ubicacion01 = usuario.Ubicacion01;
                    baseResponse.result.Ubicacion02 = usuario.Ubicacion02;
                    baseResponse.result.Clave = usuario.Clave;
                    baseResponse.result.Intentos = usuario.Intentos;
                    baseResponse.result.Bloqueado = usuario.Bloqueado;
                    baseResponse.result.Crea = usuario.Crea;
                    baseResponse.result.Fechacrea = usuario.Fechacrea;
                    baseResponse.result.Modifica = usuario.Modifica;
                    baseResponse.result.Fechamodifica = usuario.Fechamodifica;

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
        /// Insertar Usuario
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la insercion del Usuario</returns>
        [HttpPost("InsertUsuario")]
        [ProducesResponseType(typeof(BaseResponse<ResultUsuarioIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultUsuarioIU>>> InsertUbigeo(ResultUsuario request)
        {
            BaseResponse<ResultUsuarioIU> baseResponse = new BaseResponse<ResultUsuarioIU>
            {
                result = new ResultUsuarioIU()
            };
            try
            {
                Usuario usuarioinsert = new Usuario
                {
                    Idusuario = request.Idusuario,
                    Idperfil = request.Idperfil,
                    Idubigeo = request.Idubigeo,
                    Nombres = request.Nombres,
                    Apellidos = request.Apellidos,
                    Direccion01 = request.Direccion01,
                    Direccion02 = request.Direccion02,
                    Telefono01 = request.Telefono01,
                    Telefono02 = request.Telefono02,
                    Celular01 = request.Celular01,
                    Celular02 = request.Celular02,
                    Ubicacion01 = request.Ubicacion01,
                    Ubicacion02 = request.Ubicacion02,
                    Clave = request.Clave,
                    Intentos = request.Intentos,
                    Bloqueado = request.Bloqueado,
                    Estado = request.Estado,
                    Crea = request.Crea,
                    Fechacrea = request.Fechacrea,
                    Modifica = null,
                    Fechamodifica = null
                };
                //Insertar Registro en Usuario
                _context.Usuario.Add(usuarioinsert);
                await _context.SaveChangesAsync();

                baseResponse.result.idUsuario = usuarioinsert.Idusuario;
                baseResponse.result.mensaje = "Se insertó correctamente";

                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo InsertUsuario";
            }
            catch (Exception ex)
            {
                baseResponse.result.idUsuario = 0;
                baseResponse.result.mensaje = "No se pudo registrar el Usuario. " + ex.ToString();

                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo InsertUsuario Error";
            }

            return Ok(baseResponse);
        }

        /// <summary>
        /// Actualizar usuario
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la actualizacion del usuario</returns>
        [HttpPost("UpdateUsuario")]
        [ProducesResponseType(typeof(BaseResponse<ResultUsuarioIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultUsuarioIU>>> UpdateUsuario(ResultUsuario request)
        {
            BaseResponse<ResultUsuarioIU> baseResponse = new BaseResponse<ResultUsuarioIU>
            {
                result = new ResultUsuarioIU()
            };
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Usuario usuarioupdate = new Usuario
                        {
                            Idusuario = request.Idusuario,
                            Idperfil = request.Idperfil,
                            Idubigeo = request.Idubigeo,
                            Nombres = request.Nombres,
                            Apellidos = request.Apellidos,
                            Direccion01 = request.Direccion01,
                            Direccion02 = request.Direccion02,
                            Telefono01 = request.Telefono01,
                            Telefono02 = request.Telefono02,
                            Celular01 = request.Celular01,
                            Celular02 = request.Celular02,
                            Ubicacion01 = request.Ubicacion01,
                            Ubicacion02 = request.Ubicacion02,
                            Clave = request.Clave,
                            Intentos = request.Intentos,
                            Bloqueado  = request.Bloqueado,
                            Estado = request.Estado,
                            Modifica = request.Modifica,
                            Fechamodifica = request.Fechamodifica
                        };

                        _context.Usuario.Update(usuarioupdate);
                        await _context.SaveChangesAsync();
                        transaction.Commit();

                        baseResponse.result.idUsuario = usuarioupdate.Idusuario;
                        baseResponse.result.mensaje = "Se actualizó correctamente";

                        baseResponse.success = true;
                        baseResponse.code = "00000";
                        baseResponse.mensaje = "Metodo UpdateUsuario";

                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        baseResponse.result.idUsuario = 0;
                        baseResponse.result.mensaje = "No se pudo actualizar el Usuario. " + ex.ToString();
                        baseResponse.success = false;
                        baseResponse.code = "-1";
                        baseResponse.mensaje = "Metodo UpudateUsuario Error";
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                baseResponse.result.idUsuario = 0;
                baseResponse.result.mensaje = "No se pudo actualizar el Usuario. " + ex.ToString();
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo UpudateUsuario Error";
            }
            return Ok(baseResponse);
        }

        /// <summary>
        /// Eliminar Usuario
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la eliminacion del usuario</returns>
        [HttpPost("DeleteUsuario")]
        [ProducesResponseType(typeof(BaseResponse<int?>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<int?>>> DeleteUsuario(int Idusuario)
        {
            BaseResponse<int?> baseResponse = new BaseResponse<int?>();
            try
            {
                //Eliminar Usuario
                var usuario = _context.Usuario.FirstOrDefault(x => x.Idusuario.Equals(Idusuario));
                if (usuario == null)
                {
                    return NotFound();
                }
                else
                {
                    _context.Usuario.Remove(usuario);
                    await _context.SaveChangesAsync();
                }
                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo DeleteUsuario";
            }
            catch (Exception ex)
            {
                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo DeleteUsuario Error. " + ex.ToString();
                return NotFound();
            }
            baseResponse.result = null;
            return Ok(baseResponse);
        }

    }
}