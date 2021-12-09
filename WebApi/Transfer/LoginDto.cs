using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Transfer
{
    public class LoginDto
    {
        public int Id { get; set; }
        public int? PersonaId { get; set; }
        public int? PerfilId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
    }
}
