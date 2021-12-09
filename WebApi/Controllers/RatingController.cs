using AutoMapper.Configuration;
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
    public class RatingController : ControllerBase
    {
        private readonly SOAContext _ctx;

        public RatingController(SOAContext ctx)
        {
            _ctx = ctx;
        }

        // POST api/<RatingController>
        [HttpPost("RegistrarRating")]
        public async Task<ManejadorRta> RegistrarRating([FromBody]RatingDto rating)
        {
            var rta = new ManejadorRta { };
            RatingSoa rat = new RatingSoa();
            Console.WriteLine(rating.UsuarioId);
            var rati = await rat.RegistrarRating(rating, _ctx);
            if (rati != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = rati;
            }
            return rta;
        }
    }
}
