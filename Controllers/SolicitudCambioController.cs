using EvaCourier.API.Models;
using EvaCourier.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EvaCourier.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudCambioController : ControllerBase
    {
        private readonly DBEvaContext _context;
        public SolicitudCambioController(DBEvaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insertar Solicitud Cambio
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna información de la insercion de la Solicitud de Cambio</returns>
        [HttpPost("InsertSolicitudCambio")]
        [ProducesResponseType(typeof(BaseResponse<ResultSolicitudCambioIU>), StatusCodes.Status200OK)]
        public async Task<ActionResult<BaseResponse<ResultSolicitudCambioIU>>> InsertSolicitudCambio(ResultSolicitudcambio request)
        {
            BaseResponse<ResultSolicitudCambioIU> baseResponse = new BaseResponse<ResultSolicitudCambioIU>
            {
                result = new ResultSolicitudCambioIU()
            };
            try
            {
                Solicitudcambio solicitudcambio = new Solicitudcambio
                {
                    Idsolicitudcambio = request.Idsolicitudcambio,
                    Correo = request.Correo,
                    Codigogenerado = request.Codigogenerado,
                    Estadocodigo= request.Estadocodigo,
                    Fechasolicitud = request.Fechasolicitud,
                    Fechavencimiento = request.Fechavencimiento,
                    Estado = request.Estado,
                    Crea = request.Crea,
                    Fechacrea = request.Fechacrea,
                    Modifica = null,
                    Fechamodifica = null
                };
                //Insertar Registro en Usuario
                _context.Solicitudcambio.Add(solicitudcambio);
                await _context.SaveChangesAsync();

                baseResponse.result.idSolicitudcambio = solicitudcambio.Idsolicitudcambio;
                baseResponse.result.mensaje = "Se insertó correctamente";

                baseResponse.success = true;
                baseResponse.code = "00000";
                baseResponse.mensaje = "Metodo InsertSolicitudCambio";
            }
            catch (Exception ex)
            {
                baseResponse.result.idSolicitudcambio = 0;
                baseResponse.result.mensaje = "No se pudo registrar la Solicitud. " + ex.ToString();

                baseResponse.success = false;
                baseResponse.code = "-1";
                baseResponse.mensaje = "Metodo InsertSolicitudCambio Error";
            }
            return Ok(baseResponse);
        }
    }
}