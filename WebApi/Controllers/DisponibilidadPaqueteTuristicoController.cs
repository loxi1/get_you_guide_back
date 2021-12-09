using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Data.Soa;
using WebApi.Helper;
using WebApi.Models;
using WebApi.Transfer;

namespace WebApi.Controllers
{
    [EnableCors("AnotherPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadPaqueteTuristicoController : ControllerBase
    {
        private readonly SOAContext _ctx;
        private readonly IMapper _imapper;
        public DisponibilidadPaqueteTuristicoController(SOAContext ctx, IMapper imapper)
        {
            _ctx = ctx;
            _imapper = imapper;
        }
        [HttpGet("")]
        public string Get()
        {
            return "BIENBENIDO  <<<GUETYOURGUIDE>>>>";
        }

        [HttpGet("ReservaDisponible")]
        public async Task<ManejadorRta> ReservaDisponible(int idp, DateTime fechareserva, int numper)
        {
            var rta = new ManejadorRta { };
            PaqueteTuristicoSoa paq = new PaqueteTuristicoSoa();
            var reserva = await paq.DisponibilidadPaqueteTuristico(idp, fechareserva, numper, _ctx);
            if(reserva != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = reserva;
            }
            return rta;
        }

        [HttpGet("ListarPaquetesSugeridos")]
        public ManejadorRta ListarPaquetesSugeridos(int idpais, int idcat, int iduser)
        {
            var rta = new ManejadorRta { };
            PaqueteTuristicoSoa paq = new PaqueteTuristicoSoa();
            var sugeridos = paq.ListarPaquetesSugeridos(idpais, idcat, iduser, _ctx);
            if (sugeridos != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = sugeridos;
            }
            return rta;
        }

        [HttpGet("VerPaqueteTuristico")]
        public ManejadorRta VerPaqueteTuristico(int id)
        {
            var rta = new ManejadorRta { };
            PaqueteTuristicoSoa paq = new PaqueteTuristicoSoa();
            var ver = paq.VerPaqueteTuristico(id, _ctx);
            if (ver != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = ver;
            }
            return rta;
        }

        [HttpGet("VerGaleriaFotos")]
        public ManejadorRta VerGaleriaFotos(int id)
        {
            var rta = new ManejadorRta { };
            GaleriaFotoSoa ga = new GaleriaFotoSoa();
            var foto = ga.VerGaleriaFotos(id, _ctx, _imapper);
            if (foto != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = foto;
            }
            return rta;
        }

        [HttpGet("VerInformacionGeneral")]
        public ManejadorRta VerInformacionGeneral(int id)
        {
            var rta = new ManejadorRta { };
            PaqueteInformacionSoa paqi = new PaqueteInformacionSoa();
            var info = paqi.VerInformacionGeneral(id, _ctx);
            if (info != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = info;
            }
            return rta;
        }

        [HttpGet("ListarPaquetesTuristicos")]
        public ManejadorRta ListarPaquetesTuristicos(int iduser)
        {
            var rta = new ManejadorRta { };
            PaqueteTuristicoSoa paq = new PaqueteTuristicoSoa();
            var sugeridos = paq.ListarPaquetesTuristicos(iduser, _ctx);
            if (sugeridos != null)
            {
                rta.Estado = true;
                rta.Msn = "Ok";
                rta.Rta = sugeridos;
            }
            return rta;
        }
    }
}
