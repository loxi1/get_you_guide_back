using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Transfer;

namespace WebApi.Data.Soa
{
    public class PaqueteTuristicoSoa
    {
        public async Task<DisponiblePaquetedt> DisponibilidadPaqueteTuristico(int idpaquete, DateTime fechareserva, int cantidadpersonas, SOAContext _ctx)
        {
            string unidad = "";
            decimal? valor = 0;
            DateTime? fechafin;
            DateTime? fechainicioReserva;
            int? idrese = 1;
            string textoHoraInicio = "";
            string fechacancelacion = "";

            DateTime thisDay = DateTime.UtcNow.Date;

            if (idpaquete > 0 && cantidadpersonas > 0)
            {
                var lista2 = await _ctx.PaquetesTuristicos.FirstOrDefaultAsync(t => t.Id == idpaquete && t.MaxNumeroPersonas >= cantidadpersonas && fechareserva > thisDay && fechareserva >= t.FechaInicio && fechareserva <= t.FechaFin);

                if (lista2 != null)
                {
                    unidad = lista2.UnidadDuracion.ToUpper();
                    valor = lista2.TiempoDuracion;
                    fechainicioReserva = lista2.FechaInicio;
                    DateTime temp = Convert.ToDateTime(fechainicioReserva);
                    textoHoraInicio = temp.ToLongTimeString();

                    DateTime fechainicior = Convert.ToDateTime(fechareserva.ToString("yyyy-MM-dd") + " " + textoHoraInicio);
                    DateTime startTime = new DateTime(fechainicior.Year, fechainicior.Month, fechainicior.Day, fechainicior.Hour, 0, 0);

                    DateTime answer = (unidad == "DIAS") ? startTime.AddDays(Convert.ToInt32(valor)) : startTime.AddHours((double)valor);

                    fechafin = answer;

                    DateTime fechafinr = Convert.ToDateTime(fechafin);

                    var lista3 = await _ctx.Reservas.FirstOrDefaultAsync(t => t.PaquetesTuristicoId == idpaquete && t.FechaInicio >= thisDay && t.FechaInicio >= fechainicior && t.FechaInicio <= fechafinr);

                    if (lista3 == null)
                    {
                        fechafin = (unidad == "DIAS" && valor > 1) ? fechafinr.AddDays(-1) : fechafin;

                        DateTime temp4 = Convert.ToDateTime(fechafin);
                        CultureInfo culture = new CultureInfo("es-ES");
                        fechacancelacion = temp4.ToString("U", culture);

                        idrese = 0;
                    }
                }
            }
            
            var obj = new DisponiblePaquetedt {};
            if (idrese == 0)
            {
                var data = await _ctx.PaquetesTuristicos.FirstOrDefaultAsync(t => t.Id == idpaquete);
                obj.Id = data.Id;
                obj.PaqueteTuristico = data.PaqueteTuristico;
                obj.PrecioUnitario = data.PrecioUnitario;
                obj.Moneda = data.Moneda;
                obj.Simbolo = data.Simbolo;
                obj.Cantidad = cantidadpersonas;
                obj.MontoTotal = data.PrecioUnitario * cantidadpersonas;
                obj.FechaCancelacion = fechacancelacion;
                obj.HoraInicio = textoHoraInicio;
            }
            else
                return null;

            return obj;
        }

        public List<PaquetesTuristicodt> ListarPaquetesSugeridos(int idpais, int idcat, int iduser, SOAContext _ctx)
        {
            var lista = _ctx.PaquetesTuristicos.AsNoTracking()
                .Where(t => t.IdPais == idpais && t.IdCategoria == idcat)
                .OrderByDescending(t => t.PaqueteTuristico)
                .Select(m => new PaquetesTuristicodt
                {
                    Id = m.Id,
                    PaqueteTuristico = m.PaqueteTuristico,
                    Descripcion = m.Descripcion,
                    Moneda = m.Moneda,
                    Simbolo = m.Simbolo,
                    PrecioUnitario = m.PrecioUnitario,
                    TiempoDuracion = m.TiempoDuracion,
                    UnidadDuracion = m.UnidadDuracion,
                    Unaimagen = m.Galeria.FirstOrDefault().Url,
                    Raiting = (double)m.Ratings.Average(t=> t.Rating1),
                    Esfavorito = m.Favoritos.FirstOrDefault(t => t.Estado == 1 && t.PaquetesTuristicoId == m.Id && t.UsuarioId == iduser).Id
                    /*,Galeria = (ICollection<Galeriadt>)m.Galeria.Select(c => new Galeriadt
                    { 
                        Id = c.Id,
                        Url = c.Url
                    })*/
                });
            return lista.ToList();
        }

        public Object VerPaqueteTuristico(int id, SOAContext _ctx)
        {
            var obj = new PaquetesTuristicodt(){ };
            var lista = _ctx.PaquetesTuristicos.FirstOrDefault(t => t.Id == id);
            if (lista != null)
            {
                obj.Id = lista.Id;
                obj.PaqueteTuristico = lista.PaqueteTuristico;
                obj.Descripcion = lista.Descripcion;
                obj.Moneda = lista.Moneda;
                obj.Simbolo = lista.Simbolo;
                obj.PrecioUnitario = lista.PrecioUnitario;
                obj.TiempoDuracion = lista.TiempoDuracion;
                obj.UnidadDuracion = lista.UnidadDuracion;
                obj.IdCategoria = lista.IdCategoria;
                obj.IdPais = lista.IdPais;
            }
            else
                return null;
            
            return obj;
        }

        public List<PaquetesTuristicodt> ListarPaquetesTuristicos(int iduser, SOAContext _ctx)
        {
            var lista = _ctx.PaquetesTuristicos.AsNoTracking()
                .OrderByDescending(b => b.PaqueteTuristico)
                .Select(m => new PaquetesTuristicodt
                {
                    Id = m.Id,
                    PaqueteTuristico = m.PaqueteTuristico,
                    Descripcion = m.Descripcion,
                    Moneda = m.Moneda,
                    Simbolo = m.Simbolo,
                    PrecioUnitario = m.PrecioUnitario,
                    TiempoDuracion = m.TiempoDuracion,
                    UnidadDuracion = m.UnidadDuracion,
                    Unaimagen = m.Galeria.FirstOrDefault().Url,
                    Raiting = (double)m.Ratings.Average(t => t.Rating1),
                    Esfavorito = m.Favoritos.FirstOrDefault(t=> t.Estado == 1 && t.PaquetesTuristicoId == m.Id && t.UsuarioId == iduser).Id
                    /*,Galeria = (ICollection<Galeriadt>)m.Galeria.Select(c => new Galeriadt
                    { 
                        Id = c.Id,
                        Url = c.Url
                    })*/
                });
            return lista.ToList();
        }
    }
}
