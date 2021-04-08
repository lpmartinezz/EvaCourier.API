using EvaCourier.API.Models;
using EvaCourier.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EvaCourier.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly DBEvaContext _context;

        public TokenController(IConfiguration configuration, DBEvaContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        /// <summary>
        /// Para Login y generación de Tokens
        /// </summary>
        /// <response code="404"></response>
        /// <returns>Retorna los datos de todos los perfiles</returns>
        [HttpPost("Login")]
        //public async Task<IActionResult> Post(Usuario _userData)
        public async Task<IActionResult> Login(string sNombreUsuario, string sClave)
        {
            if (sNombreUsuario != null && sClave != null)
            {
                var user = await GetUser(sNombreUsuario, sClave);
                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Idusuario", user.Idusuario.ToString()),
                    new Claim("Nombres", user.Nombres),
                    new Claim("Apellidos", user.Apellidos),
                    new Claim("Nombreusuario", user.Nombreusuario),
                    new Claim("Correo", user.Correo)


                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Credenciales inválidas");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<Usuario> GetUser(string sUsuario, string password)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Nombreusuario == sUsuario && u.Clave == password);
        }

        private async Task<Usuario> GetEmail(string sEmail)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => u.Correo == sEmail || u.Correo2 == sEmail);
        }

        

    }
}
