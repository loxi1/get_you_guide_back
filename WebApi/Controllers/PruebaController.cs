using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using WebApi.Helper;
using WebApi.Transfer;
using WebApi.Data.Soa;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [EnableCors("AnotherPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PruebaController : ControllerBase
    {
        private readonly SOAContext _ctx;
        private readonly IConfiguration _conf;
        public PruebaController(SOAContext ctx, IConfiguration conf)
        {
            _ctx = ctx;
            _conf = conf;
        }
        [HttpGet("")]
        public string Get()
        {
            return "BIENBENIDO  <<<GUETYOURGUIDE>>>>";
        }

        [HttpGet("ExisteUsuario")]
        public async Task<bool> ExisteUsuario(string email)
        {
            if (await _ctx.Usuarios.AnyAsync(x => x.Correo == email))
                return true;
            return false;
        }

        [HttpGet("Login")]
        public async Task<ManejadorRtaToken> Login([FromBody] UsuarioDto usua)
        {
            var rta = new ManejadorRtaToken{};
            LoginSoa logi = new LoginSoa();
            var usuario = await logi.Login(usua.Correo, usua.Clave, _ctx);

            if(usuario !=null)
            {
                string secret = _conf.GetValue<string>("Secret");
                var jwHelper = new JWTHelper(secret);
                var token = jwHelper.CreateToken(usuario.Correo);
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = usuario;
                rta.Token = token;
            }
            return rta;
        }

        [HttpPost("Registrar")]
        public async Task<ManejadorRta> Registrar([FromBody] UsuarioDto usuario)
        {
            var rta = new ManejadorRta { };
            LoginSoa logi = new LoginSoa();
            var usuarios = await logi.Registrar(usuario, _ctx);
            if( usuarios != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = usuarios;
            }
            return rta;
        }

        [HttpGet("GetAllUsuarioAsync")]
        public async Task<List<Usuario>> GetAllUsuarioAsync()
        {
            return await _ctx.Usuarios.ToListAsync();
        }

        [HttpPost("Login2")]
        public ActionResult<object> Login2([FromBody]Usuariodt usuario) {
            
            string secret = _conf.GetValue<string>("Secret");
            var jwHelper = new JWTHelper(secret);
            var token = jwHelper.CreateToken(usuario.Correo);
            return Ok(new { ok = true, msn = "Logueado con exito", token});
        }
    }
}
