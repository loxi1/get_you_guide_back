using System;
using System.Collections.Generic;

#nullable disable

namespace WebApi.Models
{
    public partial class Favorito
    {
        public int Id { get; set; }
        public int? PaquetesTuristicoId { get; set; }
        public int? UsuarioId { get; set; }
        public int? Estado { get; set; }

        public virtual PaquetesTuristico PaquetesTuristico { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
