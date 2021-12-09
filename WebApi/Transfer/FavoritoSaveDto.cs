using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Transfer
{
    public class FavoritoSaveDto
    {
        public int? PaquetesTuristicoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? Estado { get; set; }
    }
}
