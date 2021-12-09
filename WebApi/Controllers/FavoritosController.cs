using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Data.Soa;
using WebApi.Models;
using WebApi.Transfer;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [EnableCors("AnotherPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritosController : ControllerBase
    {
        private readonly SOAContext _ctx;

        public FavoritosController(SOAContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost("RegistrarFavorito")]
        public async Task<ManejadorRta> RegistrarFavorito([FromBody]FavoritoSaveDto favorit)
        {
            var rta = new ManejadorRta { };
            FavoritoSoa fa = new FavoritoSoa();
            Console.WriteLine(favorit.UsuarioId);
            var favo = await fa.RegistrarFavorito(favorit, _ctx);
            if (favo != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = favo;
            }
            return rta;
        }
    }
}
