using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Transfer;

namespace WebApi.Data.Soa
{
    public class FavoritoSoa
    {
        public async Task<Favorito> RegistrarFavorito(FavoritoSaveDto favori, SOAContext _ctx)
        {
            var favoritos = new Favorito()
            {
                Estado = favori.Estado,
                UsuarioId = favori.UsuarioId,
                PaquetesTuristicoId = favori.PaquetesTuristicoId
            };
            var lista = await _ctx
                .Favoritos
                .SingleOrDefaultAsync(t => t.PaquetesTuristicoId == favori.PaquetesTuristicoId && t.UsuarioId == favori.UsuarioId);
            if (lista == null)
            {
                await _ctx.Favoritos.AddAsync(favoritos);
                await _ctx.SaveChangesAsync();
            }
            else
            {
                Favorito favi = await _ctx.Favoritos.FindAsync(lista.Id);
                favi.Estado = favori.Estado;
                favi.UsuarioId = favori.UsuarioId;
                favi.PaquetesTuristicoId = favori.PaquetesTuristicoId;
                _ctx.Entry(favi).State = EntityState.Modified;
                await _ctx.SaveChangesAsync();
                favoritos.Id = lista.Id;
            }
            return favoritos;
        }
    }
}
