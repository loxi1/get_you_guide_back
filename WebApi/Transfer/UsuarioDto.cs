using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Transfer
{
    public class UsuarioDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Ubicacion { get; set; }
        public string Telefono { get; set; }
        public int? PerfilId { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public int? PersonaId { get; set; }
        public int? UsuarioId { get; set; }
    }
}
