using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Transfer;

namespace WebApi.Data.Soa
{
    public class LoginSoa
    {
        public async Task<UsuarioDto> Login(string email, string clave, SOAContext _ctx)
        {            
            var usuario = await _ctx.Usuarios.FirstOrDefaultAsync(x => x.Correo == email);
            var usuarioDto = new UsuarioDto{};

            if (usuario == null)
                return null;

            if (!VerifyPasswordHash(clave, usuario.PasswordHash, usuario.PaswordSalt))
                return null;

            if (usuario != null)
            {
                var pers = await _ctx.Personas.FirstOrDefaultAsync(x => x.Id == usuario.PersonaId);
                if( pers != null)
                {
                    usuarioDto.PersonaId = usuario.PersonaId;
                    usuarioDto.PerfilId = usuario.PerfilId;
                    usuarioDto.Nombres = pers.Nombres;
                    usuarioDto.Apellidos = pers.Apellidos;
                    usuarioDto.Correo = usuario.Correo;
                    usuarioDto.UsuarioId = usuario.Id;
                }
            }

            return usuarioDto;
        }

        internal Task Login(object correo, object clave, SOAContext ctx)
        {
            throw new NotImplementedException();
        }

        private bool VerifyPasswordHash(string clave, byte[] passwordHash, byte[] paswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(paswordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(clave));

                for (int i = 0; i < computeHash.Length; i++)
                {
                    if (computeHash[i] != passwordHash[i]) return false;
                }
            }
            return true;
        }
        public async Task<UsuarioDto> Registrar(UsuarioDto usuario, SOAContext _ctx)
        {
            var persona = new Persona()
            {
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Direccion = usuario.Direccion,
                Ubicacion = usuario.Ubicacion,
                Telefono = usuario.Telefono
            };            

            await _ctx.Personas.AddAsync(persona);
            await _ctx.SaveChangesAsync();

            if (persona.Id >0)
            {
                byte[] passwordHash;
                byte[] passwordSalt;

                CreatePassowordHash(usuario.Clave, out passwordHash, out passwordSalt);

                var user = new Usuario()
                {
                    Correo = usuario.Correo,
                    PerfilId = 1,
                    PersonaId = persona.Id,
                    PasswordHash = passwordHash,
                    PaswordSalt = passwordSalt
                };

                await _ctx.Usuarios.AddAsync(user);
                await _ctx.SaveChangesAsync();

                usuario.PersonaId = persona.Id;
                usuario.UsuarioId = user.Id;
                usuario.PerfilId = user.PerfilId;
            }
            return usuario;
        }

        private void CreatePassowordHash(string clave, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hamc = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hamc.Key;
                passwordHash = hamc.ComputeHash(System.Text.Encoding.UTF8.GetBytes(clave));
            }
        }

    }
}
