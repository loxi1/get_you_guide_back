using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Transfer
{
    public class RatingDto
    {
        public decimal? Rating { get; set; }
        public int? PaquetesTuristicoId { get; set; }
        public int? UsuarioId { get; set; }
    }
}
