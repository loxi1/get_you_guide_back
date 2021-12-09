using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Transfer;

namespace WebApi.Data.Soa
{
    public class RatingSoa
    {
        public async Task<Rating> RegistrarRating(RatingDto rating, SOAContext _ctx)
        {
            var reiting = new Rating()
            {
                Rating1 = rating.Rating,
                UsuarioId = rating.UsuarioId,
                PaquetesTuristicoId = rating.PaquetesTuristicoId
            };
            var lista = await _ctx
                .Ratings
                .SingleOrDefaultAsync(t => t.PaquetesTuristicoId == rating.PaquetesTuristicoId && t.UsuarioId == rating.UsuarioId);
            if (lista == null)
            {
                await _ctx.Ratings.AddAsync(reiting);
                await _ctx.SaveChangesAsync();
            }
            else
            {
                Rating rati = await _ctx.Ratings.FindAsync(lista.Id);
                rati.Rating1 = rating.Rating;
                rati.UsuarioId = rating.UsuarioId;
                rati.PaquetesTuristicoId = rating.PaquetesTuristicoId;
                _ctx.Entry(rati).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                reiting.Id = lista.Id;
            }
            return reiting;
        }
    }
}
