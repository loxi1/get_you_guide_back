using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Transfer;

namespace WebApi.Data.Soa
{
    public class PaqueteInformacionSoa
    {
        public List<PaquetesInformaciondt> VerInformacionGeneral(int id, SOAContext _ctx)
        {
            var lista = from b in _ctx.PaquetesInformacions.Where(t => t.PaquetesTuristicoId == id)
                       select new PaquetesInformaciondt()
                       {
                           Id = b.Id,
                           Informacion = new InformacionesGeneraledt()
                           {
                               Id = b.InformacionesGenerale.Id,
                               InformacionGeneral = b.InformacionesGenerale.InformacionGeneral,
                               DetalleInformacionGeneral = b.InformacionesGenerale.DetalleInformacionGeneral,
                               Icons = b.InformacionesGenerale.Icons
                           }
                       };
            return lista.ToList();
        }
    }
}
