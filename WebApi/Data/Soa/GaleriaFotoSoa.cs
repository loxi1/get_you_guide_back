using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Transfer;

namespace WebApi.Data.Soa
{
    public class GaleriaFotoSoa
    {
        public List<Galeriadt> VerGaleriaFotos(int id, SOAContext _ctx, IMapper _imapper)
        {
            var lista = from b in _ctx.Galerias.Where(t => t.PaquetesTuristicoId == id)
                        select new Galeriadt()
                        {
                            Id = b.Id,
                            Url = b.Url
                        };

            return lista.ToList();
        }

        public Galeriadt VerGaleriaFotos2(int id, SOAContext _ctx, IMapper _imapper)
        {
            
            List< Galeria > lista = (from b in _ctx.Galerias.Where(t => t.PaquetesTuristicoId == id)
                                     select b).ToList();
            var gal = new Galeriadt {  };
            //lista.Where(t => t.PaquetesTuristicoId == id);
            var obj = lista.ToList();

            IQueryable<Galeria> queryable = _ctx.Galerias.Where(t => t.PaquetesTuristicoId == id).OrderBy(t => t.Url).Select(x => x);
            var rta = queryable.Where(t => t.PaquetesTuristicoId == id);
            var ga = _ctx.Galerias.First(t => t.PaquetesTuristicoId == id).ToString();
            var galeria = _ctx.Galerias.Where(t => t.PaquetesTuristicoId == id).OrderBy(t => t.Url).ToList();

            return _imapper.Map<Galeriadt>(galeria);
        }
    }
}
