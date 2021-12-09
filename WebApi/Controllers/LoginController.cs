using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Soa;
using WebApi.Helper;
using WebApi.Models;
using WebApi.Transfer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [EnableCors("AnotherPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly SOAContext _ctx;
        private readonly IConfiguration _conf;
        public LoginController(SOAContext ctx, IConfiguration conf)
        {
            _ctx = ctx;
            _conf = conf;
        }
        [HttpPost("InicioSession")]
        public async Task<ManejadorRtaToken> InicioSession([FromBody]SessionDto usua)
        {
            var rta = new ManejadorRtaToken { };
            LoginSoa logi = new LoginSoa();
            var usuario = await logi.Login(usua.email, usua.password, _ctx);

            if (usuario != null)
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
            if (usuarios != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = usuarios;
            }
            return rta;
        }
    }
}
